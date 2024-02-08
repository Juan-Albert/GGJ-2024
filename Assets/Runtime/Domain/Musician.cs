using System;
using System.Collections.Generic;

namespace Runtime.Domain
{
    public class Musician
    {
        private Sheet Sheet;
        private List<PlayedNote> playedNotes;

        private bool AlreadyPlayedAtBeat => playedNotes.Exists(n => n.PlayedAt == Sheet.CurrentBeat);

        public Musician(Sheet sheet)
        {
            Sheet = sheet;
            playedNotes = new List<PlayedNote>();
        }

        public Rhythm.Result Play(Note note)
        {
            if (Sheet.HasEnded)
                throw new NotSupportedException("No se puede tocar cuando la partitura a terminado");

            return AlreadyPlayedAtBeat ? Rhythm.Result.Out : SaveAndCheckPlayed(note);
        }

        private Rhythm.Result SaveAndCheckPlayed(Note note)
        {
            var played = new PlayedNote(Sheet.CurrentTime, note, Sheet.CurrentBeat);
            var result = IsOnTime(played);

            if (result != Rhythm.Result.Out)
                playedNotes.Add(played);
            
            return result;
        }

        private Rhythm.Result IsOnTime(PlayedNote played)
        {
            // hacerlo con el next beat tambien
            if (Sheet.HasEnded || DifferentNoteAsCurrentBeat())
                return Rhythm.Result.Out;
            
            return Rhythm.BetterOf(played.OnTimeAt(Sheet.StartTimeOf(played.PlayedAt)));

            bool DifferentNoteAsCurrentBeat() => !Sheet.CurrentBeat.HasNote(played.Played);
        }
    }
}