using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using PacMan.Core;
using PacMan.Interfaces;

namespace PacMan.Model
{
    public class GameField : IGameField
    {
        private int _rows;
        private int _cols;
        private byte[,] _maze;
        private List<Dot> _dots;
        private List<Point> _way;
        private List<Point> _enemyWay;
        private EnemyPlace _enemyPlace;
        private int _inTheEnemyPlace;
        private Pac _pacman;
        private Enemy _inky;
        private Enemy _pinky;
        private Enemy _blinky;
        private Enemy _clyde;
        private List<Level> _levels;
        private Random _rnd = new Random();
        private Player _currentPlayer;

        public GameField(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
            _levels = LevelsList.GetLevels();
            _dots = new List<Dot>();
            _maze = new byte[rows, cols];
            _enemyPlace = new EnemyPlace();
            _inTheEnemyPlace = 3;
            IsFrighteningMode = false;
        }

        public Player CurrentPlayer
        {
            get
            {
                return _currentPlayer;
            }
            set
            {
                _currentPlayer = value;
                CurrentPlayer.PropertyChanged += CurrentPlayerPropertyChanged;
            }
        }

        public byte[,] MatrixForWalls { get { return _maze; } }

        public Enemy Inky { get { return _inky; } }

        public Enemy Pinky { get { return _pinky; } }

        public Enemy Blinky { get { return _blinky; } }

        public Enemy Clyde { get { return _clyde; } }

        public Pac Pacman { get { return _pacman; } }

        public EnemyPlace EnemyPlace { get { return _enemyPlace; } }

        public int Rows { get { return _rows; } }

        public int Cols { get { return _cols; } }

        public List<Dot> Dots { get { return _dots; } }

        public List<Point> Way { get { return _way; } }

        public List<Point> EnemyWay { get { return _enemyWay; } }

        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _propertyChanged -= value;
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }

        public int InTheEnemyPlace
        {
            get
            {
                return _inTheEnemyPlace;
            }
            set
            {
                if (value <= 3 && value >= 0)
                    _inTheEnemyPlace = value;
            }
        }
       

        public bool IsFrighteningMode { get; set; }
      

        private void CharacterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _propertyChanged(sender, e);
        }

        private void CurrentPlayerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _propertyChanged(sender, e);
        }

        public void Add(Dot dot)
        {
            if (_dots.Find(x => x.CoordinateY == dot.CoordinateY
            && x.CoordinateX == dot.CoordinateX) == null)
            {
                _dots.Add(dot);
            }
        }

        public void RemoveDots(Dot d)
        {
            if (_dots.Count > 0)
            {
                Dot testdot = _dots.Find(x => x.CoordinateY == d.CoordinateY
                && x.CoordinateX == d.CoordinateX);
                if (testdot != null)
                {
                    if (testdot as BigDot == null)
                    {
                        if (testdot as Fruit != null)
                        {
                            CurrentPlayer.Lives += 1;
                        }
                        else
                            CurrentPlayer.Score += 10;
                    }
                    else
                    {
                        CurrentPlayer.Score += 20;
                        _propertyChanged(this, new PropertyChangedEventArgs("Mode"));
                    }

                    _dots.Remove(testdot);
                }
            }
            else
            {
                CurrentPlayer.CurrentLevel = NextLevel();
                _propertyChanged(this, new PropertyChangedEventArgs("Level"));
            }
        }

        private Enemy GetPacmanCollidedEnemy(params Enemy[] enemies)
        {
            Point currPoint = Pacman.CurrentPoint;
            Point prevPoint = Pacman.PreviousPoint;

            foreach (var enemy in enemies)
            {
                if (enemy.Visible)
                {
                    if (enemy.CurrentPoint == currPoint || (enemy.PreviousPoint == currPoint && 
                        enemy.CurrentPoint == prevPoint))
                    {
                        return enemy;
                    }                   
                }
            }
                return null;
        }

        public int GetRandomInt(int min, int max)
        {
            return _rnd.Next(min, max);
        }

        private void CollidedCheck()
        {
            Enemy enemy = GetPacmanCollidedEnemy(_blinky, _inky,
                _pinky, _clyde);
            if (enemy != null)
            {
                if (IsFrighteningMode)
                {
                    enemy.Visible = false;
                    CurrentPlayer.Score += 200;
                }
                else
                {
                    if (CurrentPlayer.Lives > 0)
                        CurrentPlayer.Lives--;
                    Thread.Sleep(1000);
                }
            }
        }

        public void GenerateWalls()
        {
            Array.Clear(_maze, 0, _maze.Length);

            for (var i = 0; i < _rows / 2 + 1; i += 2)
            {
                for (var j = i; j < _cols - i; j++)
                {
                    _maze[i, j] = _maze[_rows - 1 - i, j] = _maze[j, i] 
                        = _maze[j, _cols - i - 1]  = 1;

                    if ((i == j) || (i == _cols - j - 1)
                        || (i == 0) || (i == _rows - 1))
                        _maze[i, j] = _maze[_rows - i - 1, j]
                            = _maze[_rows - i - 1, _cols - j - 1] = 2;
                }
            }

            for (var i = 0; i < _rows; i++)
            {
                _maze[i, 0] = _maze[i, _cols - 1]  = 2;
            }

            _maze[8, 9] = _maze[8, 10] = _maze[10, 11] = _maze[9, 11]
                = _maze[11, 9] = _maze[11, 10] = 2;
            _maze[9, 9] = _maze[10, 10]  = _maze[9, 10] = _maze[10, 9]
                = _maze[9, 8] = _maze[10, 8]  = 3;
            _maze[0, 10] = _maze[_cols - 1, 10]  = 0;
        }

        public void GenerateMaze()
        {
            int count = GetRandomInt(30, 40);
            int i, j;

            while (count > 0)
            {
                i = GetRandomInt(0, _rows);
                j = GetRandomInt(0, _cols);

                if (_maze[i, j] == 1)
                {
                    _maze[i, j] = 0;

                    if (_maze[i + 1, j] == 1) _maze[i + 1, j] = 2;
                    if (_maze[i, j + 1] == 1) _maze[i, j + 1] = 2;
                    if (_maze[i - 1, j] == 1) _maze[i - 1, j] = 2;
                    if (_maze[i, j - 1] == 1) _maze[i, j - 1] = 2;

                    count--;
                }
            }
        }

        public void GenerateDots()
        {
            _dots.Clear();

            for (var i = 0; i < _rows; i++)
                for (var j = 0; j < _cols; j++)
                {
                    if (_maze[i, j] == 0)
                        _dots.Add(new Dot(i, j));
                }

            AddBigDots();
            AddFruits();
        }

        private void AddBigDots()
        {
            var index = 0;

            for (var i = 0; i < 4; i++)
            {
                index = GetRandomInt(i, _dots.Count);
                Dot dot = Dots[index];
                Dots[index] = new BigDot(dot.CoordinateX, dot.CoordinateY, 15, 15);
            }
        }

        private void AddFruits()
        {
            var index = 0;
            var numFruit = CurrentPlayer.CurrentLevel.LevelNumber < 7 ? 2 : 4;

            for (var i = 0; i < numFruit; i++)
            {
                index = GetRandomInt(i, _dots.Count);
                Dot dot = Dots[index];
                if (dot as BigDot == null)
                    Dots[index] = new Fruit(dot.CoordinateX, dot.CoordinateY,
                        15, 15, CurrentPlayer.CurrentLevel.Fruit);
                else i--;
            }
        }

        public void InitializeCharacters(IPluginEnemyBehaviorAlgorithm algorithm = null)
        {
            Point startPoint = _way[GetRandomInt(1, _way.Count)];

            RemoveCharacterPropertyChanged(_pacman, Inky, Pinky, Blinky, Clyde);

            _pacman = new Pac(this, startPoint, 20, 20);
            _inky = new Enemy(new Point(_enemyPlace.CoordinateX + 2,
                _enemyPlace.CoordinateY + 1), 20, 20, this);
            _pinky = new Enemy(new Point(_enemyPlace.CoordinateX + 2,
                _enemyPlace.CoordinateY + 2), 20, 20, this);
            _blinky = new Enemy(new Point(_enemyPlace.CoordinateX + 1,
                _enemyPlace.CoordinateY - 1), 20, 20, this);
            _clyde = new Enemy(new Point(_enemyPlace.CoordinateX + 1,
                _enemyPlace.CoordinateY + 1), 20, 20, this);

            _pacman.PropertyChanged += CharacterPropertyChanged;
            Inky.PropertyChanged += CharacterPropertyChanged;
            Pinky.PropertyChanged += CharacterPropertyChanged;
            Blinky.PropertyChanged += CharacterPropertyChanged;
            Clyde.PropertyChanged += CharacterPropertyChanged;

            if (algorithm == null)
            {
                _inky.Algorithm = new InkyBehaviorAlgorithm();
                _pinky.Algorithm = new PinkyBehaviorAlgorithm();
                _blinky.Algorithm = new BlinkyBehaviorAlgorithm();
                _clyde.Algorithm = new ClydeBehaviorAlgorithm();
            }
            else
            {
                _inky.Algorithm = algorithm;
                _pinky.Algorithm = algorithm;
                _blinky.Algorithm = algorithm;
                _clyde.Algorithm = algorithm;
            }

            ScatteringMode();
        }

        private void RemoveCharacterPropertyChanged(params Character[] characters)
        {
            foreach (var character in characters)
            {
                if (character != null)
                {
                    character.PropertyChanged -= CharacterPropertyChanged;
                }
            }
        }

        public void InitializeWay()
        {
            var dotscount = _dots.Count;
            _way = new List<Point>(dotscount);

            for (var i = 0; i < dotscount; i++)
            {
                _way.Add(_dots[i]);
            }
        }

        public void InitializeEnemyWay()
        {
            _enemyWay = new List<Point>();

            for (var i = 0; i < _rows; i++)
            {
                for (var j = 0; j < _cols; j++)
                {
                    if (_maze[i, j] == 0 || _maze[i, j] == 3)
                        _enemyWay.Add(new Point(i, j));
                }
            }
        }

        public bool CanMove(Point p)
        {
            return _way.Find(x => x.CoordinateX == p.CoordinateX
            && x.CoordinateY == p.CoordinateY) != null;
        }

        public bool CanMove(Point p, Route route)
        {
            int coordx = p.CoordinateX;
            int coordy = p.CoordinateY;

            switch (route)
            {
                case Route.Left:
                    {
                        coordx--;
                        break;
                    }
                case Route.Right:
                    {
                        coordx++;
                        break;
                    }
                case Route.Top:
                    {
                        coordy--;
                        break;
                    }
                case Route.Bottom:
                    {
                        coordy++;
                        break;
                    }
            }

            return CanMove(new Point(coordx, coordy));
        }

        public bool CanEnemyMove(Point pointCheck)
        {
            return _enemyWay.Find(x => x.CoordinateX == pointCheck.CoordinateX &&
            x.CoordinateY == pointCheck.CoordinateY) != null;
        }

        public void CloseEnemyPlace()
        {
            _enemyWay = _way;
        }

        public void ChasingMode(params Enemy[] enemies)
        {
            IsFrighteningMode = false;
            Inky.Visible = Pinky.Visible = Blinky.Visible = Clyde.Visible = true;

            if (enemies.Length == 0)
            {
                Inky.CurrentMode = EnemyModes.Chasing;
                Blinky.CurrentMode = EnemyModes.Chasing;
                Pinky.CurrentMode = EnemyModes.Chasing;
                Clyde.CurrentMode = EnemyModes.Chasing;
            }
            else
            {
                foreach (var enemy in enemies)
                {
                    enemy.CurrentMode = EnemyModes.Chasing;
                }
            }
        }

        public void ScatteringMode(params Enemy[] enemies)
        {
            if (enemies.Length == 0)
            {
                Inky.CurrentMode = EnemyModes.Scattering;
                Inky._currentState = CharacterState.Stopped;
                Blinky.CurrentMode = EnemyModes.Scattering;
                Blinky._currentState = CharacterState.Stopped;
                Pinky.CurrentMode = EnemyModes.Scattering;
                Pinky._currentState = CharacterState.Stopped;
                Clyde.CurrentMode = EnemyModes.Scattering;
                Clyde._currentState = CharacterState.Stopped;
            }
            else
            {
                foreach (var enemy in enemies)
                {
                    enemy.CurrentMode = EnemyModes.Scattering;
                    enemy._currentState = CharacterState.Stopped;
                }
            }
        }

        public void FrighteningMode(params Enemy[] enemies)
        {
            IsFrighteningMode = true;
            Inky.CurrentMode = EnemyModes.Frightening;
            Blinky.CurrentMode = EnemyModes.Frightening;
            Pinky.CurrentMode = EnemyModes.Frightening;
            Clyde.CurrentMode = EnemyModes.Frightening;
        }

        public Level NextLevel()
        {
            Level level = null;

            int levelNumber = CurrentPlayer.CurrentLevel.LevelNumber;
            levelNumber++;

            if (levelNumber >= _levels.Count)
            {
                level = _levels[_levels.Count - 1];
            }
            else
            {
                level = _levels[levelNumber];
            }
            level.LevelNumber = levelNumber;

            return level;
        }

        public void MoveCharacters()
        {
            CollidedCheck();
            Pacman.Move();
            Blinky.Move();
            Inky.Move();
            if (InTheEnemyPlace <= 2)
                Pinky.Move();
            if (InTheEnemyPlace <= 1)
                Clyde.Move();
        }

        public void ResetCharacter()
        {
            Pacman.MoveToStartingPoint();
            Inky.MoveToStartingPoint();
            Pinky.MoveToStartingPoint();
            Blinky.MoveToStartingPoint();
            Clyde.MoveToStartingPoint();
            _inTheEnemyPlace = 3;
            ScatteringMode();
        }
    }
}
