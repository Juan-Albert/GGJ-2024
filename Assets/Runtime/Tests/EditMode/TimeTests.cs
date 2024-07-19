using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class TimeTests
    {
        [Test]
        public void DefaultElapsedTime_IsZero()
        {
            new ForwardTime().ElapsedTimeInSecond.Should().BeApproximately(0f, 0.001f);
        }

        [Test]
        public void PassTime()
        {
            var sut = new ForwardTime();
            sut.PassTime(.5f);
            sut.ElapsedTimeInSecond.Should().BeApproximately(.5f, 0.001f);
            sut.PassTime(12.45f);
            sut.ElapsedTimeInSecond.Should().BeApproximately(12.95f, 0.001f);
        }
    }
}