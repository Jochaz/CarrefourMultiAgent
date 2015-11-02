
namespace MultiAgentSystemPCL
{
    public class BadZone : ObjectInWorld
    {
        protected double radius;
        public double Radius
        {
            get
            {
                return radius;
            }
        }

        protected int timeToLive = 100;

        public BadZone(double _posX, double _posY, double _radius)
        {
            PosX = _posX;
            PosY = _posY;
            radius = _radius;
        }

        public void Update()
        {
            timeToLive--;
        }

        public bool Dead()
        {
            return timeToLive <= 0;
        }
    }
}
