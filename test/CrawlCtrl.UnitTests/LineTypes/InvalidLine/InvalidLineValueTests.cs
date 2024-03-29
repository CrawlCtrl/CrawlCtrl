using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes.InvalidLine;

public sealed class InvalidLineValueTests
{
    [Theory]
    [InlineData("Some value", "Some value")]
    [InlineData(" Some value", " Some value")]
    [InlineData("Some value ", "Some value ")]
    [InlineData(" Some value ", " Some value ")]
    public void WHEN_Created_with_value_THEN_Original_value_is_set_to_provided_value(string value, string expectedOriginalValue)
    {
        // Arrange

        // Act
        var invalidLine = new CrawlCtrl.InvalidLine(value: value, comment: null);

        // Assert
        Assert.Equal(expectedOriginalValue, invalidLine.OriginalValue);
    }
    
    [Theory]
    [InlineData("Some value", "Some value")]
    [InlineData(" Some value", "Some value")]
    [InlineData("Some value ", "Some value")]
    [InlineData(" Some value ", "Some value")]
    public void WHEN_Created_with_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange

        // Act
        var invalidLine = new CrawlCtrl.InvalidLine(value: value, comment: null);

        // Assert
        Assert.Equal(expectedValue, invalidLine.Value);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void WHEN_Created_with_empty_value_THEN_Throw_exception(string value)
    {
        // Arrange
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new CrawlCtrl.InvalidLine(value: value, comment: null));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new CrawlCtrl.InvalidLine(value: value, comment: null));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }
}