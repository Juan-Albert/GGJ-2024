using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class SheetTests
    {
        [Test]
        public void EmptySheet_ContainNoBeats()
        {
            Sheet.Empty.Beats.Any().Should().BeFalse();
        }
        
        [Test]
        public void EmptySheet_HasEnded()
        {
            Sheet.Empty.HasEnded.Should().BeTrue();
        }

        [Test]
        public void CurrentBeat_IsFirstByDefault()
        {
            var beat = new Beat(1);
            var sut = new Sheet(new[] { beat });
            sut.CurrentBeat.Should().Be(beat);
        }

        [Test]
        public void TwoFrameBeat_AfterNextFrame_IsCurrent()
        {
            var beat = new Beat(2);
            var sut = new Sheet(new[] { beat });

            sut.NextFrame();
            sut.CurrentBeat.Should().Be(beat);
        }

        [Test]
        public void OneFrameBeat_AfterNextFrame_IsNotCurrent()
        {
            var beat1 = new Beat(1);
            var beat2 = new Beat(1);
            var sut = new Sheet(new[] { beat1, beat2});

            sut.NextFrame();
            sut.CurrentBeat.Should().NotBe(beat1);
            sut.CurrentBeat.Should().Be(beat2);
        }
    }
}