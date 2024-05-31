using System;
using Random = UnityEngine.Random;

namespace Runtime.Domain
{
    public static class Composer
    {
        private static Sheet[] sheetBook = 
        {
            new(Tempo.Prestissimo, new ForwardTime(), new[]
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
        };
        
        public static Song Compose()
        {
            return new Song(RandomMusic(), Tempo.Prestissimo.AsRhythm());
        }

        private static Sheet RandomMusic() => sheetBook[Random.Range(0, sheetBook.Length)];

        private static Sheet AsRhythm(this Tempo tempo) 
            => new(tempo, new ForwardTime(), new[]
                {
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                    new Beat(1, Note.Handstand),
                });
    }

}