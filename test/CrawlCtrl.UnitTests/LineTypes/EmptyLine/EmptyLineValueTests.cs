using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization.EmptyLine;

public sealed class EmptyLineValueTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "  ")]
    public void WHEN_Created_with_empty_value_THEN_Original_value_is_set_to_provided_value(string value, string expectedOriginalValue)
    {
        // Arrange

        // Act
        var emptyLine = new CrawlCtrl.EmptyLine(value: value, comment: null);

        // Assert
        Assert.Equal(expectedOriginalValue, emptyLine.OriginalValue);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "")]
    public void WHEN_Created_with_empty_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange

        // Act
        var emptyLine = new CrawlCtrl.EmptyLine(value: value, comment: null);

        // Assert
        Assert.Equal(expectedValue, emptyLine.Value);
    }

    [Fact]
    public void WHEN_Created_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new CrawlCtrl.EmptyLine(value: value, comment: null));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_with_non_empty_value_THEN_Throw_exception()
    {
        // Arrange
        const string value = "Hello, World!";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new CrawlCtrl.EmptyLine(value: value, comment: null));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }
}