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

        public static Result OnTimeAt(this Beat beat, float currentTime, float timeOfBeat, Tempo tempo)
        {
            if (DeltaPlayedTime() <= tempo.ToSeconds(PerfectTime))
                return Result.Perfect;

            if (DeltaPlayedTime() <= tempo.ToSeconds(GreatTime))
                return Result.Great;

            if (DeltaPlayedTime() <= tempo.ToSeconds(GoodTime))
                return Result.Good;

            return Result.Out;

            float DeltaPlayedTime() => Mathf.Abs(timeOfBeat - currentTime);
        }

        public static Result OnTimeAt(this PlayedNote played, float timeOfBeat, Tempo tempo)
            => played.PlayedAt.OnTimeAt(played.When, timeOfBeat, tempo);

        public static Result BetterOf(params Result[] results) => results.Max();
    }
}