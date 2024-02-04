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

        public static implicit operator string(Note note) => note.Sound;
        public static implicit operator Note(string sound) => new (sound);
    }
}