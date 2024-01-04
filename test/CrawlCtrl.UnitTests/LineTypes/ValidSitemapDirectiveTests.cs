using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class ValidSitemapDirectiveTests
{
    [Theory]
    [MemberData(nameof(DirectiveTestData.OriginalDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_THEN_Original_directive_is_set_to_provided_directive(string directive, string expectedOriginalDirective)
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");

        // Act
        var validSitemap = new ValidSitemap(directive: directive, uri: uri);

        // Assert
        Assert.Equal(expectedOriginalDirective, validSitemap.OriginalDirective);
    }
    
    [Theory]
    [MemberData(nameof(DirectiveTestData.TrimmedDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_THEN_Directive_is_set_to_whitespace_trimmed_directive(string directive, string expectedDirective)
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");

        // Act
        var validSitemap = new ValidSitemap(directive: directive, uri: uri);

        // Assert
        Assert.Equal(expectedDirective, validSitemap.Directive);
    }
    
    [Theory]
    [MemberData(nameof(DirectiveTestData.EmptyDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_set_to_empty_string_THEN_Throw_exception(string directive)
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new ValidSitemap(directive: directive, uri: uri));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Created_with_directive_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? directive = null;
        var uri = new Uri("https://www.example.com/sitemap.xml");
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new ValidSitemap(directive: directive, uri: uri));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Create_without_directive_THEN_Original_directive_is_set_to_default_sitemap_directive()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        
        var expectedDirective = Constants.Directives.Sitemap;

        // Act
        var validSitemap = new ValidSitemap(uri: uri);

        // Assert
        Assert.Equal(expectedDirective, validSitemap.OriginalDirective);
    }
    
    [Fact]
    public void WHEN_Create_without_directive_THEN_Directive_is_set_to_default_sitemap_directive()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        
        var expectedDirective = Constants.Directives.Sitemap;

        // Act
        var validSitemap = new ValidSitemap(uri: uri);

        // Assert
        Assert.Equal(expectedDirective, validSitemap.Directive);
    }
}