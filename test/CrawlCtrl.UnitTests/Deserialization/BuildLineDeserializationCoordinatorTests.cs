using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class BuildLineDeserializationCoordinatorTests
{
    [Theory]
    [InlineData(SitemapPolicy.All)]
    [InlineData(SitemapPolicy.OnlyValid)]
    [InlineData(SitemapPolicy.OnlyInvalid)]
    public void WHEN_Building_deserialization_coordinator_WHILE_Sitemaps_are_included_in_options_THEN_Include_sitemap_deserializer(SitemapPolicy sitemapPolicy)
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            SitemapPolicy = sitemapPolicy
        }.ToImmutableOrDefault();
        
        // Act
        var deserializationCoordinator = LineDeserializationCoordinatorFactory.Build(options);

        // Assert
        Assert.Contains(deserializationCoordinator.LineDeserializers.Values, deserializer => deserializer is SitemapLineDeserializer);
    }

    [Fact]
    public void WHEN_Building_deserialization_coordinator_WHILE_Sitemaps_are_excluded_in_options_THEN_Include_sitemap_deserializer()
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            SitemapPolicy = SitemapPolicy.Ignore
        }.ToImmutableOrDefault();
        
        // Act
        var deserializationCoordinator = LineDeserializationCoordinatorFactory.Build(options);

        // Assert
        Assert.DoesNotContain(deserializationCoordinator.LineDeserializers.Values, deserializer => deserializer is SitemapLineDeserializer);
    }
}