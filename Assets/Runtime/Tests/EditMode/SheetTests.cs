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
    }
}