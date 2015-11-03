using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiAgentSystemPCL.TP
{

    public class VoitureAgent
    {

        private Boolean turned;

        public Boolean Turned
        {
            get { return turned; }
            set { turned = value; }
        }
        /**
         * longueur du véhicule
         */
        private double width;

        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        /**
        * largeur du véhicule
        */
        private double height;

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        /**
        * sa direction (top, bot, left ou right)
        */
        private string direction;

        public string Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /**
         * les coordonnées d'apparition du véhicule sur la route
         * indice 0 : coordonnées left
         * indice 1 : coordonées top
         * indice 2 : correspondance top, bot, left ou right
         */
        private int[] coordonneesApparition;

        public int[] CoordonneesApparition
        {
            get { return coordonneesApparition; }
            set { coordonneesApparition = value; }
        }

        private int vitesse;

        public int Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        public VoitureAgent()
        {
            coordonneesApparition = rdmApparition();
            direction = rdmDirection();

            while (direction == apparitionToString())
            {
                direction = rdmDirection();
            }

            if (apparitionToString() == "bot" || apparitionToString() == "top")
            {
                width = 40;
                height = 60;

            }
            else
            {
                width = 60;
                height = 40;
            }
            vitesse = 2;
            turned = false;

        }
        private int[] rdmApparition()
        {
            int value;
            Random randomGenerator = new Random();
            value = randomGenerator.Next(4);
            int[] tabCoordonnees = new int[3];

            /* tabCoordonnees[2]
                1 = left
                2 = right
                3 = top
                4 = bot
             */
            if (value == 0)
            {
                tabCoordonnees[0] = -60;
                tabCoordonnees[1] = 360;
                tabCoordonnees[2] = 1;
            }
            else if (value == 1)
            {
                tabCoordonnees[0] = 700;
                tabCoordonnees[1] = 295;
                tabCoordonnees[2] = 2;
            }
            else if (value == 2)
            {
                tabCoordonnees[0] = 300;
                tabCoordonnees[1] = -60;
                tabCoordonnees[2] = 3;
            }
            else if (value == 3)
            {
                tabCoordonnees[0] = 370;
                tabCoordonnees[1] = 700;
                tabCoordonnees[2] = 4;
            }

            return tabCoordonnees;

        }

        private string rdmDirection()
        {
            int value;
            Random randomGenerator = new Random();
            value = randomGenerator.Next(4);
            string direction = "";
            if (value == 0)
            {
                direction = "top";
            }
            else if (value == 1)
            {
                direction = "bot";
            }
            else if (value == 2)
            {
                direction = "left";
            }
            else if (value == 3)
            {
                direction = "right";

            }
            return direction;
        }

        public string apparitionToString()
        {
            int apparition = coordonneesApparition[2];
            //top par defaut;
            if (apparition == 1)
                return "left";
            else if (apparition == 2)
                return "right";
            else if (apparition == 3)
                return "top";
            else if (apparition == 4)
                return "bot";

            return "top";
        }

        public void tourner()
        {
            double temp;

            temp = width;
            width = height;
            height = temp;
            turned = true;
        }

    }
}
