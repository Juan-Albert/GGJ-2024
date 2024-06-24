using System;
using Runtime.Domain;
using UnityEngine;

namespace Runtime.Tests.EditMode
{
    public class Applausometer
    {
        public const float MaxApplauseMeter = 100;
        public const float OutModifier = -10f;
        public const float GoodModifier = 1f;
        public const float GreatModifier = 2f;
        public const float PerfectModifier = 3f;
        public float ApplauseMeter { get; private set; }

        public Applausometer(float applauseMeter = MaxApplauseMeter)
        {
            if (applauseMeter is > MaxApplauseMeter or < 0)
                throw new NotSupportedException("Un Aplausometro no puede ser negativo o tener un valor mayor que el maximo");
            
            ApplauseMeter = applauseMeter;
        }
        
        public void ReactTo(Rhythm.Result result)
        {
            switch (result)
            {
                case Rhythm.Result.Out:
                    ApplyModifier(OutModifier);
                    break;
                case Rhythm.Result.Good:
                    ApplyModifier(GoodModifier);
                    break;
                case Rhythm.Result.Great:
                    ApplyModifier(GreatModifier);
                    break;
                case Rhythm.Result.Perfect:
                    ApplyModifier(PerfectModifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        private void ApplyModifier(float modifier) 
            => ApplauseMeter = Mathf.Clamp(ApplauseMeter + modifier, 0f, MaxApplauseMeter);
    }
}