namespace Runtime.Domain
{
    public class Song
    {
        public Sheet Music { get; }
        public Sheet Rhythm { get; }

        public Song(Sheet music, Sheet rhythm)
        {
            Music = music;
            Rhythm = rhythm;
        }

        public bool HasEnded => Music.HasEnded;

        public void PassTime(float elapsedTime)
        {
            Rhythm.PassTime(elapsedTime);
            Music.PassTime(elapsedTime);
        }

        public string PlayRhythm() => Rhythm.Read();

        public Note PlayMusic() => Music.Read();
    }
}