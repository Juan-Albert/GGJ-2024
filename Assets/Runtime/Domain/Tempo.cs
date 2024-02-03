using System;

namespace Runtime.Domain
{
    public class Tempo
    {
        public Tempo(int beatsPerMinute)
        {
            if (beatsPerMinute <= 0)
                throw new NotSupportedException("el tempo no puede ser igual o menor a cero");
            
            BeatsPerMinute = beatsPerMinute;
        }

        public int BeatsPerMinute { get; }
        
        public float SecondsPerBeat => 60f / BeatsPerMinute;

        public static Tempo Lento => new(40);
        public static Tempo Moderato => new(80);
        public static Tempo Presto => new(160);

    }
}