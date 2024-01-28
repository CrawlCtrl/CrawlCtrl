using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes.UnknownDirective;

public sealed class UnknownDirectiveValueTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "  ")]
    [InlineData("Some value", "Some value")]
    [InlineData(" Some value", " Some value")]
    [InlineData("Some value ", "Some value ")]
    [InlineData(" Some value ", " Some value ")]
    public void WHEN_Created_with_empty_value_THEN_Original_value_is_set_to_provided_value(string value, string expectedOriginalValue)
    {
        // Arrange
        const string directive = "directive";

        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value);

        // Assert
        Assert.Equal(expectedOriginalValue, unknownDirective.OriginalValue);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "")]
    [InlineData("Some value", "Some value")]
    [InlineData(" Some value", "Some value")]
    [InlineData("Some value ", "Some value")]
    [InlineData(" Some value ", "Some value")]
    public void WHEN_Created_with_empty_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange
        const string directive = "directive";

        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value);

        // Assert
        Assert.Equal(expectedValue, unknownDirective.Value);
    }
    
    [Fact]
    public void WHEN_Create_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string directive = "directive";
        const string? value = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new CrawlCtrl.UnknownDirective(directive: directive, value: value));

        // Assert
        Assert.Equal("value", exception.ParamName);
    }
}