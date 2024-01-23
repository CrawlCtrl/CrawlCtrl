using Xunit;

namespace CrawlCtrl.UnitTests.Sitemap;

public sealed class ValidSitemapValueTests
{
    [Theory]
    [InlineData("https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData(" https://www.example.com/sitemap.xml", " https://www.example.com/sitemap.xml")]
    [InlineData("https://www.example.com/sitemap.xml ", "https://www.example.com/sitemap.xml ")]
    [InlineData(" https://www.example.com/sitemap.xml ", " https://www.example.com/sitemap.xml ")]
    public void WHEN_Created_with_uri_THEN_Original_value_is_set_to_original_string(string uriString, string expectedOriginalValue)
    {
        // Arrange
        var uri = new Uri(uriString);
        
        // Act
        var validSitemap = new ValidSitemap(uri: uri);

        // Assert
        Assert.Equal(expectedOriginalValue, validSitemap.OriginalValue);
    }
    
    [Theory]
    [InlineData("https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData(" https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData("https://www.example.com/sitemap.xml ", "https://www.example.com/sitemap.xml")]
    [InlineData(" https://www.example.com/sitemap.xml ", "https://www.example.com/sitemap.xml")]
    public void WHEN_Created_with_uri_THEN_Value_is_set_to_whitespace_trimmed_original_uri_string(string uriString, string expectedValue)
    {
        // Arrange
        var uri = new Uri(uriString);
        
        // Act
        var validSitemap = new ValidSitemap(uri: uri);

        // Assert
        Assert.Equal(expectedValue, validSitemap.Value);
    }

    [Fact]
    public void WHEN_Created_with_uri_THEN_Uri_is_set_to_same_value()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");

        // Act
        var validSitemap = new ValidSitemap(uri: uri);
        
        // Assert
        Assert.Equal(uri, validSitemap.Uri);
    }
    
    [Fact]
    public void WHEN_Created_with_uri_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        Uri? uri = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new ValidSitemap(uri: uri));

        // Assert
        Assert.Equal("uri", exception.ParamName);
    }
}