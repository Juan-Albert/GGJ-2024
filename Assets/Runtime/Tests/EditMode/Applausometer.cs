﻿using System;
using Runtime.Domain;
using UnityEngine;

namespace Runtime.Tests.EditMode
{
    public class Applausometer
    {
        public const float MaxApplauseMeter = 100f;
        public const float ComboIncrementalValue = 0.1f;
        public const float OutModifier = -10f;
        public const float GoodModifier = 1f;
        public const float GreatModifier = 2f;
        public const float PerfectModifier = 3f;
        
        public float ApplauseMeter { get; private set; }
        public Combo ApplauseCombo { get; private set; }

        private float ApplauseModifier => 1f + ApplauseCombo.Counter * ComboIncrementalValue;

        public Applausometer(float applauseMeter = MaxApplauseMeter)
        {
            if (applauseMeter is > MaxApplauseMeter or < 0)
                throw new NotSupportedException("Un Aplausometro no puede ser negativo o tener un valor mayor que el maximo");
            
            ApplauseMeter = applauseMeter;
            ApplauseCombo = new Combo(20);
        }
        
        public void ReactTo(Rhythm.Result result)
        {
            if(result == Rhythm.Result.Out)
                ApplauseCombo.Reset();
            
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
            if(result != Rhythm.Result.Out)
                ApplauseCombo.Increase();
        }

        private void ApplyModifier(float howMuch)
        {
            var increasedApplause = howMuch * ApplauseModifier;
            ApplauseMeter = Mathf.Clamp(ApplauseMeter + increasedApplause, 0f, MaxApplauseMeter);
        }
    }
}