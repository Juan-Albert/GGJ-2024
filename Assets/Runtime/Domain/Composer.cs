using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Runtime.Domain
{
    public static class Composer
    {
        private static readonly Sheet[] SheetBook = 
        {
            new(Tempo.OneBeatPerSecond, new ForwardTime(), new[]
            {
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Silence),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Silence),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Silence),
                new Beat(1, Note.Trumpet),
                new Beat(1, Note.Silence),
            })
        };
        
        private static Sheet RandomMusic() => SheetBook[Random.Range(0, SheetBook.Length)].AsCopy();
        
        public static Song Compose() => new(RandomMusic());
        
        public static Sheet AsRhythm(this Tempo tempo) 
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

        private static Sheet AsCopy(this Sheet toBeCopied)
            => new(toBeCopied.TempoOfSheet, new ForwardTime(), GetBeatsOf(toBeCopied));

        private static Beat[] GetBeatsOf(Sheet sheet) 
            => sheet.Beats.Select(b => b.Copy()).ToArray();
    }

}