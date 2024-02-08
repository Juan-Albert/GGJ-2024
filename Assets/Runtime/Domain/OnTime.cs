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
            if (Mathf.Abs(currentTimeOfBeat - played.When) <= PerfectTime)
                return Result.Perfect;

            if(Mathf.Abs(currentTimeOfBeat - played.When) <= GreatTime)
                return Result.Great;

            if(Mathf.Abs(currentTimeOfBeat - played.When) <= GoodTime)
                return Result.Good;

            return Result.Out;
        }

    }
}