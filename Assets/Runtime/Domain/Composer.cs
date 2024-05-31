namespace Runtime.Domain
{
    public static class Composer
    {
        public static Song Compose()
        {
            return new Song
            (new Sheet(Tempo.Prestissimo, new ForwardTime(), new[]
                {
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                }),
                new Sheet(Tempo.Prestissimo, new ForwardTime(), new[]
                {
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                })
            );
        }
    }

}