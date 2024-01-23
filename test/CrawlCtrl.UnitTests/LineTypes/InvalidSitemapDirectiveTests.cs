using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class InvalidSitemapDirectiveTests
{
    [Theory]
    [MemberData(nameof(DirectiveTestData.OriginalDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_THEN_Original_directive_is_set_to_provided_directive(string directive, string expectedOriginalDirective)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, value: value, comment: null);

        // Assert
        Assert.Equal(expectedOriginalDirective, invalidSitemap.OriginalDirective);
    }
    
    [Theory]
    [MemberData(nameof(DirectiveTestData.TrimmedDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_THEN_Directive_is_set_to_whitespace_trimmed_directive(string directive, string expectedDirective)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, value: value, comment: null);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.Directive);
    }
    
    [Theory]
    [MemberData(nameof(DirectiveTestData.EmptyDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_set_to_empty_string_THEN_Throw_exception(string directive)
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new InvalidSitemap(directive: directive, value: value, comment: null));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Created_with_directive_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? directive = null;
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidSitemap(directive: directive, value: value, comment: null));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Create_without_directive_THEN_Original_directive_is_set_to_default_sitemap_directive()
    {
        // Arrange
        const string value = "Some value";
        
        var expectedDirective = Constants.Directives.Sitemap;

        // Act
        var invalidSitemap = new InvalidSitemap(value: value);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.OriginalDirective);
    }
    
    [Fact]
    public void WHEN_Create_without_directive_THEN_Directive_is_set_to_default_sitemap_directive()
    {
        // Arrange
        const string value = "Some value";
        
        var expectedDirective = Constants.Directives.Sitemap;

        // Act
        var invalidSitemap = new InvalidSitemap(value: value);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.Directive);
    }
}