using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateInvalidSitemapTests
{
    [Fact]
    public void WHEN_Created_with_sitemap_value_THEN_Sitemap_value_is_set_to_provided_value()
    {
        // Arrange
        const string expectedSitemapValue = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(sitemapValue: expectedSitemapValue);

        // Assert
        Assert.Equal(expectedSitemapValue, invalidSitemap.SitemapValue);
    }
    
    [Fact]
    public void WHEN_Created_with_sitemap_value_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? sitemapValue = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidSitemap(sitemapValue: sitemapValue));
        
        // Assert
        Assert.Equal("sitemapValue", exception.ParamName);
    }

    [Fact]
    public void WHEN_Create_without_directive_THEN_Directive_is_set_to_default()
    {
        // Arrange
        const string sitemapValue = "Some value";
        var expectedDirective = Constants.Directives.Sitemap;

        // Act
        var invalidSitemap = new InvalidSitemap(sitemapValue: sitemapValue);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.Directive);
    }
    
    [Fact]
    public void WHEN_Create_with_directive_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string sitemapValue = "Some value";
        const string? directive = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidSitemap(sitemapValue: sitemapValue, directive: directive));

        // Assert
        Assert.Equal("directive", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Create_with_custom_directive_THEN_Directive_is_set_to_provided_value()
    {
        // Arrange
        const string sitemapValue = "Some value";
        var expectedDirective = "SitEmAp";

        // Act
        var invalidSitemap = new InvalidSitemap(sitemapValue: sitemapValue, directive: expectedDirective);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.Directive);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        const string sitemapValue = "Some value";
        
        // Act
        var invalidSitemap = new InvalidSitemap(sitemapValue: sitemapValue);
        
        // Assert
        Assert.Null(invalidSitemap.Comment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some comment")]
    [InlineData(" Some comment ")]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_provided_value(string? comment)
    {
        // Arrange
        const string sitemapValue = "Some value";
        var expectedComment = comment;

        // Act
        var invalidSitemap =  new InvalidSitemap(sitemapValue: sitemapValue, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidSitemap.Comment);
    }
}