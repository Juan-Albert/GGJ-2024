using System;
using System.Collections.Generic;

namespace Runtime.Domain
{
    public class Instrument
    {
        private Sheet Sheet;
        private List<PlayedNote> playedNotes;

        public Instrument(Sheet sheet)
        {
            Sheet = sheet;
            playedNotes = new List<PlayedNote>();
        }

        public OnTime.Result Play(Note note)
        {
            if (Sheet.HasEnded)
                throw new NotSupportedException("No se puede tocar cuando la partitura a terminado");

            throw new NotImplementedException();
        }
        
        public OnTime.Result IsOnTime(Note note)
        {
            // hacerlo con el next beat tambien
            if (Sheet.HasEnded)
                return OnTime.Result.Out;
            
            if (!Sheet.CurrentBeat.HasNote(note))
                return OnTime.Result.Out;
            
            //Se ha tocado ya
            
            var played = new PlayedNote(Sheet.CurrentTime, note, Sheet.CurrentBeat);
            return played.OnTimeAt(Sheet.StartTimeOf(played.PlayedAt)); 
        }
    }
}