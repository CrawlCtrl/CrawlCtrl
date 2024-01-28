using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes.UnknownDirective;

public sealed class UnknownDirectiveFullLineTests
{
    [Fact]
    public void WHEN_Creating_instance_without_full_line_THEN_Full_line_is_null()
    {
        // Arrange
        const string directive = "unknown";
        const string value = "Some value";
        
        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, comment: null);

        // Assert
        Assert.Null(unknownDirective.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_null_THEN_Full_line_is_null()
    {
        // Arrange
        const string directive = "unknown";
        const string value = "Some value";
        const string? fullLine = null;
        
        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, fullLine: fullLine);

        // Assert
        Assert.Null(unknownDirective.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_string_THEN_Full_line_is_set()
    {
        // Arrange
        const string directive = "unknown";
        const string value = "Some value";
        const string fullLine = "unknown: Some value ";

        const string expectedFullLine = "unknown: Some value ";
        
        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, fullLine: fullLine);

        // Assert
        Assert.Equal(expectedFullLine, unknownDirective.FullLine);
    }
}