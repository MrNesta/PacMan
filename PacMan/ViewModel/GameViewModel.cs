using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Linq;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight.Command;
using PacMan.Infrastructure;
using PacMan.Model;
using PacMan.Interfaces;
using PacMan.Repositories;

namespace PacMan.ViewModel
{
    public class GameViewModel : DialogViewModelBase
    {
        private List<Image> _characterImages;
        ImageUploadService _imageUploadService;
        private IGameField _gameField;
        private List<UIElement> _coins;
        private DispatcherTimer _moveTimer;
        private DispatcherTimer _scatterModeTimer;
        private DispatcherTimer _chaseModeTimer;
        private DispatcherTimer _frightenedModeTimer;
        private bool _isGameStoped;
        private IPluginEnemyBehaviorAlgorithm _pluginEnemyAlgorithm;
        private IDialogService _playerNameDialog;
        private IDialogService _recordsTableDialog;
        private IDialogService _pluginsDialog;
        private IRepository<Player> _database;
        private MediaPlayer _mediaPlayer;

        public GameViewModel(IDialogService playerNameDialog,
            IDialogService recordsTableDialog,
            IDialogService pluginsDialog, IGameField gameField)
        {
            _playerNameDialog = playerNameDialog;

            if (!(playerNameDialog).Open())
            {
                DialogResult = false;
            }

            _recordsTableDialog = recordsTableDialog;
            _pluginsDialog = pluginsDialog;
            EnabledDifficalty = true;
            CurrentPlayer = new Player((playerNameDialog).Message);
            _levelsOfDifficulty = new DifficultyList();
            _boundChildrens = new ObservableCollection<UIElement>();
            _characterImages = new List<Image>(5);
            _coins = new List<UIElement>();
            _gameField = gameField;
            _gameField.CurrentPlayer = CurrentPlayer;

            _gameField.PropertyChanged += LevelPropertyChanged;
            _gameField.PropertyChanged += ModePropertyChanged;
            _gameField.PropertyChanged += LivesMinusPropertyChanged;
            _gameField.PropertyChanged += GameOverPropertyChanged;
            _gameField.PropertyChanged += CharacterPropertyChanged;

            _imageUploadService = new ImageUploadService();

            _moveTimer = new DispatcherTimer();
            _scatterModeTimer = new DispatcherTimer();
            _chaseModeTimer = new DispatcherTimer();
            _frightenedModeTimer = new DispatcherTimer();

            _moveTimer.Tick += new EventHandler(MoveTick);
            _scatterModeTimer.Tick += new EventHandler(ScatterModeTick);
            _chaseModeTimer.Tick += new EventHandler(ChaseModeTick);
            _frightenedModeTimer.Tick += new EventHandler(FrightenedModeTick);

            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Open(new Uri(@"Resources\Pacman.mp3", UriKind.Relative));
            _mediaPlayer.MediaFailed += new EventHandler<ExceptionEventArgs>(OnMediaFailed);
            _mediaPlayer.MediaEnded += new EventHandler(OnMediaEnded);

            SetImagesCharacter();
        }

        private BitmapImage BlueEnemy { get; set; }

        private BitmapImage Eyes { get; set; }

        public Player CurrentPlayer { get; private set; }

        private string _infoText;

        public string InfoText
        {
            get
            {
                if (_infoText == null)
                    _infoText = "";
                return _infoText;
            }
            set
            {
                _infoText = value;
                RaisePropertyChanged("InfoText");
            }
        }

        private string _pauseText;

        public string PauseText
        {
            get
            {
                if (_pauseText == null)
                    _pauseText = "Pause";
                return _pauseText;
            }
            set
            {
                _pauseText = value;
                RaisePropertyChanged("PauseText");
            }
        }

        public bool EnabledDifficalty
        {
            get
            {
                return _isGameStoped;
            }
            set
            {
                _isGameStoped = value;
                RaisePropertyChanged("EnabledDifficalty");
                (StartNewGameCommand as RelayCommand).RaiseCanExecuteChanged();
                (PauseGameCommand as RelayCommand).RaiseCanExecuteChanged();
                (KeyboardPressCommand as RelayCommand<object>).RaiseCanExecuteChanged();
                (RecordsTableCommand as RelayCommand).RaiseCanExecuteChanged();
                (SaveGameResultsCommand as RelayCommand).RaiseCanExecuteChanged();
                (PluginsCommand as RelayCommand).RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<UIElement> _boundChildrens;

        public ObservableCollection<UIElement> BoundChildrens
        {
            get
            {
                return _boundChildrens;
            }
        }

        private readonly List<Difficulty> _levelsOfDifficulty;

        public List<Difficulty> LevelsOfDifficulty
        {
            get
            {
                return _levelsOfDifficulty;
            }
        }

        private int _currentDifficulty;

        public int SelectedDifficulty
        {
            get
            {
                return _currentDifficulty;
            }
            set
            {
                _currentDifficulty = (value);
                RaisePropertyChanged("SelectedDifficulty");
            }
        }

        private RelayCommand _saveGameCommand;

        public ICommand SaveGameResultsCommand
        {
            get
            {
                if (_saveGameCommand == null)
                    _saveGameCommand = new RelayCommand(
                        () => CurrentPlayer.GameOver = true,
                        () => !_isGameStoped);
                return _saveGameCommand;
            }
        }

        private RelayCommand _recordsTableCommand;

        public ICommand RecordsTableCommand
        {
            get
            {
                if (_recordsTableCommand == null)
                    _recordsTableCommand = new RelayCommand(
                        () => _recordsTableDialog.Open(),
                        () => _isGameStoped);
                return _recordsTableCommand;
            }
        }

        private RelayCommand _pluginsCommand;

        public ICommand PluginsCommand
        {
            get
            {
                if (_pluginsCommand == null)
                    _pluginsCommand = new RelayCommand(ExecutePluginsCommand,
                        () => _isGameStoped);
                return _pluginsCommand;
            }
        }

        private RelayCommand _pauseCommand;

        public ICommand PauseGameCommand
        {
            get
            {
                if (_pauseCommand == null)
                    _pauseCommand = new RelayCommand(ExecutePauseCommand,
                        () => !_isGameStoped);
                return _pauseCommand;
            }
        }
        private RelayCommand _startCommand;

        public ICommand StartNewGameCommand
        {
            get
            {
                if (_startCommand == null)
                    _startCommand = new RelayCommand(
                        () =>
                        {
                            StartGame();
                            EnabledDifficalty = false;
                        },
                        () => _isGameStoped);
                return _startCommand;
            }
        }

        private RelayCommand _exitCommand;

        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new RelayCommand(
                        () =>
                        {
                            DialogResult = false;
                        });
                return _exitCommand;
            }
        }

        private RelayCommand<object> _keyboardPressCommand;

        public ICommand KeyboardPressCommand
        {
            get
            {
                if (_keyboardPressCommand == null)
                    _keyboardPressCommand = new RelayCommand<object>(
                        ExecuteKeyboardPressCommand,
                        (x) => !_isGameStoped);
                return _keyboardPressCommand;
            }
        }

        private void SavePlayer()
        {
            CurrentPlayer.Date = DateTime.Now;

            try
            {
                _database = new PlayerRepository("RecordsConnection");
                using (_database)
                {
                    _database.Create(CurrentPlayer);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error accessing database");
                LogService.SaveToLog(exc.Message);
            }
        }

        private AppDomain _pluginDomain;

        private void ExecutePluginsCommand()
        {
            if (_pluginsDialog.Open())
            {
                string _pluginPath = _pluginsDialog.Message;

                if (_pluginPath != null)
                {
                    _pluginDomain = AppDomain.CreateDomain("PluginAD");
                    AppDomain.CurrentDomain
                        .ReflectionOnlyAssemblyResolve += PluginDomainReflectionOnlyAssemblyResolve;

                    try
                    {
                        Assembly pluginAssembly = Assembly.ReflectionOnlyLoadFrom(_pluginPath);

                        foreach (TypeInfo type in pluginAssembly.DefinedTypes)
                        {
                            if (type.IsClass
                             && type.IsAbstract == false
                             && type.ImplementedInterfaces.Any(
                                 it => it.GUID == typeof(IPluginEnemyBehaviorAlgorithm).GUID)
                             && type.BaseType == typeof(MarshalByRefObject))
                            {
                                _pluginEnemyAlgorithm = _pluginDomain.CreateInstanceFromAndUnwrap(type.
                                    Assembly.CodeBase, type.FullName) as IPluginEnemyBehaviorAlgorithm;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        AppDomain.Unload(_pluginDomain);
                        LogService.SaveToLog(exc.Message);
                    }
                    finally
                    {
                        AppDomain.CurrentDomain
                            .ReflectionOnlyAssemblyResolve -= PluginDomainReflectionOnlyAssemblyResolve;
                    }
                }
                else _pluginEnemyAlgorithm = null;
            }
        }     

        private static Assembly PluginDomainReflectionOnlyAssemblyResolve
            (object sender, ResolveEventArgs args)
        {
            string strTempAssmbPath = "";

            Assembly objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            if (arrReferencedAssmbNames.Any(
                strAssmbName => strAssmbName.FullName
                .Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name
                .Substring(0, args.Name.IndexOf(","))))
            {
                strTempAssmbPath = System.IO.Path.GetDirectoryName(
                    objExecutingAssemblies.Location) + "\\" + args.Name
                    .Substring(0, args.Name.IndexOf(",")) + ".dll";
            }

            if (File.Exists(strTempAssmbPath))
            {
                return Assembly.ReflectionOnlyLoadFrom(strTempAssmbPath);
            }
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        private void ExecutePauseCommand()
        {
            if (PauseText == "Pause")
            {
                _mediaPlayer.Pause();
                StopAll();
                PauseText = "Play";
            }
            else
            {
                _mediaPlayer.Play();
                PlayAll();
                PauseText = "Pause";
            }
        }

        private void PlayAll()
        {
            _moveTimer.Start();
            _frightenedModeTimer.Start();
            _chaseModeTimer.Start();
            _scatterModeTimer.Start();
        }       

        private void ExecuteKeyboardPressCommand(object obj)
        {
            switch ((obj as KeyEventArgs).Key)
            {
                case Key.Left:
                    {
                        _gameField.Pacman.AttemptedRoute = Route.Left;
                        break;

                    }
                case Key.Right:
                    {
                        _gameField.Pacman.AttemptedRoute = Route.Right;
                        break;
                    }
                case Key.Up:
                    {
                        _gameField.Pacman.AttemptedRoute = Route.Top;
                        break;
                    }
                case Key.Down:
                    {
                        _gameField.Pacman.AttemptedRoute = Route.Bottom;
                        break;
                    }
                default:
                    _gameField.Pacman.AttemptedRoute = Route.None;
                    break;
            }
        }

        private bool _isFirstPairOfEnemies = true;

        private void FrightenedModeTick(object sender, EventArgs e)
        {
            _frightenedModeTimer.Interval = new TimeSpan(0, 0, 5);

            if (_isFirstPairOfEnemies)
            {
                _gameField.ChasingMode(_gameField.Blinky,
                    _gameField.Inky);
                _isFirstPairOfEnemies = false;
            }
            else
            {
                _gameField.ChasingMode(_gameField.Pinky,
                    _gameField.Clyde);
                _isFirstPairOfEnemies = true;
            }

            _chaseModeTimer.Start();
            _frightenedModeTimer.Stop();
        }

        private void ChaseModeTick(object sender, EventArgs e)
        {
            if (_isFirstPairOfEnemies)
            {
                _gameField.ScatteringMode(_gameField.Pinky,
                    _gameField.Clyde);
            }
            else
            {
                _gameField.ScatteringMode(_gameField.Blinky,
                    _gameField.Inky);
            }
            _chaseModeTimer.Stop();
            _scatterModeTimer.Start();
        }

        private void ScatterModeTick(object sender, EventArgs e)
        {
            _gameField.ChasingMode();
            _scatterModeTimer.Stop();
            _chaseModeTimer.Start();
        }

        private void MoveTick(object sender, EventArgs e)
        {
            try
            {
                _gameField.MoveCharacters();
            }
            catch (Exception exc)
            {
                CurrentPlayer.GameOver = true;

                if (_pluginDomain != null)
                {
                    AppDomain.Unload(_pluginDomain);
                    _pluginDomain = null;
                }

                _pluginEnemyAlgorithm = null;

                MessageBox.Show("This is a bug in the implementation of the plugin! " +
                    "The algorithm behavior enemy was installed by default.");
                EnabledDifficalty = true;
                LogService.SaveToLog(exc.Message);
            }
        }

        private void StartGame()
        {
            PauseText = "Pause";
            InfoText = "GAME STARTED";
           
            _mediaPlayer.Play();

            RemoveBoundChildrenCollection(_boundChildrens.ToList());
            _coins.Clear();

            CurrentPlayer.Score = 0;
            SetDifficultyMode();
            CurrentPlayer.GameOver = false;
            CurrentPlayer.CurrentLevel.LevelNumber = 1;

            GameFieldCreate();
            _gameField.InitializeCharacters(_pluginEnemyAlgorithm);

            NewGamePreparation();
        }

        private void SetDifficultyMode()
        {
            switch (_currentDifficulty)
            {
                case 0:
                    {
                        CurrentPlayer.Lives = 10;
                        _frightenedModeTimer.Interval = new TimeSpan(0, 0, 8);
                        _scatterModeTimer.Interval = new TimeSpan(0, 0, 10);
                        _chaseModeTimer.Interval = new TimeSpan(0, 0, 28);
                        _moveTimer.Interval = new TimeSpan(0, 0, 0, 0, 170);
                        break;
                    }
                case 1:
                    {
                        CurrentPlayer.Lives = 5;
                        _frightenedModeTimer.Interval = new TimeSpan(0, 0, 5);
                        _scatterModeTimer.Interval = new TimeSpan(0, 0, 7);
                        _chaseModeTimer.Interval = new TimeSpan(0, 0, 20);
                        _moveTimer.Interval = new TimeSpan(0, 0, 0, 0, 135);
                        break;
                    }
                case 2:
                    {
                        CurrentPlayer.Lives = 3;
                        _frightenedModeTimer.Interval = new TimeSpan(0, 0, 3);
                        _scatterModeTimer.Interval = new TimeSpan(0, 0, 5);
                        _chaseModeTimer.Interval = new TimeSpan(0, 0, 15);
                        _moveTimer.Interval = new TimeSpan(0, 0, 0, 0, 85);
                        break;
                    }
                default:
                    break;
            }
        }

        public void GameFieldCreate()
        {
            _gameField.GenerateWalls();
            _gameField.GenerateMaze();
            _gameField.GenerateDots();
            _gameField.InitializeWay();
            _gameField.InitializeEnemyWay();
        }

        private void RemoveBoundChildrenCollection(IEnumerable<UIElement> uIElement)
        {
            foreach (var child in uIElement)
            {
                _boundChildrens.Remove(child);
            }
        }

        private void CharacterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Lives" &&
                e.PropertyName != "Score" &&
                !_isGameStoped)
            {
                Redraw();
            }
        }

        private void GameOverPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GameOver")
            {
                _mediaPlayer.Stop();
                StopAll();
                InfoText = "GAME OVER";
                _gameField.ResetCharacter();
                SavePlayer();
                EnabledDifficalty = true;
            }
        }

        private void LivesMinusPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "LivesMinus" && !_isGameStoped)
            {
                TryAnother();
            }
        }

        private void OnMediaFailed(object sender, ExceptionEventArgs e)
        {
            LogService.SaveToLog(e.ErrorException.Message);
            _mediaPlayer.Close();
        }

        private void OnMediaEnded(object sender, EventArgs e)
        {
            _mediaPlayer.Position = TimeSpan.FromSeconds(0);
            _mediaPlayer.Play();
        }

        private void TryAnother()
        {
            _gameField.ResetCharacter();
            Redraw();
            Thread.Sleep(300);
            _moveTimer.Start();
            _scatterModeTimer.Start();
        }

        private void Redraw()
        {
            RemoveBoundChildrenCollection(_coins);
            RemoveBoundChildrenCollection(_characterImages);
            _coins.Clear();
            DrawDots();
            DrawCharacters();
        }

        private void ModePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Mode")
            {
                _gameField.FrighteningMode();
                _chaseModeTimer.Stop();
                _scatterModeTimer.Stop();

                if (!_frightenedModeTimer.IsEnabled)
                {
                    _frightenedModeTimer.Start();
                }
                else if (_frightenedModeTimer.IsEnabled)
                {
                    _frightenedModeTimer.Interval += new TimeSpan(0, 0, 5);
                }
            }
        }

        private void LevelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Level" &&
                CurrentPlayer.CurrentLevel.LevelNumber != 1)
                NextLevelGame();
        }

        private void NextLevelGame()
        {
            StopAll();
            RemoveBoundChildrenCollection(_boundChildrens.ToList());
            _coins.Clear();
            _gameField.ResetCharacter();
            GameFieldCreate();
            NewGamePreparation();
        }

        private void StopAll()
        {
            _moveTimer.Stop();
            _chaseModeTimer.Stop();
            _frightenedModeTimer.Stop();
            _scatterModeTimer.Stop();
        }

        private void NewGamePreparation()
        {
            DrawMaze();
            DrawDots();
            DrawCharacters();
            _moveTimer.Start();
            _scatterModeTimer.Start();
        }

        private void DrawCharacters()
        {
            DrawPacman();

            if (!_gameField.IsFrighteningMode)
            {
                DrawCharacter(0, _gameField.Blinky);
                DrawCharacter(1, _gameField.Pinky);
                DrawCharacter(2, _gameField.Inky);
                DrawCharacter(3, _gameField.Clyde);
            }
            else
            {
                DrawCharacterFrightened(_gameField.Inky);
                DrawCharacterFrightened(_gameField.Pinky);
                DrawCharacterFrightened(_gameField.Blinky);
                DrawCharacterFrightened(_gameField.Clyde);
            }
        }

        private void DrawPacman()
        {
            var animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(150),
                AutoReverse = true,
                EasingFunction = new ExponentialEase
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DrawCharacter(4, _gameField.Pacman);
            _characterImages[4].BeginAnimation(UIElement.OpacityProperty, animation);

            DrawCharacter(5, _gameField.Pacman);
            _characterImages[4].RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            _characterImages[5].RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            switch (_gameField.Pacman.Route)
            {
                case Route.Left:
                    {
                        _characterImages[4].RenderTransform = new RotateTransform(180);
                        _characterImages[5].RenderTransform = new RotateTransform(180);
                        break;
                    }
                case Route.Right:
                    {
                        _characterImages[4].RenderTransform = new RotateTransform(0);
                        _characterImages[5].RenderTransform = new RotateTransform(0);
                        break;
                    }
                case Route.Top:
                    {
                        _characterImages[4].RenderTransform = new RotateTransform(-90);
                        _characterImages[5].RenderTransform = new RotateTransform(-90);
                        break;
                    }
                case Route.Bottom:
                    {
                        _characterImages[4].RenderTransform = new RotateTransform(90);
                        _characterImages[5].RenderTransform = new RotateTransform(90);
                        break;
                    }
                default:
                    break;
            }
        }

        private void DrawCharacterFrightened(Enemy enemy)
        {
            var img = new Image();
            if (enemy.Visible)
                img.Source = BlueEnemy;
            else
                img.Source = Eyes;

            img.Width = enemy.Width;
            img.Height = enemy.Height;
            Canvas.SetTop(img, enemy.CurrentPoint.CoordinateY * _gameField.Cols);
            Canvas.SetLeft(img, enemy.CurrentPoint.CoordinateX * _gameField.Rows);

            _characterImages.Add(img);
            _boundChildrens.Add(img);
        }

        private void SetImagesCharacter()
        {
            for (var i = 0; i < 6; i++)
            {
                var img = new Image
                {
                    Source = _imageUploadService.GetImage(
                    ImagesPathDataBase.GetPathImage(i))
                };

                _characterImages.Add(img);
            }

            BlueEnemy = _imageUploadService.GetImage(ImagesPathDataBase
                .GetPathImage(nameof(BlueEnemy)));

            Eyes = _imageUploadService.GetImage(ImagesPathDataBase
                .GetPathImage(nameof(Eyes)));
        }

        private void DrawCharacter(int num, Character character)
        {
            _characterImages[num].Width = character.Width;
            _characterImages[num].Height = character.Height;
            Canvas.SetTop(_characterImages[num], 
                character.CurrentPoint.CoordinateY * _gameField.Cols);
            Canvas.SetLeft(_characterImages[num], 
                character.CurrentPoint.CoordinateX * _gameField.Rows);
            _boundChildrens.Add(_characterImages[num]);
        }

        private void DrawDots()
        {
            foreach (Dot d in _gameField.Dots)
            {
                int x, y;
                UIElement uielem;

                if (d as Fruit == null)
                {
                    uielem = new Ellipse
                    {
                        Fill = Brushes.GreenYellow,
                        Width = d.Width,
                        Height = d.Height
                    };

                    if (d as BigDot == null)
                    {
                        ((Ellipse)uielem).Fill = Brushes.Yellow;
                        y = d.CoordinateY * _gameField.Cols + d.Width / 2;
                        x = d.CoordinateX * _gameField.Rows + d.Height / 2;
                    }
                    else
                    {
                        y = d.CoordinateY * _gameField.Cols + d.Width / 4;
                        x = d.CoordinateX * _gameField.Rows + d.Height / 4;
                    }

                    Canvas.SetTop(uielem, y);
                    Canvas.SetLeft(uielem, x);
                }
                else
                {
                    uielem = new Image
                    {
                        Width = d.Width,
                        Height = d.Height
                    };

                    y = d.CoordinateY * _gameField.Cols + d.Width / 6;
                    x = d.CoordinateX * _gameField.Rows + d.Height / 6;
                    Canvas.SetTop(uielem, d.CoordinateY * _gameField.Cols);
                    Canvas.SetLeft(uielem, d.CoordinateX * _gameField.Rows);
                    ((Image)uielem).Source = _imageUploadService.GetImage(
                        ImagesPathDataBase.GetPathImage(
                            Enum.GetName(typeof(FruitType),
                            (CurrentPlayer.CurrentLevel.Fruit))));
                }

                _coins.Add(uielem);
                _boundChildrens.Add(uielem);
            }
        }

        private void DrawMaze()
        {
            for (var i = 0; i < _gameField.Rows; i++)
            {
                for (var j = 0; j < _gameField.Cols; j++)
                {
                    if (_gameField.MatrixForWalls[i, j] == 1 || 
                        _gameField.MatrixForWalls[i, j] == 2)
                    {
                        var tile = new Rectangle
                        {
                            Width = 20,
                            Height = 20,
                            Stroke = Brushes.DimGray,
                            Fill = Brushes.DarkBlue,
                            StrokeThickness = 2
                        };

                        Canvas.SetLeft(tile, i * _gameField.Rows);
                        Canvas.SetTop(tile, j * _gameField.Cols);

                        _boundChildrens.Add(tile);
                    }
                }
            }
        }
    }
}