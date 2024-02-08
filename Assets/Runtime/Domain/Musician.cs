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

        /*public Rhythm.Result Play(Note note)
        {
            if (Sheet.HasEnded)
                throw new NotSupportedException("No se puede tocar cuando la partitura a terminado");

            throw new NotImplementedException();
        }*/

        public Rhythm.Result IsOnTime(Note note)
        {
            // hacerlo con el next beat tambien
            if (Sheet.HasEnded || AlreadyPlayedAtBeat || SameNoteAsCurrentBeat())
                return Rhythm.Result.Out;
            
            var played = new PlayedNote(Sheet.CurrentTime, note, Sheet.CurrentBeat);
            return Rhythm.BetterOf(played.OnTimeAt(Sheet.StartTimeOf(played.PlayedAt)));

            bool SameNoteAsCurrentBeat() => !Sheet.CurrentBeat.HasNote(note);
        }
    }
}