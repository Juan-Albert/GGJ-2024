using System.Collections.Generic;
using System.Linq;

namespace Runtime.Domain
{
    public class Sheet
    {
        public IEnumerable<Beat> Beats { get; set; }

        public Beat CurrentBeat { get; private set; }

        public Sheet(IEnumerable<Beat> beats)
        {
            Beats = beats;
            CurrentBeat = beats.First();
        }

        public static Sheet Empty => new Sheet(new List<Beat>());
    }
}