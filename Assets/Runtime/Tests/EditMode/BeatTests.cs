using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class BeatTests
    {
        [Test]
        public void PlayBeat_FirstTime_Sound()
        {
            var sut = new Beat(1, new Note("Sound"));

            sut.Play().Should().Be("Sound");
        }
        
        [Test]
        public void PlayBeat_SecondTime_DoNotSound()
        {
            var sut = new Beat(1, new Note("Sound"));
            sut.Play();
            sut.Play().Should().Be(Note.Silence.Sound);
        }

        [Test]
        public void HasNote()
        { 
            Beat.Sound.HasNote("Sound").Should().BeTrue();
            Beat.Sound.HasNote("Silence").Should().BeFalse();
        }
    }
}