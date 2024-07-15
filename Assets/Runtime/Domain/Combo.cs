using System;

namespace Runtime.Domain
{
    public class Combo
    {
        public int MaxCombo { get; }
        public int Counter => Math.Clamp(counter, 0, MaxCombo);
        
        private int counter;

        public Combo(int maxCombo) => MaxCombo = maxCombo;

        public void Increase() => counter++;

        public void Reset() => counter = 0;
    }
}