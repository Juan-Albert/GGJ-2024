using System;
using System.Collections.Generic;
using System.Linq;

namespace Runtime.Domain
{
    public class Sheet
    {
        public IEnumerable<Beat> Beats { get; }
        public int CurrentFrame { get; private set; }
        
        private int currentBeatIndex;
        
        public Beat CurrentBeat => HasEnded ? Beat.Silence : Beats.ElementAt(currentBeatIndex);
        public bool HasEnded => currentBeatIndex >= Beats.Count();
        
        
        public static Sheet Empty => new (new List<Beat>());

        public Sheet(IEnumerable<Beat> beats)
        {
            if (beats == null)
                throw new ArgumentException("La lista de beats no puede ser nula");
            
            Beats = beats;
            CurrentFrame = 0;
            currentBeatIndex = 0;
        }

        public void NextFrame()
        {
            if(HasEnded)
                return;
            
            CurrentFrame++;

            if (CurrentBeat.IsCompleted)
                NextBeat();
            else
                CurrentBeat.NextFrame();
        }

        private void NextBeat()
        {
            if (HasEnded)
                throw new NotSupportedException("No se puede pasar de beat si la partitura ha terminado");

            currentBeatIndex++;
        }
    }
}