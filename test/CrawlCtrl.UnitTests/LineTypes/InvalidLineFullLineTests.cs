using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class InvalidLineFullLineTests
{
    [Fact]
    public void WHEN_Creating_instance_without_full_line_THEN_Full_line_is_null()
    {
        // Arrange
        const string value = "Invalid";
        
        // Act
        var invalidLine = new InvalidLine(value: value, comment: null);

        // Assert
        Assert.Null(invalidLine.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_null_THEN_Full_line_is_null()
    {
        // Arrange
        const string value = "Invalid";
        const string? fullLine = null;
        
        // Act
        var invalidLine = new InvalidLine(value: value, fullLine: fullLine);

        // Assert
        Assert.Null(invalidLine.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_string_THEN_Full_line_is_set()
    {
        // Arrange
        const string value = "Invalid";
        const string fullLine = " Invalid ";

        const string expectedFullLine = " Invalid ";
        
        // Act
        var invalidLine = new InvalidLine(value: value, fullLine: fullLine);

        // Assert
        Assert.Equal(expectedFullLine, invalidLine.FullLine);
    }
}