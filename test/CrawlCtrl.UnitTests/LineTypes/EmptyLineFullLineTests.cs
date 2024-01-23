using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class EmptyLineFullLineTests
{
    [Fact]
    public void WHEN_Creating_instance_without_full_line_THEN_Full_line_is_null()
    {
        // Arrange
        const string value = "";
        
        // Act
        var emptyLine = new EmptyLine(value: value, comment: null);

        // Assert
        Assert.Null(emptyLine.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_null_THEN_Full_line_is_null()
    {
        // Arrange
        const string value = "";
        const string? fullLine = null;
        
        // Act
        var emptyLine = new EmptyLine(value: value, fullLine: fullLine);

        // Assert
        Assert.Null(emptyLine.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_string_THEN_Full_line_is_set()
    {
        // Arrange
        const string value = "";
        const string fullLine = "  ";

        const string expectedFullLine = "  ";
        
        // Act
        var emptyLine = new EmptyLine(value: value, fullLine: fullLine);

        // Assert
        Assert.Equal(expectedFullLine, emptyLine.FullLine);
    }
}