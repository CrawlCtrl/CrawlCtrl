using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class BuildLineDeserializationCoordinatorTests
{
    [Fact]
    public void WHEN_Sitemaps_are_included_in_options_THEN_Include_sitemap_deserializers_in_coordinator()
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            IncludeSitemaps = true
        };
        
        // Act
        var deserializationCoordinator = LineDeserializationCoordinatorFactory.Build(options);

        // Assert
        Assert.Contains(deserializationCoordinator.LineDeserializers.Values, deserializer => deserializer is SitemapLineDeserializer);
    }
}