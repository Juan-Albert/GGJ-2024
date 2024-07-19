using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Domain
{
    public static class Composer
    {
        private static readonly Sheet[] NoviceSheetBook =
        {
            new(Tempo.OneBeatPerSecond, new ForwardTime(), new[]
            {
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet),
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet)
            })
        };

        private static readonly Sheet[] BeginnerSheetBook =
        {
            new(Tempo.Moderato, new ForwardTime(), new[]
            {
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet),
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet)
            })
        };

        private static readonly Sheet[] CompetentSheetBook =
        {
            new(Tempo.Allegro, new ForwardTime(), new[]
            {
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet),
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet)
            })
        };

        private static readonly Sheet[] ProficientSheetBook =
        {
            new(Tempo.Presto, new ForwardTime(), new[]
            {
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet),
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet)
            })
        };

        private static readonly Sheet[] ExpertSheetBook =
        {
            new(Tempo.Prestissimo, new ForwardTime(), new[]
            {
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet),
                new Beat(1, Note.Handstand),
                new Beat(1, Note.Ball),
                new Beat(1, Note.Juggle),
                new Beat(1, Note.Trumpet)
            })
        };

        public static Song ComposeBasedOn(int sheetsPlayed) => new(RandomMusicWith(CalculateDifficulty(sheetsPlayed)));

        private static int CalculateDifficulty(int sheetsPlayed) => Mathf.FloorToInt(sheetsPlayed / 10f);

        private static Sheet RandomMusicWith(int difficulty)
        {
            var sheetBook = GetSheetBookBasedOn(difficulty);
            return sheetBook[Random.Range(0, sheetBook.Length)].AsCopy();
        }

        private static Sheet[] GetSheetBookBasedOn(int difficulty) =>
            difficulty switch
            {
                0 => NoviceSheetBook,
                1 => BeginnerSheetBook,
                2 => CompetentSheetBook,
                3 => ProficientSheetBook,
                _ => ExpertSheetBook
            };

        public static Sheet AsRhythm(this Tempo tempo)
            => new(tempo, new ForwardTime(), new[]
            {
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
            });

        public static Song AsCopy(this Song toBeCopied) => new(toBeCopied.Music.AsCopy());

        private static Sheet AsCopy(this Sheet toBeCopied)
            => new(toBeCopied.TempoOfSheet, new ForwardTime(), GetBeatsOf(toBeCopied));

        private static Beat[] GetBeatsOf(Sheet sheet)
            => sheet.Beats.Select(b => b.Copy()).ToArray();
    }
}