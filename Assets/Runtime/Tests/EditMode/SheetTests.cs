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
        
    }
}