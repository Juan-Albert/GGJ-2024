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
            Tempo.Lento.SecondsPerBeat.Should().BeApproximately(1.5f, 0.001f);
            Tempo.Moderato.SecondsPerBeat.Should().BeApproximately(0.75f, 0.001f);
            Tempo.Presto.SecondsPerBeat.Should().BeApproximately(0.375f, 0.001f);
        }

        [Test]
        public void TempoToSeconds()
        {
            Tempo.Lento.ToSeconds(10).Should().BeApproximately(15f, 0.001f);
            Tempo.Moderato.ToSeconds(5).Should().BeApproximately(3.75f, 0.001f);
            Tempo.Presto.ToSeconds(3).Should().BeApproximately(1.125f, 0.001f);
        }
        
    }
}