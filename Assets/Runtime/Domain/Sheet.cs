using System;
using System.Collections.Generic;
using System.Linq;

namespace Runtime.Domain
{
    public class Sheet
    {
        public ForwardTime ForwardTime { get; }
        public Tempo Tempo { get; }
        public IEnumerable<Beat> Beats { get; }
        
        private int currentBeatIndex;
        
        public Beat CurrentBeat => HasEnded ? Beat.Silence : Beats.ElementAt(currentBeatIndex);
        public bool HasEnded => currentBeatIndex >= Beats.Count();
        
        public static Sheet Empty => new (new Tempo(1), new ForwardTime(),new List<Beat>());

        public Sheet(Tempo tempo, ForwardTime forwardTime, IEnumerable<Beat> beats)
        {
            if (beats == null)
                throw new ArgumentException("La lista de beats no puede ser nula");
            
            Beats = beats;
            Tempo = tempo;
            ForwardTime = forwardTime;
            currentBeatIndex = 0;
        }
        
        public string PlayCurrentBeat() => CurrentBeat.Play();

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