using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes.ValidSitemap;

public sealed class ValidSitemapFullLineTests
{
    [Fact]
    public void WHEN_Creating_instance_without_full_line_THEN_Full_line_is_null()
    {
        // Arrange
        const string directive = "sitemap";
        var sitemapUri = new Uri("https://www.example.com/sitemap.xml");
        
        // Act
        var validSitemap = new CrawlCtrl.ValidSitemap(directive: directive, uri: sitemapUri, comment: null);

        // Assert
        Assert.Null(validSitemap.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_null_THEN_Full_line_is_null()
    {
        // Arrange
        const string directive = "sitemap";
        var sitemapUri = new Uri("https://www.example.com/sitemap.xml");
        const string? fullLine = null;
        
        // Act
        var validSitemap = new CrawlCtrl.ValidSitemap(directive: directive, uri: sitemapUri, fullLine: fullLine);

        // Assert
        Assert.Null(validSitemap.FullLine);
    }
    
    [Fact]
    public void WHEN_Creating_instance_with_full_line_set_to_string_THEN_Full_line_is_set()
    {
        // Arrange
        const string directive = "sitemap";
        var sitemapUri = new Uri("https://www.example.com/sitemap.xml");
        const string fullLine = "Sitemap: Invalid ";

        const string expectedFullLine = "Sitemap: Invalid ";
        
        // Act
        var validSitemap = new CrawlCtrl.ValidSitemap(directive: directive, uri: sitemapUri, fullLine: fullLine);

        // Assert
        Assert.Equal(expectedFullLine, validSitemap.FullLine);
    }
}