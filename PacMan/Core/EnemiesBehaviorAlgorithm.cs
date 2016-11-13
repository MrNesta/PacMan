using System;
using System.Collections.Generic;
using System.ComponentModel;
using PacMan.Model;

namespace PacMan.Core
{
    public abstract class EnemiesBehaviorAlgorithm : IPluginEnemyBehaviorAlgorithm
    {
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

        protected List<Point> GetNextPossibleCoordinates(Route route, Point currentPoint, Enemy enemy)
        {
            int x = currentPoint.CoordinateX;
            int y = currentPoint.CoordinateY;

            var points = new List<Point>();
            Point pointCheck;

            switch (route)
            {
                case Route.Right:
                    pointCheck = new Point(x + 1, y);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x, y - 1);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x, y + 1);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    break;

                case Route.Left:
                    pointCheck = new Point(x - 1, y);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x, y - 1);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x, y + 1);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    break;

                case Route.Bottom:
                    pointCheck = new Point(x + 1, y);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x, y + 1);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x - 1, y);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    break;

                case Route.Top:
                    pointCheck = new Point(x + 1, y);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x - 1, y);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    pointCheck = new Point(x, y - 1);
                    if (enemy.Field.CanEnemyMove(pointCheck)) points.Add(pointCheck);

                    break;
            }

            return points;
        }

        protected List<Point> GetAllPossibleCoordinates(Point currentPoint, Enemy enemy)
        {
            int x = currentPoint.CoordinateX;
            int y = currentPoint.CoordinateY;

            var pts = new List<Point>();
            Point pntCheck;

            pntCheck = new Point(x + 1, y);
            if (enemy.Field.CanEnemyMove(pntCheck)) pts.Add(pntCheck);

            pntCheck = new Point(x - 1, y);
            if (enemy.Field.CanEnemyMove(pntCheck)) pts.Add(pntCheck);

            pntCheck = new Point(x, y - 1);
            if (enemy.Field.CanEnemyMove(pntCheck)) pts.Add(pntCheck);

            pntCheck = new Point(x, y + 1);
            if (enemy.Field.CanEnemyMove(pntCheck)) pts.Add(pntCheck);

            return pts;
        }

        protected Point FindNearestPoint(List<Point> points, Enemy enemy)
        {
            Point nearestPoint = null;
            int distance, minDistance = 1000;

            foreach (Point p in points)
            {
                distance = FindDistance(p, enemy.TargetPoint);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPoint = p;
                }
            }

            return nearestPoint;
        }

        protected Point FindNearestPointNotPrevious(List<Point> points, Enemy enemy)
        {
            Point nearestPoint = null;
            int distance, minDistance = 1000;

            foreach (Point p in points)
            {
                if (p != enemy.PreviousPoint)
                {
                    distance = FindDistance(p, enemy.TargetPoint);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPoint = p;
                    }
                }
            }

            return nearestPoint;
        }

        protected void GetRoute(Point point, Enemy enemy)
        {
            if (enemy.CurrentPoint.CoordinateY < point.CoordinateY)
                enemy.AttemptedRoute = Route.Bottom;
            else if (enemy.CurrentPoint.CoordinateX < point.CoordinateX)
                enemy.AttemptedRoute = Route.Right;
            else if (enemy.CurrentPoint.CoordinateX > point.CoordinateX)
                enemy.AttemptedRoute = Route.Left;
            else if (enemy.CurrentPoint.CoordinateY > point.CoordinateY)
                enemy.AttemptedRoute = Route.Top;
        }

        protected int FindDistance(Point point, Point targetPoint)
        {
            return (int)Math.Sqrt((targetPoint.CoordinateX - point.CoordinateX) 
                * (targetPoint.CoordinateX - point.CoordinateX) 
                + (targetPoint.CoordinateY - point.CoordinateY) 
                * (targetPoint.CoordinateY - point.CoordinateY));
        }

        protected Point GetNextPoint(Route route, Point point, Enemy enemy, int steps)
        {
            Point nextPoint = point;

            for (var i = 0; i < steps; i++)
            {
                nextPoint = enemy.GetNextPoint(route, nextPoint);
            }

            return nextPoint;
        }

        protected void MoveToTargetPoint(object objectToMove, Point targetPoint)
        {
            Enemy enemy = objectToMove as Enemy;

            if (enemy != null)
            {
                enemy.TargetPoint = targetPoint;

                GetRoute(enemy.TargetPoint, enemy);

                Point nextPoint = enemy.GetNextPoint(enemy.AttemptedRoute, enemy.CurrentPoint);

                if (enemy.AttemptedRoute != Route.None && enemy.Field.CanEnemyMove(nextPoint))
                {
                    enemy.MoveOnNextPoint(enemy.AttemptedRoute);
                }
                else
                {
                    List<Point> points = GetNextPossibleCoordinates(enemy.AttemptedRoute, enemy.CurrentPoint, enemy);
                    Point nearestPoint = FindNearestPoint(points, enemy);

                    if (nearestPoint != null && nearestPoint != enemy.PreviousPoint)
                    {
                        GetRoute(nearestPoint, enemy);
                        enemy.MoveOnNextPoint(enemy.AttemptedRoute);
                    }
                    else
                    {
                        points = GetAllPossibleCoordinates(enemy.CurrentPoint, enemy);
                        nearestPoint = FindNearestPointNotPrevious(points, enemy);

                        if (nearestPoint != null)
                        {
                            GetRoute(nearestPoint, enemy);

                            enemy.MoveOnNextPoint(enemy.AttemptedRoute);
                        }
                        else if (points.Count == 1 && points[0] == enemy.PreviousPoint)
                        {
                            GetRoute((Point)points[0], enemy);
                            enemy.MoveOnNextPoint(enemy.AttemptedRoute);
                        }
                    }
                }
            }
        }

        public abstract void Chase(object objectToMove);

        public virtual void Frightened(object objectToMove)
        {
            Enemy enemy = objectToMove as Enemy;
            if (enemy != null)
            {
                List<Point> points = GetAllPossibleCoordinates(enemy.CurrentPoint, enemy);

                int iSelected = enemy.Field.GetRandomInt(1, points.Count + 1);

                Point SelectedPoint = (Point)points[iSelected - 1];

                MoveToTargetPoint(enemy, SelectedPoint);
            }
        }

        public abstract void Scatter(object objectToMove);
    }


    public class BlinkyBehaviorAlgorithm : EnemiesBehaviorAlgorithm, IPluginEnemyBehaviorAlgorithm
    {
        public override void Chase(object objectToMove)
        {
            Enemy enemy = objectToMove as Enemy;

            if (enemy != null)
            {
                MoveToTargetPoint(enemy, enemy.Field.Pacman.CurrentPoint);
            }
        }

        public override void Scatter(object objectToMove)
        {
            MoveToTargetPoint(objectToMove, new Point(20, -1));
        }
    }

    public class PinkyBehaviorAlgorithm : EnemiesBehaviorAlgorithm, IPluginEnemyBehaviorAlgorithm
    {
        public override void Chase(object objectToMove)
        {
            Enemy enemy = objectToMove as Enemy;
            if (enemy != null)
            {
                Point pacmanFuturePoint = GetNextPoint(enemy.Field.Pacman.Route, 
                    enemy.Field.Pacman.CurrentPoint, enemy, 4);

                MoveToTargetPoint(enemy, pacmanFuturePoint);
            }
        }

        public override void Scatter(object objectToMove)
        {
            MoveToTargetPoint(objectToMove, new Point(2, -1));
        }
    }


    public class InkyBehaviorAlgorithm : EnemiesBehaviorAlgorithm, IPluginEnemyBehaviorAlgorithm
    {
        public override void Chase(object objectToMove)
        {
            Enemy enemy = objectToMove as Enemy;

            if (enemy != null)
            {
                Point pacmanFuturePoint = GetNextPoint(enemy.Field.Pacman.Route, 
                    enemy.Field.Pacman.CurrentPoint, enemy, 2);

                int newX, newY;

                int x = enemy.Field.Blinky.CurrentPoint.CoordinateX - pacmanFuturePoint.CoordinateX;
                int y = enemy.Field.Blinky.CurrentPoint.CoordinateY - pacmanFuturePoint.CoordinateY;

                if (enemy.Field.Blinky.CurrentPoint.CoordinateX < pacmanFuturePoint.CoordinateX)
                {
                    newX = pacmanFuturePoint.CoordinateX + x;
                }
                else
                {
                    newX = pacmanFuturePoint.CoordinateX - x;
                }

                if (enemy.Field.Blinky.CurrentPoint.CoordinateY < pacmanFuturePoint.CoordinateY)
                {
                    newY = pacmanFuturePoint.CoordinateY + y;
                }
                else
                {
                    newY = pacmanFuturePoint.CoordinateY - y;
                }

                MoveToTargetPoint(enemy, new Point(newX, newY));
            }
        }

        public override void Scatter(object objectToMove)
        {
            MoveToTargetPoint(objectToMove, new Point(20, 21));
        }
    }


    public class ClydeBehaviorAlgorithm : EnemiesBehaviorAlgorithm, IPluginEnemyBehaviorAlgorithm
    {
        public override void Chase(object objectToMove)
        {
            Enemy enemy = objectToMove as Enemy;

            if (enemy != null)
            {
                int distance = FindDistance(enemy.CurrentPoint, enemy.Field.Pacman.CurrentPoint);

                if (distance > 8)
                {
                    MoveToTargetPoint(enemy, enemy.Field.Pacman.CurrentPoint);
                }
                else
                {
                    MoveToTargetPoint(enemy, new Point(0, 21));
                }
            }
        }

        public override void Scatter(object objectToMove)
        {
            MoveToTargetPoint(objectToMove, new Point(0, 21));
        }
    }
}
