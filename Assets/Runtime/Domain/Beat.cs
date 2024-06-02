using System;

namespace Runtime.Domain
{
    public class Beat
    {
        public int Duration { get; }

        private bool hadSound;
        private readonly Note note;

        public bool HasNote(Note note) => this.note.Sound == note.Sound;
        
        public Beat(int duration) : this (duration, Note.Silence) { }
        
        public Beat(int duration, Note note)
        {
            if (duration <= 0)
                throw new NotSupportedException("No se puede crear un beat que no tenga duracion");

            hadSound = false;
            Duration = duration;
            this.note = note;
        }

        public string Play() => hadSound ? Note.Silence.Sound : PlayFirstTime();
        public Beat Copy() => new(Duration, note);
        
        private string PlayFirstTime()
        {
            hadSound = true;
            return note.Sound;
        }

        public static Beat Silence => new (1);
        public static Beat Sound => new (1, new Note("Sound"));
    }
}