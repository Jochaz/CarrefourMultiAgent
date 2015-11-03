using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentSystemPCL.TP
{
    enum Direction
    {
        dHaut,
        dBas,
        dGauche,
        dDroite
    }

    public class VoitureAgent
    {
        private double speedX;
        private double PosX;
        private double PosY;
        private double speedY;
        private double width;
        private double height;
        private Direction Depart;
        private Direction Arrive;

        public VoitureAgent(Direction _depart, Direction _arrive)
        {
            Depart = _depart;
            Arrive = _arrive;
            if (_depart == Direction.dGauche)
            {
                PosX = 0;
                PosY = 265;
                height = 40;
                width = 50;
            }
            else if (_depart == Direction.dDroite)
            {
                PosX = 500 - 50;
                PosY = 265;
                height = 40;
                width = 50;
            }
            else if (_depart == Direction.dHaut)
            {
                PosX = 0;
                PosY = 0;
                height = 50;
                width = 40;
            }
            else if (_depart == Direction.dBas)
            {
                PosX = 0;
                PosY = 0;
                height = 50;
                width = 40;
            }

        }
    }
}
