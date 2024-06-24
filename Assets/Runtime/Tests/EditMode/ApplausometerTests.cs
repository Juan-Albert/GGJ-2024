﻿using System;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class ApplausometerTests
    {
        private const float HalfMaxApplauseMeter = Applausometer.MaxApplauseMeter * 0.5f;

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
        public void ReactToResult(float applauseMeterModifier, Rhythm.Result result)
        {
            var sut = new Applausometer(HalfMaxApplauseMeter);
            sut.ReactTo(result);
            sut.ApplauseMeter.Should().Be(HalfMaxApplauseMeter + applauseMeterModifier);
        }

        [Test]
        public void ReactToPositiveResult_AtMaxApplauseMeter_EqualsMaxAplauseMeter()
        {
            var sut = new Applausometer();
            sut.ReactTo(Rhythm.Result.Perfect);
            sut.ApplauseMeter.Should().Be(Applausometer.MaxApplauseMeter);
        }

        [Theory]
        [TestCase(Applausometer.GoodModifier, Rhythm.Result.Good)]
        [TestCase(Applausometer.GreatModifier, Rhythm.Result.Great)]
        [TestCase(Applausometer.PerfectModifier, Rhythm.Result.Perfect)]
        public void ComboPositivesResults_GenerateMoreApplauseMeter(float applauseMeterModifier, Rhythm.Result result)
        {
            var sut = new Applausometer(HalfMaxApplauseMeter);
            sut.ReactTo(result);
            sut.ReactTo(result);
            sut.ReactTo(result);

            sut.ApplauseMeter.Should().Be(HalfMaxApplauseMeter);
        }
    }
}