using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows.Shapes;


namespace MultiAgentSystemPCL.TP
{

    public class VoitureAgent
    {
        public int Clignote { get; set; }
        public int IndexCouleurvoiture { get; set; }

        private Boolean stopped;

        public Boolean Stopped
        {
            get { return stopped; }
            set { stopped = value; }
        }
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
         * les coordonnées du véhicule sur la route
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

        private int freine;

        public int Freine
        {
            get { return freine; }
            set { freine = value; }
        }

        private int distanceSecurite;

        public int DistanceSecurite
        {
            get { return distanceSecurite; }
            set { distanceSecurite = value; }
        }

        public VoitureAgent(int apparition)
        {
            Random randomGenerator = new Random();
            IndexCouleurvoiture = randomGenerator.Next(3);
            coordonneesApparition = getApparition(apparition);
            direction = rdmDirection();
            Clignote = randomGenerator.Next(2);
            if (apparitionToString() == "bot" || apparitionToString() == "top")
            {
                width = 15;
                height = 22;

            }
            else
            {
                width = 22;
                height = 15;
            }
            freine = 1;
            turned = false;
            stopped = false;

            distanceSecurite = rdmDistance();

            //if(direction == "left")

        }

        private int rdmDistance()
        {
            Random randomGenerator = new Random();
            return randomGenerator.Next(8);
        }
        private int[] getApparition(int value)
        {
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
                tabCoordonnees[1] = 455;
                tabCoordonnees[2] = 1;
            }
            else if (value == 1)
            {
                tabCoordonnees[0] = 850;
                tabCoordonnees[1] = 380;
                tabCoordonnees[2] = 2;
            }
            else if (value == 2)
            {
                tabCoordonnees[0] = 380;
                tabCoordonnees[1] = -60;
                tabCoordonnees[2] = 3;
            }
            else if (value == 3)
            {
                tabCoordonnees[0] = 455;
                tabCoordonnees[1] = 850;
                tabCoordonnees[2] = 4;
            }

            return tabCoordonnees;

        }

        private string rdmDirection()
        {
            int value;
            Random randomGenerator = new Random();
            value = randomGenerator.Next(4);


            while (value + 1 == coordonneesApparition[2])
            {
                value = randomGenerator.Next(4);
            }

            string direction = "";
            if (value == 0)
            {
                direction = "left";
            }
            else if (value == 1)
            {
                direction = "right";
            }
            else if (value == 2)
            {
                direction = "top";
            }
            else if (value == 3)
            {
                direction = "bot";

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

            return "";
        }

        public string directionActuelle()
        {
            if (!turned)
            {
                if (apparitionToString() == "left")
                    return "right";
                if (apparitionToString() == "right")
                    return "left";
                if (apparitionToString() == "bot")
                    return "top";
                return "bot";
            }
            else
                return direction;

        }

        public void tourner()
        {
            double temp;

            temp = width;
            width = height;
            height = temp;
            turned = true;
            
        }

        public Boolean timeToTurn()
        {
            string apparition = apparitionToString();
            int left = CoordonneesApparition[0];
            int top = CoordonneesApparition[1];
            if ((apparition == "left" || apparition == "right") && (Direction != "right" || Direction != "left"))
            {
                if (Direction == "bot" && left == 370)
                    return true;
                else if (Direction == "top" && left == 440)
                    return true;
            }
            if ((apparition == "top" || apparition == "bot") && (Direction != "bot" || Direction != "top"))
            {
                if (Direction == "right" && top == 440)
                    return true;
                else if (Direction == "left" && top == 370)
                    return true;
            }
            return false;
        }

        public Boolean sortieDuCarrefour()
        {
            if(directionActuelle() == "left" && coordonneesApparition[0] < 314)
                return true;
            if (directionActuelle() == "right" && coordonneesApparition[0] > 494)
                return true;
            if (directionActuelle() == "bot" && coordonneesApparition[1] > 494)
                return true;
            if (directionActuelle() == "top" && coordonneesApparition[1] < 314)
                return true;
            return false;
        }

    }
}
