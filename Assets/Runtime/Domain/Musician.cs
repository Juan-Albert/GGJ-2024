using System;
using System.Collections.Generic;

namespace Runtime.Domain
{
    public class Musician
    {
        private Sheet Sheet;
        private List<PlayedNote> playedNotes;

        private bool AlreadyPlayedAt(Beat beat) => playedNotes.Exists(n => n.PlayedAt == beat);

        public Musician(Sheet sheet)
        {
            Sheet = sheet;
            playedNotes = new List<PlayedNote>();
        }

        public Rhythm.Result Play(Note note)
        {
            if (Sheet.HasEnded)
                throw new NotSupportedException("No se puede tocar cuando la partitura a terminado");

            return Rhythm.BetterOf(CheckCurrentBeat(), CheckNextBeat());

            Rhythm.Result CheckCurrentBeat() 
                => AlreadyPlayedAt(Sheet.CurrentBeat)  ?
                Rhythm.Result.Out :
                SaveAndCheckPlayed(note, Sheet.CurrentBeat);
            
            Rhythm.Result CheckNextBeat() 
                => !Sheet.HasNext || AlreadyPlayedAt(Sheet.NextBeat) ?
                Rhythm.Result.Out :
                SaveAndCheckPlayed(note, Sheet.NextBeat);
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
            if (Sheet.HasEnded || DifferentNoteAsCurrentBeat())
                return Rhythm.Result.Out;
            
            return played.OnTimeAt(Sheet.StartTimeOf(played.PlayedAt), Sheet.TempoOfSheet);

            bool DifferentNoteAsCurrentBeat() => !Sheet.CurrentBeat.HasNote(played.Played);
        }
    }
}