using UnityEngine;

namespace Runtime.Domain
{
    public static class OnTime
    {
        public enum Result
        {
            Out,
            Good,
            Great,
            Perfect
        }

        public const float GoodTime = 0.5f;
        public const float GreatTime = .35f;
        public const float PerfectTime = .2f;

        public static Result OnTimeAt(this PlayedNote played, float currentTimeOfBeat)
        {
            if (Mathf.Abs(currentTimeOfBeat - played.playedAt) <= PerfectTime)
                return Result.Perfect;

            if(Mathf.Abs(currentTimeOfBeat - played.playedAt) <= GreatTime)
                return Result.Great;

            if(Mathf.Abs(currentTimeOfBeat - played.playedAt) <= GoodTime)
                return Result.Good;

            return Result.Out;
        }

    }
}