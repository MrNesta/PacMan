using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PacMan.Model
{
    public enum CharacterState
    {
        Moving = 0,
        Stopped = 1,
    }

    public enum Route
    {
        Left = 1,
        Right = 2,
        Top = 3,
        Bottom = 4,
        None = 5
    }

    public abstract class Character: MarshalByRefObject, INotifyPropertyChanged
    {
        protected Point _currentPoint;
        protected Point _previousPoint;
        protected Point _startingPoint;
        protected int _width;
        protected int _height;
        protected Route _route;
        protected Route _attemptedRoute;
        protected Route _previousRoute;
        protected bool _visible;

        public CharacterState _currentState;

        public GameField Field { get; set; }

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

        public int Width { get { return _width; } }

        public int Height { get { return _height; }
        }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                NotifyPropertyChanged("Visible");
            }
        }

        public Route Route
        {
            get { return _route; }
            set { _route = value; }
        }

        public Route AttemptedRoute
        {
            get { return _attemptedRoute; }
            set { _attemptedRoute = value; }
        }

        public Route PreviousRoute
        {
            get { return _previousRoute; }
            set { _previousRoute = value; }
        }

        public Point StartingPoint { get { return _startingPoint; } }

        public Point CurrentPoint { get { return _currentPoint; } }

        public Point PreviousPoint { get { return _previousPoint; } }

        public Character(Point startingPoint, int width, int height, GameField field)
        {
            _previousPoint = _currentPoint = _startingPoint = startingPoint;
            _width = width;
            _height = height;
            Field = field;
            _currentState = CharacterState.Stopped;
            _route = _attemptedRoute = _previousRoute = Route.Left;
            _visible = true;
        }

        public virtual void Move()
        {
            _currentState = CharacterState.Stopped;

            if (CurrentPoint.CoordinateX == Field.Cols - 1 && Route == Route.Right)
            {
                Field.RemoveDots(new Dot(CurrentPoint.CoordinateX, CurrentPoint.CoordinateY));
                CurrentPoint.CoordinateX = 0;
                Field.RemoveDots(new Dot(CurrentPoint.CoordinateX, CurrentPoint.CoordinateY));
            }
            else if (CurrentPoint.CoordinateX == 0 && Route == Route.Left)
            {
                Field.RemoveDots(new Dot(CurrentPoint.CoordinateX, CurrentPoint.CoordinateY));
                CurrentPoint.CoordinateX = Field.Cols - 1;
                Field.RemoveDots(new Dot(CurrentPoint.CoordinateX, CurrentPoint.CoordinateY));
            }

            if (AttemptedRoute != Route.None && Field.CanMove(GetNextPoint(AttemptedRoute, CurrentPoint)))
            {
                Field.RemoveDots(new Dot(CurrentPoint.CoordinateX, CurrentPoint.CoordinateY));
                MoveOnNextPoint(AttemptedRoute);
            }
            else
            {
                if (Route != Route.None && Field.CanMove(GetNextPoint(Route, CurrentPoint)))
                {
                    Field.RemoveDots(new Dot(CurrentPoint.CoordinateX, CurrentPoint.CoordinateY));
                    MoveOnNextPoint(Route);
                }
                else
                {
                    PreviousRoute = Route;
                    Route = Route.None;
                    _currentState = CharacterState.Stopped;
                }
            }
        }

        public void MoveOnNextPoint(Route route)
        {
            Point nextPoint = GetNextPoint(route, CurrentPoint);
            _previousPoint = _currentPoint;
            _currentPoint = nextPoint;

            _propertyChanged(this, new PropertyChangedEventArgs("currentPoint"));

            if (AttemptedRoute == route)
            {
                PreviousRoute = Route;

                Route = AttemptedRoute;

                AttemptedRoute = Route.None;

                _currentState = CharacterState.Moving;
            }
            else
            {
                PreviousRoute = Route;
                _currentState = CharacterState.Moving;
            }

            return;
        }

        public void MoveToStartingPoint() => _currentPoint = _startingPoint;

        public Point GetNextPoint(Route route, Point currentPoint)
        {
            int x = currentPoint.CoordinateX;
            int y = currentPoint.CoordinateY;

            switch (route)
            {
                case Route.Right:
                    x++;
                    break;
                case Route.Left:
                    x--;
                    break;
                case Route.Bottom:
                    y++;
                    break;
                case Route.Top:
                    y--;
                    break;
            }

            return new Point(x, y);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
