using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializeEmptyLineTests
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
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsType<CrawlCtrl.EmptyLine>(deserializedLine);
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
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsNotType<CrawlCtrl.EmptyLine>(deserializedLine);
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
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<CrawlCtrl.EmptyLine>(deserializedLine);
        Assert.Equal(expectedValue, emptyLine.OriginalValue);
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
            }.ToImmutableOrDefault()
        );

        const string line = "";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<CrawlCtrl.EmptyLine>(deserializedLine);
        Assert.Null(emptyLine.OriginalComment);
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
            }.ToImmutableOrDefault()
        );

        const string line = "";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<CrawlCtrl.EmptyLine>(deserializedLine);
        Assert.Null(emptyLine.OriginalComment);
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
            }.ToImmutableOrDefault()
        );

        const string line = "# Some comment";
        const string expectedComment = " Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<CrawlCtrl.EmptyLine>(deserializedLine);
        Assert.Equal(expectedComment, emptyLine.OriginalComment);
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
            }.ToImmutableOrDefault()
        );

        const string line = "# Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var emptyLine = Assert.IsType<CrawlCtrl.EmptyLine>(deserializedLine);
        Assert.Null(emptyLine.OriginalComment);
    }
    
    [Theory]
    [InlineData("# The comment", "# The comment")]
    [InlineData(" # The comment", " # The comment")]
    [InlineData("# The comment ", "# The comment ")]
    [InlineData(" # The comment ", " # The comment ")]
    public void WHEN_Line_has_been_deserialized_THEN_Full_robots_txt_line_is_set(string line, string expectedFullLine)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeEmptyLines = true
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.Equal(expectedFullLine, deserializedLine.FullLine);
    }
}