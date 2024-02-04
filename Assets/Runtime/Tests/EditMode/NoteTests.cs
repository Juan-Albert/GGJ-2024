using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class NoteTests
    {
        [Test]
        public void NoteEquality()
        {
            Note.Silence.Should().Be(new Note("Silence"));
            Note.Silence.Should().NotBe(new Note("Sound"));
        }

        [Test]
        public void NotePlaySound()
        {
            var sound = "Sound";
            var sut = new Note(sound);

            sut.Sound.Should().Be(sound);
        }
    }
}