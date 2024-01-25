using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class BeatTests
    {
        [Test]
        public void CurrentFrame_IsZeroByDefault()
        {
            Assert.True(new Beat(1).CurrentFrame == 0);
        }

        [Test]
        public void BeatNextFrame()
        {
            var sut = new Beat(2);
            sut.Next();
            Assert.True(sut.CurrentFrame.Equals(1));
            sut.Next();
            Assert.True(sut.CurrentFrame.Equals(2));
        }

        [Test]
        public void BeatIsCompleted()
        {
            var sut = new Beat(1);
            Assert.False(sut.IsCompleted);
            sut.Next();
            Assert.True(sut.IsCompleted);
        }
    }
}