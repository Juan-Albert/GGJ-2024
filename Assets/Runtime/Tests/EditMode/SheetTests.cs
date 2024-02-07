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
            var otherBeat = new Beat(1);
            var sut = new Sheet(new Tempo(1), new ForwardTime(), new[] { beat, otherBeat });
            sut.CurrentBeat.Should().Be(beat);
        }

        [Test]
        public void AsTimeGoesBy_BeatChange()
        {
            var beat = new Beat(1);
            var otherBeat = new Beat(2);
            var sut = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new[] { beat, otherBeat });
            
            sut.PassTime(1.1f);

            sut.CurrentBeat.Should().Be(otherBeat);
        }
        
        [Test]
        public void Play()
        {
            var beat = Beat.Sound;
            var otherBeat = new Beat(2 ,"otherSound");
            var sut = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new[] { beat, otherBeat });

            sut.Play().Should().Be("Sound");
            sut.PassTime(1.1f);
            sut.Play().Should().Be("otherSound");
        }

        [Theory]
        [TestCase(0, OnTime.Result.Perfect)]
        [TestCase(OnTime.PerfectTime, OnTime.Result.Great)]
        [TestCase(OnTime.GreatTime, OnTime.Result.Good)]
        [TestCase(OnTime.GoodTime, OnTime.Result.Out)]
        public void PlayedNote_OnTime(float passedTime, OnTime.Result score)
        {
            var noteToBePlayed = new Note("Rhythm");
            var sut = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []{new Beat(10, new Note("Rhythm"))});
            
            sut.PassTime(passedTime + 0.01f);
            sut.IsOnTime(noteToBePlayed).Should().Be(score);
        }
        
        [Test]
        public void PlayedNote_DifferentAsBeat_IsOutTime()
        {
            var noteToBePlayed = new Note("Silence");
            var sut = Sheet.OneBeatSheet;

            sut.PassTime( 0.01f);
            sut.IsOnTime(noteToBePlayed).Should().Be(OnTime.Result.Out);
        }
    }
}