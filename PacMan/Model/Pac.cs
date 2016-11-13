namespace PacMan.Model
{
    public class Pac : Character
    {
        public Pac(GameField field, Point currentPoint, int width, int height)
            : base(currentPoint, width, height, field) { }

        public override void Move()
        {
            base.Move();
        }
    }
}
