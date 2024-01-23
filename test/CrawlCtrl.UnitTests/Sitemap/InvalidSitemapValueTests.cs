using Xunit;

namespace CrawlCtrl.UnitTests.Sitemap;

public sealed class InvalidSitemapValueTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "  ")]
    [InlineData("Some value", "Some value")]
    [InlineData(" Some value", " Some value")]
    [InlineData("Some value ", "Some value ")]
    [InlineData(" Some value ", " Some value ")]
    public void WHEN_Created_with_value_THEN_Original_value_is_set_to_provided_value(string value, string expectedOriginalValue)
    {
        // Arrange

        // Act
        var invalidSitemap = new InvalidSitemap(value: value);

        // Assert
        Assert.Equal(expectedOriginalValue, invalidSitemap.OriginalValue);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "")]
    [InlineData("Some value", "Some value")]
    [InlineData(" Some value", "Some value")]
    [InlineData("Some value ", "Some value")]
    [InlineData(" Some value ", "Some value")]
    public void WHEN_Created_with_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange

        // Act
        var invalidSitemap = new InvalidSitemap(value: value);

        // Assert
        Assert.Equal(expectedValue, invalidSitemap.Value);
    }
    
    [Fact]
    public void WHEN_Create_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidSitemap(value: value));

        // Assert
        Assert.Equal("value", exception.ParamName);
    }
}