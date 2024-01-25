using System;

namespace Runtime.Domain
{
    public class Beat
    {
        private int framesDuration;

        public int CurrentFrame { get; private set; }
        public bool IsCompleted => CurrentFrame >= framesDuration;

        public Beat(int framesDuration)
        {
            if (framesDuration <= 0)
                throw new NotSupportedException("No se puede crear un beat con 0 o menos frames");
            
            this.framesDuration = framesDuration;
            CurrentFrame = 0;
        }

        public void Next()
        {
            if (CurrentFrame >= framesDuration)
                throw new NotSupportedException("El beat ya habia terminado y quería seguir");

            CurrentFrame += 1;
        }
    }
}