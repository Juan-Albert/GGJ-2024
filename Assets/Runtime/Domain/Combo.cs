using System;

namespace Runtime.Domain
{
    public class Combo
    {
        public int MaxCombo { get; }
        
        public int Counter { get; private set; }

        public Combo(int maxCombo)
        {
            MaxCombo = maxCombo;
        }

        public void Increase() => Counter = Math.Clamp(Counter + 1, 0, MaxCombo);

        public void Reset() => Counter = 0;
    }
}