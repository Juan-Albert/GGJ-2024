namespace Runtime.Domain
{
    public readonly struct Note
    {
        public string Sound { get; }

        public Note(string sound)
        {
            Sound = sound;
        }

        public static Note Silence => new ("Silence");
        public static Note Rhythm => new ("Rhythm");
        public static Note Ball => new ("Ball");
        public static Note Juggle => new ("Juggle");
        public static Note Handstand => new ("Handstand");
        public static Note Trumpet => new ("Trumpet");

        public static implicit operator string(Note note) => note.Sound;
        public static implicit operator Note(string sound) => new (sound);
        
    }
}