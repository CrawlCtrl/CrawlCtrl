using FluentAssertions;
using Xunit;

namespace CrawlCtrl.Reader.UnitTests;

public sealed class MappingOptionsToImmutableOptionsTests
{
    [Fact]
    public void THEN_User_agent_options_are_mapped_to_immutable_options()
    {
        // Arrange
        var options = new RobotsReaderOptions
        {
            UserAgent = new StringUserAgent("Test", false)
        };

        var expectedUserAgent = options.UserAgent;

        // Act
        var immutableOptions = options.ToImmutable();

        // Assert
        immutableOptions.UserAgent.Should().BeEquivalentTo(expectedUserAgent);
    }
    
    [Fact]
    public void THEN_Deserializer_options_are_mapped_to_immutable_options()
    {
        // Arrange
        var options = new RobotsReaderOptions
        {
            DeserializerOptions = new RobotsDeserializerOptions
            {
                IncludeComments = true,
                IncludeUnknownDirectives = true
            }
        };

        var expectedDeserializerOptions = options.DeserializerOptions;
        
        // Act
        var immutableOptions = options.ToImmutable();
        
        // Assert
        immutableOptions.DeserializerOptions.Should().BeEquivalentTo(expectedDeserializerOptions);
    }
}