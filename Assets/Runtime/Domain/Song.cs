namespace Runtime.Domain
{
    public class Song
    {
        public Sheet Music { get; }
        public Sheet Rhythm { get; }
        public Tempo Tempo => Music.TempoOfSheet;

        public Song(Sheet music)
        {
            Music = music;
            Rhythm = music.TempoOfSheet.AsRhythm();
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