namespace PacMan.Model
{
    public enum EnemyModes
    {
        Chasing = 1,
        Scattering = 2,
        Frightening = 3
    }


    public class Enemy : Character
    {
        public Enemy(Point startingPoint, int width, int height, GameField field)
            : base(startingPoint, width, height, field)
        {

        }

        public Point TargetPoint { get; set; }

        public EnemyModes CurrentMode { get; set; }

        public IPluginEnemyBehaviorAlgorithm Algorithm { get; set; }

        public override void Move()
        {
            if (InTheEnemyPlace())
            {
                GetOut();
                return;
            }

            if (CurrentPoint.CoordinateX == Field.Cols - 1 && Route == Route.Right)
            {
                CurrentPoint.CoordinateX = 0;
            }
            else if (CurrentPoint.CoordinateX == 0 && Route == Route.Left)
            {
                CurrentPoint.CoordinateX = Field.Cols - 1;
            }

            if (CurrentMode == EnemyModes.Chasing)
            {
                Algorithm.Chase(this);
                return;
            }
            else if (CurrentMode == EnemyModes.Scattering)
            {
                Algorithm.Scatter(this);
                return;
            }
            else if (CurrentMode == EnemyModes.Frightening)
            {
                Algorithm.Frightened(this);
                return;
            }
        }

        private void GetOut()
        {
            Route = Route.Top;
            MoveOnNextPoint(Route);

            if (!InTheEnemyPlace())
            {
                Field.InTheEnemyPlace--;

                if (Field.InTheEnemyPlace == 0)
                {
                    Field.CloseEnemyPlace();
                }
            }
        }

        private bool InTheEnemyPlace()
        {
            return Field.EnemyPlace.Contains(this);
        }
    }
}
