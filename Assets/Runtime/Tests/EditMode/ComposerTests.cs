using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Runtime.Tests.EditMode
{
    public class ComposerTests
    {
        [Test]
        public void ComposeDifficultySheets_BasedOnPlayedSheets()
        {
            Composer.ComposeBasedOn(0).Tempo.Should().Be(Tempo.OneBeatPerSecond);
            Composer.ComposeBasedOn(9).Tempo.Should().Be(Tempo.OneBeatPerSecond);
            Composer.ComposeBasedOn(10).Tempo.Should().Be(Tempo.Moderato);
            Composer.ComposeBasedOn(20).Tempo.Should().Be(Tempo.Allegro);
            Composer.ComposeBasedOn(30).Tempo.Should().Be(Tempo.Presto);
            Composer.ComposeBasedOn(40).Tempo.Should().Be(Tempo.Prestissimo);
            Composer.ComposeBasedOn(50).Tempo.Should().Be(Tempo.Prestissimo);
        }        
    }
}