using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class BeatTests
    {
        [Test]
        public void BeatStartAt_FirstFrame()
        {
            Assert.True(new Beat(1).CurrentFrame == 0);
        }

        [Test]
        public void BeatNextFrame()
        {
            var sut = new Beat(2);
            sut.Next();
            Assert.True(sut.CurrentFrame == 1);
            sut.Next();
            Assert.True(sut.CurrentFrame == 2);

        }
    }
}