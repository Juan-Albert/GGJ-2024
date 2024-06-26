﻿using System;
using System.Collections.Generic;

namespace Runtime.Domain
{
    public class Musician
    {
        private readonly Sheet Sheet;
        private readonly List<PlayedNote> playedNotes;

        private bool AlreadyPlayedAt(Beat beat) => playedNotes.Exists(n => n.PlayedAt == beat);

        public Musician(Sheet sheet)
        {
            Sheet = sheet;
            playedNotes = new List<PlayedNote>();
        }

        public bool HasFailedLastBeat()
        {
            if (Sheet.HasEnded)
                return false;

            return !IsSilence() &&
                   !AlreadyPlayedAt(Sheet.CurrentBeat) &&
                   OutOfTime();

            bool IsSilence() => Sheet.CurrentBeat.HasNote(Note.Silence);

            bool OutOfTime()
                => RhythmOfLastBeat() == Rhythm.Result.Out;

            Rhythm.Result RhythmOfLastBeat() 
                => Sheet.CurrentBeat.OnTimeAt(Sheet.CurrentTime, Sheet.StartTimeOf(Sheet.CurrentBeat), Sheet.TempoOfSheet);
        }

        public Rhythm.Result Play(Note note)
        {
            if (Sheet.HasEnded)
                throw new NotSupportedException("No se puede tocar cuando la partitura a terminado");
            return Rhythm.BetterOf(CheckCurrentBeat(), CheckNextBeat());

            Rhythm.Result CheckCurrentBeat()
                => AlreadyPlayedAt(Sheet.CurrentBeat) ? Rhythm.Result.Out : SaveAndCheckPlayed(note, Sheet.CurrentBeat);

            Rhythm.Result CheckNextBeat()
                => !Sheet.HasNext || AlreadyPlayedAt(Sheet.NextBeat)
                    ? Rhythm.Result.Out
                    : SaveAndCheckPlayed(note, Sheet.NextBeat);
        }

        private Rhythm.Result SaveAndCheckPlayed(Note note, Beat beat)
        {
            var played = new PlayedNote(Sheet.CurrentTime, note, beat);
            var result = IsOnTime(played);

            if (result != Rhythm.Result.Out)
                playedNotes.Add(played);

            return result;
        }

        private Rhythm.Result IsOnTime(PlayedNote played)
        {
            if (Sheet.HasEnded || DifferentNoteAsBeat())
                return Rhythm.Result.Out;

            return played.OnTimeAt(Sheet.StartTimeOf(played.PlayedAt), Sheet.TempoOfSheet);

            bool DifferentNoteAsBeat() => !played.PlayedAt.HasNote(played.Played);
        }

        public void FailLastBeat()
        {
            var failedNote = new PlayedNote(Sheet.CurrentTime, Note.Wrong, Sheet.CurrentBeat);
            playedNotes.Add(failedNote);
        }
    }
}