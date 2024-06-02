using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.Domain
{
    public static class Rhythm
    {
        public enum Result
        {
            Out = 0,
            Good = 1,
            Great = 2,
            Perfect = 3
        }

        public const float GoodTime = .15f;
        public const float GreatTime = .10f;
        public const float PerfectTime = .05f;

        public static Result OnTimeAt(this PlayedNote played, float currentTimeOfBeat, Tempo tempo)
        {
            if (DeltaPlayedTime() <= tempo.ToSeconds(PerfectTime))
                return Result.Perfect;

            if(DeltaPlayedTime() <= tempo.ToSeconds(GreatTime))
                return Result.Great;

            if(DeltaPlayedTime() <= tempo.ToSeconds(GoodTime))
                return Result.Good;

            return Result.Out;

            float DeltaPlayedTime() => Mathf.Abs(currentTimeOfBeat - played.When);
        }

        public static Result BetterOf(params Result[] results) => results.Max();
    }
}