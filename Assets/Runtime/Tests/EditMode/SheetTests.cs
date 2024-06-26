﻿using System;
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
        public void NextBeat()
        {
            var beat = new Beat(1);
            var otherBeat = new Beat(1);
            var sut = new Sheet(new Tempo(1), new ForwardTime(), new[] { beat, otherBeat });
            sut.NextBeat.Should().Be(otherBeat);
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
        public void Read()
        {
            var beat = Beat.Sound;
            var otherBeat = new Beat(2 ,"otherSound");
            var sut = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new[] { beat, otherBeat });

            sut.Read().Should().Be("Sound");
            sut.PassTime(1.1f);
            sut.Read().Should().Be("otherSound");
        }
    }
}