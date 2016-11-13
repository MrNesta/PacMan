namespace PacMan.Model
{
    public class Fruit: Dot
    {
        private FruitType _type;

        public Fruit(int coordX, int coordY, int width, int heigth, FruitType type)
            : base(coordX, coordY, width, heigth)
        {
            _type = type;
        }      

        public FruitType Type { get { return _type; } }
    }
}
