namespace PacMan.Model
{
    public class Dot : Point
    {
        private int _height = 7;
        private int _width = 7;

        public Dot(int coordX, int coordY)
            : base(coordX, coordY) { }
        public Dot(int coordX, int coordY, int dotWidth, int dotHeight)
            : base(coordX, coordY)
        {
            _height = dotHeight;
            _width = dotWidth;
        }

        public int Height { get { return _height; } }

        public int Width { get { return _width; } }
    }


    public class BigDot : Dot
    {
        public BigDot(int coordX, int coordY, int dotWidth, int dotHeight)
            : base(coordX, coordY, dotWidth, dotHeight) { }
    }
}