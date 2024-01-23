using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class InvalidSitemapFullLineTests
{
    [Fact]
    public void WHEN_Creating_instance_without_full_line_THEN_Full_line_is_null()
    {
        // Arrange
        const string directive = "sitemap";
        const string value = "Invalid";
        
        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, value: value, comment: null);

        // Assert
        Assert.Null(invalidSitemap.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_null_THEN_Full_line_is_null()
    {
        // Arrange
        const string directive = "sitemap";
        const string value = "Invalid";
        const string? fullLine = null;
        
        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, value: value, fullLine: fullLine);

        // Assert
        Assert.Null(invalidSitemap.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_string_THEN_Full_line_is_set()
    {
        // Arrange
        const string directive = "sitemap";
        const string value = "Invalid";
        const string fullLine = "Sitemap: Invalid ";

        const string expectedFullLine = "Sitemap: Invalid ";
        
        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, value: value, fullLine: fullLine);

        // Assert
        Assert.Equal(expectedFullLine, invalidSitemap.FullLine);
    }
}