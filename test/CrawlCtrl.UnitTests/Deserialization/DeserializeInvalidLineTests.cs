using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializeInvalidLineTests
{
    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid # Some comment")]
    [InlineData(" invalid ")]
    [InlineData(" invalid # Some comment")]
    public void WHEN_Line_is_invalid_line_WHILE_Including_invalid_lines_THEN_Deserialize_as_invalid_line(string line)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsType<InvalidLine>(deserializedLine);
    }
    
    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid # Some comment")]
    [InlineData(" invalid ")]
    [InlineData(" invalid # Some comment")]
    public void WHEN_Line_is_invalid_line_WHILE_Excluding_invalid_lines_THEN_Do_not_deserialize_as_invalid_line(string line)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = false
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.IsNotType<InvalidLine>(deserializedLine);
    }
    
    [Theory]
    [InlineData("invalid", "invalid")]
    [InlineData("invalid # Some comment", "invalid ")]
    [InlineData(" invalid ", " invalid ")]
    [InlineData(" invalid # Some comment", " invalid ")]
    public void WHEN_Line_is_invalid_line_THEN_Set_value_to_everything_after_directive_terminator_excluding_comment(string line, string expectedValue)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var invalidLine = Assert.IsType<InvalidLine>(deserializedLine);
        Assert.Equal(expectedValue, invalidLine.OriginalValue);
    }
    
    [Fact]
    public void WHEN_Line_has_no_comment_WHILE_Including_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true,
                IncludeComments = true
            }.ToImmutableOrDefault()
        );

        const string line = "invalid";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var invalidLine = Assert.IsType<InvalidLine>(deserializedLine);
        Assert.Null(invalidLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Line_has_no_comment_WHILE_Excluding_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true,
                IncludeComments = false
            }.ToImmutableOrDefault()
        );

        const string line = "invalid";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var invalidLine = Assert.IsType<InvalidLine>(deserializedLine);
        Assert.Null(invalidLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Including_comment_THEN_Comment_is_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true,
                IncludeComments = true
            }.ToImmutableOrDefault()
        );

        const string line = "invalid # Some comment";
        const string expectedComment = " Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var invalidLine = Assert.IsType<InvalidLine>(deserializedLine);
        Assert.Equal(expectedComment, invalidLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Excluding_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true,
                IncludeComments = false
            }.ToImmutableOrDefault()
        );

        const string line = "invalid # Some comment";
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        var invalidLine = Assert.IsType<InvalidLine>(deserializedLine);
        Assert.Null(invalidLine.OriginalComment);
    }
    
    [Theory]
    [InlineData("the-value # The comment", "the-value # The comment")]
    [InlineData(" the-value # The comment", " the-value # The comment")]
    [InlineData("the-value # The comment ", "the-value # The comment ")]
    [InlineData(" the-value # The comment ", " the-value # The comment ")]
    public void WHEN_Line_has_been_deserialized_THEN_Full_robots_txt_line_is_set(string line, string expectedFullLine)
    {
        // Arrange
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: new Dictionary<string, ILineDeserializer<Line>>(),
            options: new RobotsDeserializerOptions
            {
                IncludeInvalidLines = true
            }.ToImmutableOrDefault()
        );
        
        // Act
        var deserializedLine = coordinator.Deserialize(line);
        
        // Assert
        Assert.Equal(expectedFullLine, deserializedLine.FullLine);
    }
}