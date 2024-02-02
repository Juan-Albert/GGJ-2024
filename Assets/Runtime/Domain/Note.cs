namespace Runtime.Domain
{
    public readonly struct Note
    {
        private readonly string sound;

        public Note(string sound)
        {
            this.sound = sound;
        }

        public static Note Silence => new ("Silence");

        public string Play() => sound;
    }
}