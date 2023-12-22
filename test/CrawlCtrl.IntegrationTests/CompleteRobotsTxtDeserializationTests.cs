using FluentAssertions;
using Xunit;

namespace CrawlCtrl.IntegrationTests;

public sealed class CompleteRobotsTxtDeserializationTests
{
    private readonly RobotsDeserializer _robotsDeserializer = new();
    
    [Fact]
    public async Task WHEN_Deserializing_all_lines_THEN_Deserialize_all_lines()
    {
        // Arrange
        var expectedLines = RobotsTestData.CompleteRobotsTxt;
        var deserializationOptions = new RobotsDeserializerOptions
        {
            IncludeSitemaps = true,
            SitemapsInclusionScope = InclusionScope.All,
            IncludeEmptyLines = true,
            IncludeInvalidLines = true,
            IncludeUnknownDirectives = true
        };
        using var robotsStreamReader = RobotsTestData.GetCompleteRobotsStreamReader();
        
        // Act
        var actualLines = await _robotsDeserializer.GetDeserializedLinesAsync(robotsStreamReader, deserializationOptions);

        // Assert
        expectedLines
            .Should()
            .BeEquivalentTo(actualLines, options =>
            {
                options.WithStrictOrdering();
                options.RespectingRuntimeTypes();
                return options;
            });
    }
    
    [Fact]
    public async Task WHEN_Deserializing_only_valid_sitemaps_THEN_Deserialize_only_valid_sitemaps()
    {
        // Arrange
        var expectedLines = RobotsTestData.CompleteRobotsTxt.Where(line => line is ValidSitemap);
        var deserializationOptions = new RobotsDeserializerOptions
        {
            IncludeSitemaps = true,
            SitemapsInclusionScope = InclusionScope.ValidOnly,
            IncludeEmptyLines = false,
            IncludeInvalidLines = false,
            IncludeUnknownDirectives = false
        };
        using var robotsStreamReader = RobotsTestData.GetCompleteRobotsStreamReader();
        
        // Act
        var actualLines = await _robotsDeserializer.GetDeserializedLinesAsync(robotsStreamReader, deserializationOptions);

        // Assert
        expectedLines
            .Should()
            .BeEquivalentTo(actualLines, options =>
            {
                options.WithStrictOrdering();
                options.RespectingRuntimeTypes();
                return options;
            });
    }
}