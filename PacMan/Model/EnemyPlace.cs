namespace PacMan.Model
{
    public class EnemyPlace
    {
        private int _coordinateX;
        private int _coordinateY;
        private int _width;
        private int _height;

        public EnemyPlace()
        {
            _coordinateX = 8;
            _coordinateY = 8;
            _width = 4;
            _height = 4;
        }

        public EnemyPlace(int coordX, int coordY, int width, int height)
        {
            _coordinateX = coordX;
            _coordinateY = coordY;
            _width = width;
            _height = height;
        }

        public int CoordinateX { get { return _coordinateX; } }

        public int CoordinateY { get { return _coordinateY; } }

        public int Width { get { return _width; } }

        public int Height { get { return _height; } }

        public bool Contains(Enemy enemy)
        {
            Point p = enemy.CurrentPoint;

            if (p.CoordinateX > CoordinateX && p.CoordinateY 
                >= CoordinateY && p.CoordinateX < CoordinateX
                + Width && p.CoordinateY < CoordinateY + Height)
                return true;
            else return false;
        }
    }
}
