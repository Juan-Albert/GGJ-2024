using System;

namespace Runtime.Domain
{
    public class Beat
    {
        public int Duration { get; }
        
        private bool hadSound;
        private Note note;

        public Beat(int duration) : this (duration, Note.Silence) { }

        public Beat(int duration, Note note)
        {
            if (duration <= 0)
                throw new NotSupportedException("No se puede crear un beat que no tenga duracion");

            hadSound = false;
            Duration = duration;
            this.note = note;
        }

        public string Play() => hadSound ? Note.Silence.Play() : note.Play();

        public static Beat Silence => new (1);
        public static Beat Sound => new (1, new Note("Sound"));
    }
}