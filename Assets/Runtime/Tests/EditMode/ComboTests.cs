using FluentAssertions;
using NUnit.Framework;

namespace Runtime.Tests.EditMode
{
    public class ComboTests
    {
        [Test]
        public void IncreaseCombo()
        {
            var sut = new Combo(20);
            
            sut.Increase();
            sut.Increase();
            sut.Increase();

            sut.Counter.Should().Be(3);
        }

        [Test]
        public void ResetCombo()
        {
            var sut = new Combo(20);
            
            sut.Increase();
            sut.Increase();
            sut.Increase();
            sut.Reset();

            sut.Counter.Should().Be(0);
        }
        
        [Test]
        public void CantIncreaseCombo_AboveMax()
        {
            var sut = new Combo(2);
            
            sut.Increase();
            sut.Increase();
            sut.Increase();

            sut.Counter.Should().Be(sut.MaxCombo);
        }
        
    }
}