using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class TempoTest
    {
        [Test]
        public void SecondsPerBeat()
        {
            Tempo.Lento.SecondsPerBeat.Should().BeApproximately(2f, 0.001f);
            Tempo.Moderato.SecondsPerBeat.Should().BeApproximately(0.75f, 0.001f);
            Tempo.Presto.SecondsPerBeat.Should().BeApproximately(0.375f, 0.001f);
        }
    }
}