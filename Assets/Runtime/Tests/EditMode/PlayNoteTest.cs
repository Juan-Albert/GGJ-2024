using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class PlayNoteTest
    {
        [Test]
        public void PlayedNote_DifferentAsBeat_IsOutTime()
        {
            var noteToBePlayed = Note.Wrong;
            var sheet = Sheet.OneBeatSheet;
            var sut = new Musician(sheet);

            sheet.PassTime( 0.01f);
            sut.Play(noteToBePlayed).Should().Be(Rhythm.Result.Out);
        }
        
        [Test]
        public void PlayNote_OncePerBeat()
        {
            var noteToBePlayed = Note.Rhythm;
            var anotherNoteToBePlayed = Note.Rhythm;
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []{new Beat(5, new Note("Rhythm"))});
            var sut = new Musician(sheet);

            sheet.PassTime( 0.01f);
            sut.Play(noteToBePlayed).Should().Be(Rhythm.Result.Perfect);
            sut.Play(anotherNoteToBePlayed).Should().Be(Rhythm.Result.Out);
        }
        
        [Theory]
        [TestCase(0, Rhythm.Result.Perfect)]
        [TestCase(Rhythm.PerfectTime, Rhythm.Result.Great)]
        [TestCase(Rhythm.GreatTime, Rhythm.Result.Good)]
        [TestCase(Rhythm.GoodTime, Rhythm.Result.Out)]
        public void PlayedNote_OnTime(float resultTempo, Rhythm.Result score)
        {
            var noteToBePlayed = Note.Rhythm;
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []{new Beat(10, new Note("Rhythm"))});
            var sut = new Musician(sheet);
            
            sheet.PassTime(Tempo.OneBeatPerSecond.ToSeconds(resultTempo) + 0.01f);
            sut.Play(noteToBePlayed).Should().Be(score);
        }
        
        [Theory]
        [TestCase(0, Rhythm.Result.Perfect)]
        [TestCase(Rhythm.PerfectTime, Rhythm.Result.Great)]
        [TestCase(Rhythm.GreatTime, Rhythm.Result.Good)]
        [TestCase(Rhythm.GoodTime, Rhythm.Result.Out)]
        public void PlayedNote_BeforeBeat_IsOnTime(float resultTempo, Rhythm.Result score)
        {
            var noteToBePlayed = Note.Rhythm;
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
            {
                new Beat(2, Note.Silence),
                new Beat(2, Note.Rhythm)
            });
            var sut = new Musician(sheet);

            var elapsedTime = Tempo.OneBeatPerSecond.ToSeconds(2) - 
                              Tempo.OneBeatPerSecond.ToSeconds(resultTempo) - 0.01f;
            sheet.PassTime(elapsedTime);
            sut.Play(noteToBePlayed).Should().Be(score);
        }

        [Test]
        public void PlayedNote_BeforeADifferentNoteOfBeat_IsOutTime()
        {
            var noteToBePlayed = Note.Rhythm;
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
            {
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Silence)
            });
            var sut = new Musician(sheet);

            var elapsedTime = Tempo.OneBeatPerSecond.ToSeconds(1) - 0.01f;
            sheet.PassTime(elapsedTime);
            sut.Play(noteToBePlayed).Should().Be(Rhythm.Result.Out);
        }

        [Test]
        public void NotPlayNote_AfterNotPlayedBeat_IsOutTime()
        {
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
            {
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm)
            });
            var sut = new Musician(sheet);
            
            var outOfTime = Rhythm.GoodTime + 0.01f;
            sheet.PassTime(outOfTime);
            sut.HasFailedLastBeat().Should().BeTrue();
            
            sheet.PassTime(1.01f);
            sut.HasFailedLastBeat().Should().BeTrue();
        }
        
        [Test]
        public void NotPlayNote_AfterPlayedBeat_IsNotOutTime()
        {
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
            {
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Rhythm)
            });
            var outOfTime = Rhythm.GoodTime + 0.01f;
            var sut = new Musician(sheet);
            
            sut.Play(Note.Rhythm);
            sheet.PassTime(outOfTime);
            
            sut.HasFailedLastBeat().Should().BeFalse();
        }
        
        [Test]
        public void AfterFailNote_IsNotOutTime()
        {
            var sheet = new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
            {
                new Beat(1, Note.Rhythm),
                new Beat(1, Note.Silence)
            });
            var sut = new Musician(sheet);
            
            var outOfTime = Rhythm.GoodTime + 0.01f;
            sheet.PassTime(outOfTime);
            sut.FailLastBeat();
            sut.HasFailedLastBeat().Should().BeFalse();
        }
    }
}