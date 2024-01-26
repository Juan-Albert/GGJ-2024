using System;
using System.Linq;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class SheetTests
    {
        [Test]
        public void EmptySheet_ContainNoBeats()
        {
            Assert.True(!Sheet.Empty.Beats.Any());
        }

        [Test]
        public void CurrentBeat_IsFirstByDefault()
        {
            var beat = new Beat(1);
            var sut = new Sheet(new[] { beat });
            Assert.True(sut.CurrentBeat.Equals(beat));
        }

        [Test]
        public void CurrentFrame_IsZeroByDefault()
        {
            var sut = new Sheet(Array.Empty<Beat>());
            Assert.True(sut.CurrentFrame.Equals(0));
        }

        [Test]
        public void NextFrame()
        {
            var sut = new Sheet(new []{new Beat(3)});
            sut.NextFrame();
            Assert.True(sut.CurrentFrame.Equals(1));
            sut.NextFrame();
            Assert.True(sut.CurrentFrame.Equals(2));
            sut.NextFrame();
            Assert.True(sut.CurrentFrame.Equals(3));
        }

        [Test]
        public void TwoFrameBeat_AfterNextFrame_IsCurrent()
        {
            var beat = new Beat(2);
            var sut = new Sheet(new[] { beat });

            sut.NextFrame();
            Assert.True(sut.CurrentBeat.Equals(beat));
        }

        [Test]
        public void OneFrameBeat_AfterNextFrame_IsNotCurrent()
        {
            var beat1 = new Beat(1);
            var beat2 = new Beat(1);
            var sut = new Sheet(new[] { beat1, beat2});

            sut.NextFrame();
            Assert.True(sut.CurrentBeat.Equals(beat2));
        }
    }
}