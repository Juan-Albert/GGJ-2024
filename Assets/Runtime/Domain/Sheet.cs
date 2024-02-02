using System;
using System.Collections.Generic;
using System.Linq;

namespace Runtime.Domain
{
    public class Sheet
    {
        public IEnumerable<Beat> Beats { get; }
        
        private int currentBeatIndex;
        
        public Beat CurrentBeat => HasEnded ? Beat.Silence : Beats.ElementAt(currentBeatIndex);
        public bool HasEnded => currentBeatIndex >= Beats.Count();

        
        public static Sheet Empty => new (new List<Beat>());

        public Sheet(IEnumerable<Beat> beats)
        {
            if (beats == null)
                throw new ArgumentException("La lista de beats no puede ser nula");
            
            Beats = beats;
            currentBeatIndex = 0;
        }
        
        public string PlayCurrentBeat => CurrentBeat.Play();

        public void NextFrame()
        {
            if(HasEnded)
                throw new NotSupportedException("No se puede reproducir una partitura que ya ha terminado");
            
            CurrentBeat.NextFrame();
            
            if(CurrentBeat.IsCompleted)
                NextBeat();
        }

        private void NextBeat()
        {
            if (HasEnded)
                throw new NotSupportedException("No se puede pasar de beat si la partitura ha terminado");

            currentBeatIndex++;
        }
    }
}