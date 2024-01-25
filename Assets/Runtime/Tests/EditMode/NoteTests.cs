using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class NoteTests
    {
        [Test]
        public void NoteEquality()
        {
            Assert.True(Note.Silence.Equals(new Note("Silence")));
            Assert.False(Note.Silence.Equals(new Note("Sound")));
        }
    }
}