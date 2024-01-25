using CrawlCtrl.Deserialization;
using CrawlCtrl.UnitTests.TestData;
using Xunit;

namespace CrawlCtrl.UnitTests.General.Deserialization;

public sealed class DeserializingUnknownDirectiveTests
{
    [Theory]
    [InlineData("unknown:")]
    [InlineData("unknown: directive")]
    [InlineData("unknown: directive # Some comment")]
    public void WHEN_Line_is_unknown_directive_WHILE_Including_unknown_directives_THEN_Deserialize_as_unknown_directive(string line)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsType<UnknownDirective>(deserializedLine);
    }
    
    [Theory]
    [InlineData("unknown:")]
    [InlineData("unknown: directive")]
    [InlineData("unknown: directive # Some comment")]
    public void WHEN_Line_is_unknown_directive_WHILE_Excluding_unknown_directives_THEN_Do_not_deserialize_as_unknown_directive(string line)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = false
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsNotType<UnknownDirective>(deserializedLine);
    }

    [Theory]
    [InlineData("unknown:", "unknown")]
    [InlineData(" unknown : directive", " unknown ")]
    public void WHEN_Line_is_unknown_directive_THEN_Set_directive_to_everything_before_directive_terminator(string line, string expectedDirective)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var unknownDirective = Assert.IsType<UnknownDirective>(deserializedLine);
        Assert.Equal(expectedDirective, unknownDirective.OriginalDirective);
    }

    [Theory]
    [InlineData("unknown:", "")]
    [InlineData("unknown:  ", "  ")]
    [InlineData("unknown: directive ", " directive ")]
    [InlineData("unknown: directive # Some comment", " directive ")]
    public void WHEN_Line_is_unknown_directive_THEN_Set_value_to_everything_after_directive_terminator_excluding_comment(string line, string expectedValue)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var unknownDirective = Assert.IsType<UnknownDirective>(deserializedLine);
        Assert.Equal(expectedValue, unknownDirective.OriginalValue);
    }
    
    [Fact]
    public void WHEN_Line_has_no_comment_WHILE_Including_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true,
                IncludeComments = true
            }
        );

        const string line = "directive: value";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var unknownDirective = Assert.IsType<UnknownDirective>(deserializedLine);
        Assert.Null(unknownDirective.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Line_has_no_comment_WHILE_Excluding_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true,
                IncludeComments = false
            }
        );

        const string line = "directive: value";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var unknownDirective = Assert.IsType<UnknownDirective>(deserializedLine);
        Assert.Null(unknownDirective.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Including_comment_THEN_Comment_is_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true,
                IncludeComments = true
            }
        );

        const string line = "directive: value # Some comment";
        const string expectedComment = " Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var unknownDirective = Assert.IsType<UnknownDirective>(deserializedLine);
        Assert.Equal(expectedComment, unknownDirective.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Excluding_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true,
                IncludeComments = false
            }
        );

        const string line = "directive: value # Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var unknownDirective = Assert.IsType<UnknownDirective>(deserializedLine);
        Assert.Null(unknownDirective.OriginalComment);
    }

    [Theory]
    [InlineData("the-directive: the-value # The comment", "the-directive: the-value # The comment")]
    [InlineData(" the-directive: the-value # The comment", " the-directive: the-value # The comment")]
    [InlineData("the-directive: the-value # The comment ", "the-directive: the-value # The comment ")]
    [InlineData(" the-directive: the-value # The comment ", " the-directive: the-value # The comment ")]
    public void WHEN_Line_has_been_deserialized_THEN_Full_robots_txt_line_is_set(string line, string expectedFullLine)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeUnknownDirectives = true
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.Equal(expectedFullLine, deserializedLine.FullLine);
    }
}