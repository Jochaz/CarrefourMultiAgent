using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiAgentSystemPCL.TP;

namespace MultiAgentSystemPCL
{
    //public delegate void RouteUpdated(VoitureAgent[] _voiture, List<BadZone> _obstacles);
    class Route
    {
        
        //public event RouteUpdated routeUpdatedEvent;

        VoitureAgent[] VoitureList = null;
        List<BadZone> obstacles = null;

        protected double MAX_WIDTH;
        protected double MAX_HEIGHT;
        Random randomGenerator;
 

        public Route(int _voitureNb, double _width, double _height)
        {
            MAX_WIDTH = _width;
            MAX_HEIGHT = _height;
            randomGenerator = new Random();

            VoitureList = new VoitureAgent[_voitureNb];
            obstacles = new List<BadZone>();
            for (int i = 0; i < _voitureNb; i++)
            {
                // VoitureList[i] = new VoitureAgent(randomGenerator.NextDouble() * MAX_WIDTH, randomGenerator.NextDouble() * MAX_HEIGHT, randomGenerator.NextDouble() * 2 * Math.PI);
            }
        }
        public void UpdateEnvironnement()
        {
           // UpdateObstacles();
           // UpdateFish();
          //  if (routeUpdatedEvent != null)
          //  {
            //    routeUpdatedEvent(VoitureList, obstacles);
          //  }
        }
    }
}
