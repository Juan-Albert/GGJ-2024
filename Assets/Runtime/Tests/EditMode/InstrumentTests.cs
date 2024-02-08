using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class InstrumentTests
    {
        [Theory]
        [TestCase(0, Rhythm.Result.Perfect)]
        [TestCase(Rhythm.PerfectTime, Rhythm.Result.Great)]
        [TestCase(Rhythm.GreatTime, Rhythm.Result.Good)]
        [TestCase(Rhythm.GoodTime, Rhythm.Result.Out)]
        public void PlayedNote_OnTime(float passedTime, Rhythm.Result score)
        {
            var noteToBePlayed = new Note("Rhythm");
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []{new Beat(10, new Note("Rhythm"))});
            var sut = new Musician(sheet);
            
            sheet.PassTime(passedTime + 0.01f);
            sut.Play(noteToBePlayed).Should().Be(score);
        }
        
        [Test]
        public void PlayNote_OncePerBeat()
        {
            var noteToBePlayed = new Note("Rhythm");
            var anotherNoteToBePlayed = new Note("Rhythm");
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []{new Beat(5, new Note("Rhythm"))});
            var sut = new Musician(sheet);

            sheet.PassTime( 0.01f);
            sut.Play(noteToBePlayed).Should().Be(Rhythm.Result.Perfect);
            sut.Play(anotherNoteToBePlayed).Should().Be(Rhythm.Result.Out);
        }
        
        [Test]
        public void PlayedNote_DifferentAsBeat_IsOutTime()
        {
            var noteToBePlayed = new Note("Silence");
            var sheet = Sheet.OneBeatSheet;
            var sut = new Musician(sheet);

            sheet.PassTime( 0.01f);
            sut.Play(noteToBePlayed).Should().Be(Rhythm.Result.Out);
        }
    }
}