using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PacMan.Model
{
    public enum FruitType
    {
        Cherry,
        Strawberry,
        Orange,
        Apple,
        Melon
    }

    public class Level : INotifyPropertyChanged
    {
        private FruitType _fruit;
        private int _levelNum;       

        public Level(int LevelNumber, FruitType Fruit)
        {
            _levelNum = LevelNumber;
            _fruit = Fruit;
        }

        public int LevelNumber
        {
            get
            {
                return _levelNum;
            }
            set
            {
                _levelNum = value;
                NotifyPropertyChanged("LevelNumber");
            }
        }

        public FruitType Fruit { get { return _fruit; } set { _fruit = value; } }

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


    public class LevelsList
    {
        public static List<Level> GetLevels()
        {
            var levels = new List<Level>
            {
                new Level(1, FruitType.Cherry),
                new Level(2, FruitType.Strawberry),
                new Level(3, FruitType.Orange),
                new Level(4, FruitType.Melon),
                new Level(5, FruitType.Apple),
                new Level(6, FruitType.Melon),
                new Level(7, FruitType.Cherry),
                new Level(8, FruitType.Orange),
                new Level(9, FruitType.Melon),
                new Level(10, FruitType.Orange),
                new Level(11, FruitType.Strawberry),
                new Level(12, FruitType.Apple),
                new Level(13, FruitType.Strawberry),
                new Level(14, FruitType.Cherry),
                new Level(15, FruitType.Orange)
           };

            return levels;
        }
    }
}
