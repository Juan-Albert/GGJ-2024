using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class BeatTests
    {
        [Test]
        public void CurrentFrame_IsZeroByDefault()
        {
            new Beat(1).CurrentFrame.Should().Be(0);
        }

        [Test]
        public void BeatNextFrame()
        {
            var sut = new Beat(2);
            sut.NextFrame();
            sut.CurrentFrame.Should().Be(1);
            sut.NextFrame();
            sut.CurrentFrame.Should().Be(2);
        }

        [Test]
        public void BeatIsCompleted()
        {
            var sut = new Beat(1);
            sut.IsCompleted.Should().BeFalse();
            sut.NextFrame();
            sut.IsCompleted.Should().BeTrue();
        }
    }
}