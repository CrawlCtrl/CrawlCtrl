using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class EmptyLineDeserializationTests
{
    [Theory]
    [InlineData("")]
    [InlineData("# Some comment")]
    [InlineData("  ")]
    [InlineData("  # Some comment")]
    public void WHEN_Line_is_empty_line_WHILE_Including_empty_lines_THEN_Deserialize_as_empty_line(string line)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsType<EmptyLine>(deserializedLine);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("# Some comment")]
    [InlineData("  ")]
    [InlineData("  # Some comment")]
    public void WHEN_Line_is_empty_line_WHILE_Excluding_empty_lines_THEN_Do_not_deserialize_as_empty_line(string line)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = false
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsNotType<EmptyLine>(deserializedLine);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("# Some comment", "")]
    [InlineData("  ", "  ")]
    [InlineData("  # Some comment", "  ")]
    public void WHEN_Line_is_empty_line_THEN_Set_value_to_everything_after_directive_terminator_excluding_comment(string line, string expectedValue)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true
            }
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<EmptyLine>(deserializedLine);
        Assert.Equal(expectedValue, emptyLine.Value);
    }
    
    [Fact]
    public void WHEN_Line_has_no_comment_WHILE_Including_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true,
                IncludeComments = true
            }
        );

        const string line = "";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<EmptyLine>(deserializedLine);
        Assert.Null(emptyLine.Comment);
    }
    
    [Fact]
    public void WHEN_Line_has_no_comment_WHILE_Excluding_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true,
                IncludeComments = false
            }
        );

        const string line = "";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<EmptyLine>(deserializedLine);
        Assert.Null(emptyLine.Comment);
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Including_comment_THEN_Comment_is_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true,
                IncludeComments = true
            }
        );

        const string line = "# Some comment";
        const string expectedComment = " Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<EmptyLine>(deserializedLine);
        Assert.Equal(expectedComment, emptyLine.Comment);
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Excluding_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true,
                IncludeComments = false
            }
        );

        const string line = "# Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<EmptyLine>(deserializedLine);
        Assert.Null(emptyLine.Comment);
    }
}