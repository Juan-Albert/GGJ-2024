namespace Runtime.Domain
{
    public class Note
    {
        private readonly string sound;

        public Note(string sound)
        {
            this.sound = sound;
        }

        public static Note Silence => new Note("Silence");

        public bool Equals(Note note)
        {
            return sound.Equals(note.sound);
        }
    }
}