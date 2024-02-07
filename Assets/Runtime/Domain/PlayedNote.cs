namespace Runtime.Domain
{
    public readonly struct PlayedNote
    {
        public readonly float playedAt;
        public readonly Note note;

        public PlayedNote(float playedAt, Note note)
        {
            this.playedAt = playedAt;
            this.note = note;
        }
    }
}