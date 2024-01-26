using System;
using UnityEditor.Experimental.GraphView;

namespace Runtime.Domain
{
    public class Beat
    {
        private int framesDuration;

        public int CurrentFrame { get; private set; }
        public bool IsCompleted => CurrentFrame >= framesDuration;

        public static Beat Silence => new (1);

        public Beat(int framesDuration) : this (framesDuration, Note.Silence) { }

        public Beat(int framesDuration, Note note)
        {
            if (framesDuration <= 0)
                throw new NotSupportedException("No se puede crear un beat con 0 o menos frames");
            
            this.framesDuration = framesDuration;
            CurrentFrame = 0;
        }

        public void NextFrame()
        {
            if (IsCompleted)
                throw new NotSupportedException("El beat ya habia terminado y quería seguir");

            CurrentFrame++;
        }
    }
}