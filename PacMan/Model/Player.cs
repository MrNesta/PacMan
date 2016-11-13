using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PacMan.Model
{
    public class Player : INotifyPropertyChanged
    {
        private int _score;
        private int _lives;
        private bool _gameOver;
        private Level _curentLevel;
       
        private Player()
        {

        }

        public Player(string name)
        {           
            Name = name;
            _score = 0;
            _lives = 3;
            _gameOver = false;
            CurrentLevel = LevelsList.GetLevels()[0];
        }

        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int Score
        {
            get { return _score; }
            set
            {
                if (value >= 0)
                {
                    _score = value;
                    NotifyPropertyChanged("Score");
                }
            }
        }

        [NotMapped]
        public int Lives
        {
            get { return _lives; }
            set
            {
                if (value >= 0)
                {
                    if (value < _lives)
                    {
                        if (value == 0)
                        {
                            GameOver = true;
                        }
                        else
                        {
                            NotifyPropertyChanged("LivesMinus");
                        }
                    }

                    _lives = value;
                    NotifyPropertyChanged("Lives");
                }                     
            }
        }

        [NotMapped]
        public Level CurrentLevel
        {
            get { return _curentLevel; }
            set
            {
                _curentLevel = value;
                NotifyPropertyChanged("CurrentLevel");
            }
        }

        [NotMapped]
        public bool GameOver
        {
            get { return _gameOver; }
            set
            {
                _gameOver = value;
                if (_gameOver)
                {
                    NotifyPropertyChanged("GameOver");
                }
            }
        }

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

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
