namespace PacMan.Model
{
    public class Point
    {
        private int _coordinateX;
        private int _coordinateY;

        public Point(int coordX, int coordY)
        {
            _coordinateX = coordX;
            _coordinateY = coordY;
        }

        public int CoordinateX { get { return _coordinateX; } set { _coordinateX = value; } }

        public int CoordinateY { get { return _coordinateY; } set { _coordinateY = value; } }

        public static bool operator !=(Point left, Point right)
        {
            if ((object)right == null)
            {
                if ((object)left == null)
                {
                    return false;
                }
                return true;               
            }
            return !left.Equals(right);
        }

        public static bool operator ==(Point left, Point right)
        {
            if ((object)right == null)
            {
                if ((object)left == null)
                {
                    return true;
                }
                return false;
            }
            return left.Equals(right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Point p = obj as Point;

            if (p == null)
            {
                return false;
            }

            return (CoordinateX == p.CoordinateX) && (CoordinateY == p.CoordinateY);
        }

        public bool Equals(Point p)
        {
            if (p == null)
            {
                return false;
            }

            return (CoordinateX == p.CoordinateX) && (CoordinateY == p.CoordinateY);
        }

        public override int GetHashCode()
        {
            return CoordinateX.GetHashCode() * CoordinateY.GetHashCode();
        }
    } 
}
