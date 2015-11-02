using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiAgentSystemPCL
{
    public delegate void OceanUpdated(FishAgent[] _fish, List<BadZone> _obstacles);

    public class Ocean
    {
        public event OceanUpdated oceanUpdatedEvent;

        FishAgent[] fishList = null;
        List<BadZone> obstacles = null;

        Random randomGenerator;

        protected double MAX_WIDTH;
        protected double MAX_HEIGHT;

        public Ocean(int _fishNb, double _width, double _height)
        {
            MAX_WIDTH = _width;
            MAX_HEIGHT = _height;
            randomGenerator = new Random();

            fishList = new FishAgent[_fishNb];
            obstacles = new List<BadZone>();
            for (int i = 0; i < _fishNb; i++)
            {
                fishList[i] = new FishAgent(randomGenerator.NextDouble() * MAX_WIDTH, randomGenerator.NextDouble() * MAX_HEIGHT, randomGenerator.NextDouble() * 2 * Math.PI);
            }
        }

        public void UpdateEnvironnement()
        {
            UpdateObstacles();
            UpdateFish();
            if (oceanUpdatedEvent != null)
            {
                oceanUpdatedEvent(fishList, obstacles);
            }
        }

        private void UpdateObstacles()
        {
            foreach (BadZone obstacle in obstacles)
            {
                obstacle.Update();
            }
            obstacles.RemoveAll(x => x.Dead());
        }

        private void UpdateFish()
        {
            foreach (FishAgent fish in fishList)
            {
                fish.Update(fishList, obstacles, MAX_WIDTH, MAX_HEIGHT);
            }
        }

        public void AddObstacle(double _posX, double _posY, double _radius) {
            obstacles.Add(new BadZone(_posX, _posY, _radius));
        }
    }
}
