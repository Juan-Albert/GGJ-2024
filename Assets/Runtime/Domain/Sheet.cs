using System.Collections.Generic;

namespace Runtime.Domain
{
    public class Sheet
    {
        public static Sheet Empty => new Sheet(new List<Beat>());

        public IEnumerable<Beat> Beats { get; set; }

        private Sheet(IEnumerable<Beat> beats)
        {
            Beats = beats;
        }
    }
}