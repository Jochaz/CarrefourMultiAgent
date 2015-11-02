using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiAgentSystemPCL
{
    public class FishAgent : ObjectInWorld
    {
        protected const double STEP = 3;
        protected const double DISTANCE_MIN = 5;
        protected const double SQUARE_DISTANCE_MIN = 25;
        //protected const double DISTANCE_MAX = 40;
        protected const double SQUARE_DISTANCE_MAX = 1600;

        protected double speedX;
        public double SpeedX { get { return speedX; } }

        protected double speedY;
        public double SpeedY { get { return speedY; } }

        internal FishAgent(double _x, double _y, double _dir)
        {
            PosX = _x;
            PosY = _y;
            speedX = Math.Cos(_dir);
            speedY = Math.Sin(_dir);
        }

        internal void UpdatePosition()
        {
            PosX += STEP * SpeedX;
            PosY += STEP * SpeedY;
        }

        private bool Near(FishAgent _fish)
        {
            double squareDistance = SquareDistanceTo(_fish);
            return squareDistance < SQUARE_DISTANCE_MAX && squareDistance > SQUARE_DISTANCE_MIN;
        }

        internal double DistanceToWall(double _wallXMin, double _wallYMin, double _wallXMax, double _wallYMax)
        {
            double min = double.MaxValue;
            min = Math.Min(min, PosX - _wallXMin);
            min = Math.Min(min, PosY - _wallYMin);
            min = Math.Min(min, _wallYMax - PosY);
            min = Math.Min(min, _wallXMax - PosX);
            return min;
        }

        internal void ComputeAverageDirection(FishAgent[] _fishList)
        {
            List<FishAgent> fishUsed = _fishList.Where(x => Near(x)).ToList();
            int nbFish = fishUsed.Count;
            if (nbFish >= 1)
            {
                double speedXTotal = 0;
                double speedYTotal = 0;
                foreach (FishAgent neighbour in fishUsed)
                {
                    speedXTotal += neighbour.SpeedX;
                    speedYTotal += neighbour.SpeedY;
                }

                speedX = (speedXTotal / nbFish + speedX) /2;
                speedY = (speedYTotal / nbFish + speedY) /2;
                Normalize();
            }
        }

        protected void Normalize()
        {
            double speedLength = Math.Sqrt(SpeedX * SpeedX + SpeedY * SpeedY);
            speedX /= speedLength;
            speedY /= speedLength;
        }

        internal bool AvoidWalls(double _wallXMin, double _wallYMin, double _wallXMax, double _wallYMax)
        {
            // Stop at walls
            if (PosX < _wallXMin)
            {
                PosX = _wallXMin;
            }
            if (PosY < _wallYMin)
            {
                PosY = _wallYMin;
            }
            if (PosX > _wallXMax)
            {
                PosX = _wallXMax;
            }
            if (PosY > _wallYMax)
            {
                PosY = _wallYMax;
            }

            // Change direction
            double distance = DistanceToWall(_wallXMin, _wallYMin, _wallXMax, _wallYMax);
            if (distance < DISTANCE_MIN)
            {
                if (distance == (PosX - _wallXMin))
                {
                    speedX += 0.3;
                }
                else if (distance == (PosY - _wallYMin))
                {
                    speedY += 0.3;
                }
                else if (distance == (_wallXMax - PosX))
                {
                    speedX -= 0.3;
                }
                else if (distance == (_wallYMax - PosY))
                {
                    speedY -= 0.3;
                }
                Normalize();
                return true;
            }
            return false;
        }

        internal bool AvoidFish(FishAgent _fishAgent)
        {
            double squareDistanceToFish = SquareDistanceTo(_fishAgent);
            if (squareDistanceToFish < SQUARE_DISTANCE_MIN)
            {
                double diffX = (_fishAgent.PosX - PosX) / Math.Sqrt(squareDistanceToFish);
                double diffY = (_fishAgent.PosY - PosY) / Math.Sqrt(squareDistanceToFish); 

                speedX = SpeedX - diffX/4;
                speedY = SpeedY - diffY/4;
                Normalize();
                return true;
            }
            return false;
        }

        internal bool AvoidObstacle(List<BadZone> _obstacles)
        {
            BadZone nearestObstacle = _obstacles.Where(x => SquareDistanceTo(x) < x.Radius*x.Radius).FirstOrDefault(); 

            if (nearestObstacle != null)
            {
                double distanceToObstacle = DistanceTo(nearestObstacle);
                double diffX = (nearestObstacle.PosX - PosX) / distanceToObstacle;
                double diffY = (nearestObstacle.PosY - PosY) / distanceToObstacle;

                speedX = SpeedX - diffX/2;
                speedY = SpeedY - diffY/2;
                Normalize();
                return true;
            }
            return false;
        }

        internal void Update(FishAgent[] _fishList, List<BadZone> _obstacles, double _max_width, double _max_height)
        {
            if (!AvoidWalls(0, 0, _max_width, _max_height))
            {
                if (!AvoidObstacle(_obstacles))
                {
                    double squareDistanceMin = _fishList.Where(x => !x.Equals(this)).Min(x => x.SquareDistanceTo(this));
                    if (!AvoidFish(_fishList.Where(x => x.SquareDistanceTo(this) == squareDistanceMin).FirstOrDefault()))
                    {
                        ComputeAverageDirection(_fishList);
                    }
                }
            }
            UpdatePosition();
        }
    }
}
