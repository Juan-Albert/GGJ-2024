using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class ApplausometerTests
    {
        [Test]
        public void ApplauseMeterIsFull_ByDefault()
        {
            var sut = new Applausometer();
            sut.ApplauseMeter.Should().Be(Applausometer.MaxApplauseMeter);
        }

        [Theory]
        [TestCase(Applausometer.OutModifier, Rhythm.Result.Out)]
        [TestCase(Applausometer.GoodModifier, Rhythm.Result.Good)]
        [TestCase(Applausometer.GreatModifier, Rhythm.Result.Great)]
        [TestCase(Applausometer.PerfectModifier, Rhythm.Result.Perfect)]
        public void ReactToResult(float applausometerModifier, Rhythm.Result result)
        {
            var sut = new Applausometer(Applausometer.MaxApplauseMeter * 0.5f);
            sut.ReactTo(result);
            sut.ApplauseMeter.Should().Be((Applausometer.MaxApplauseMeter * 0.5f) + applausometerModifier);
        }
    }
}