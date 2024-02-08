namespace Runtime.Domain
{
    public readonly struct PlayedNote
    {
        public float When { get; }
        public Note Played { get; }
        public Beat PlayedAt { get; }

        public PlayedNote(float when, Note played, Beat playedAt)
        {
            When = when;
            Played = played;
            PlayedAt = playedAt;
        }
    }
}