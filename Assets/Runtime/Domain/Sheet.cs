using System;
using System.Collections.Generic;
using System.Linq;

namespace Runtime.Domain
{
    public class Sheet
    {
        public ForwardTime ForwardTime { get; }
        public Tempo TempoOfSheet { get; }
        public IEnumerable<Beat> Beats { get; }

        public float CurrentTime => ForwardTime.ElapsedTimeInSecond;
        public Beat CurrentBeat => HasEnded ? Beat.Silence : BeatAtCurrentTime();
        public Beat NextBeat => throw new NotImplementedException();
        public bool HasEnded => ForwardTime.ElapsedTimeInSecond >= TotalSheetDuration;
        public float TotalSheetDuration => Beats.Sum(b => TempoOfSheet.ToSeconds(b.Duration));


        public Sheet(Tempo tempoOfSheet, ForwardTime forwardTime, IEnumerable<Beat> beats)
        {
            if (beats == null)
                throw new ArgumentException("La lista de beats no puede ser nula");
            
            Beats = beats;
            TempoOfSheet = tempoOfSheet;
            ForwardTime = forwardTime;
        }

        public string Read() => CurrentBeat.Play();

        public void PassTime(float elapsedTime)
        {
            if (HasEnded)
                throw new NotSupportedException("No se puede reproducir una partitura que ha terminado");
            
            ForwardTime.PassTime(elapsedTime);
        }

        public float StartTimeOf(Beat beat)
        {
            if (!Beats.Contains(beat))
                throw new NotSupportedException("La partitura no contiene ese beat");
                
            var elapsedTime = 0f;
            for (var i = 0; i < Beats.Count(); i++)
            {
                if (Beats.ElementAt(i).Equals(beat))
                    return elapsedTime;
                
                elapsedTime += TempoOfSheet.ToSeconds(Beats.ElementAt(i).Duration);
            }

            throw new NotSupportedException("No deberia de llegar aquí");
        }

        private Beat BeatAtCurrentTime()
        {
            if (HasEnded)
                throw new NotSupportedException("No hay un beat actual en una partitura que ha terminado");
            
            var elapsedTime = ForwardTime.ElapsedTimeInSecond;
            for (var i = 0; i < Beats.Count(); i++)
            {
                elapsedTime -= TempoOfSheet.ToSeconds(Beats.ElementAt(i).Duration);

                if (elapsedTime <= 0f)
                    return Beats.ElementAt(i);
            }

            throw new NotSupportedException("No deberia de llegar aquí");
        }

        public static Sheet Empty => new (new Tempo(1), new ForwardTime(),new List<Beat>());


        public static Sheet OneBeatSheet => new 
            (
                Tempo.OneBeatPerSecond,
                new ForwardTime(),
                new List<Beat> { Beat.Sound }
            );
    }
}