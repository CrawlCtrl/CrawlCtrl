using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CrawlCtrl.IntegrationTests;

public sealed class EnumeratingDeserializedLinesTests
{
    private readonly RobotsDeserializer _robotsDeserializer = new();
    
    private readonly RobotsDeserializerOptions _deserializationOptions = new()
    {
        IncludeSitemaps = true,
        SitemapsInclusionScope = InclusionScope.All,
        IncludeEmptyLines = true,
        IncludeInvalidLines = true,
        IncludeUnknownDirectives = true
    };
    
    [Fact]
    public void WHILE_Robots_has_lines_WHEN_Enumerating_all_synchronously_THEN_Return_all_lines()
    {
        // Arrange
        var expectedDeserializedLines = RobotsTestData.CompleteRobotsTxt;
        using var robotsStreamReader = RobotsTestData.GetCompleteRobotsStreamReader();
        
        // Act
        var actualDeserializedLines = _robotsDeserializer
            .EnumerateDeserializedLines(robotsStreamReader, _deserializationOptions)
            .ToList();

        // Assert
        expectedDeserializedLines
            .Should()
            .BeEquivalentTo(actualDeserializedLines, options =>
            {
                options.WithStrictOrdering();
                options.RespectingRuntimeTypes();
                return options;
            });
    }
    
    [Fact]
    public void WHILE_Robots_is_empty_WHEN_Enumerating_all_synchronously_THEN_Return_no_lines()
    {
        // Arrange
        var expectedDeserializedLines = RobotsTestData.EmptyRobotsTxt;
        using var robotsStreamReader = RobotsTestData.GetEmptyRobotsStreamReader();
        
        // Act
        var actualDeserializedLines = _robotsDeserializer
            .EnumerateDeserializedLines(robotsStreamReader, _deserializationOptions)
            .ToList();

        // Assert
        expectedDeserializedLines
            .Should()
            .BeEquivalentTo(actualDeserializedLines, options =>
            {
                options.WithStrictOrdering();
                options.RespectingRuntimeTypes();
                return options;
            });
    }
    
    [Fact]
    public async Task WHILE_Robots_has_lines_WHEN_Enumerating_all_asynchronously_THEN_Return_all_lines()
    {
        // Arrange
        var expectedDeserializedLines = RobotsTestData.CompleteRobotsTxt;
        using var robotsStreamReader = RobotsTestData.GetCompleteRobotsStreamReader();
        
        // Act
        var actualDeserializedLines = await _robotsDeserializer
            .EnumerateDeserializedLinesAsync(robotsStreamReader, _deserializationOptions)
            .ToListAsync();

        // Assert
        expectedDeserializedLines
            .Should()
            .BeEquivalentTo(actualDeserializedLines, options =>
            {
                options.WithStrictOrdering();
                options.RespectingRuntimeTypes();
                return options;
            });
    }
    
    [Fact]
    public async Task WHILE_Robots_is_empty_WHEN_Enumerating_all_asynchronously_THEN_Return_no_lines()
    {
        // Arrange
        var expectedDeserializedLines = RobotsTestData.EmptyRobotsTxt;
        using var robotsStreamReader = RobotsTestData.GetEmptyRobotsStreamReader();
        
        // Act
        var actualDeserializedLines = await _robotsDeserializer
            .EnumerateDeserializedLinesAsync(robotsStreamReader, _deserializationOptions)
            .ToListAsync();

        // Assert
        expectedDeserializedLines
            .Should()
            .BeEquivalentTo(actualDeserializedLines, options =>
            {
                options.WithStrictOrdering();
                options.RespectingRuntimeTypes();
                return options;
            });
    }

    [Fact]
    public async Task WHILE_Enumerating_some_lines_THEN_Do_not_deserialize_more_lines_than_requested()
    {
        // Arrange
        const int numberOfLinesToTake = 3;
        using var robotsStreamReader = Substitute.For<StreamReader>("./robots_only_sitemaps.txt");
        
        // Act
        var actualDeserializedLines = _robotsDeserializer
            .EnumerateDeserializedLinesAsync(robotsStreamReader, _deserializationOptions);

        var enumerationIndex = 0;
        await foreach (var _ in actualDeserializedLines)
        {
            if (enumerationIndex == numberOfLinesToTake - 1)
            {
                break;
            }
            enumerationIndex++;
        }

        // Assert
        await robotsStreamReader.Received(numberOfLinesToTake).ReadLineAsync();
    }
}