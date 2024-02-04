using System;

namespace Runtime.Domain
{
    public class ForwardTime
    {
        public float ElapsedTimeInSecond { get; private set; }

        public void PassTime(float elapsedTime)
        {
            if (elapsedTime <= 0)
                throw new NotSupportedException("El tiempo tiene que pasar hacia delante");
            
            ElapsedTimeInSecond += elapsedTime;
        }
    }
}