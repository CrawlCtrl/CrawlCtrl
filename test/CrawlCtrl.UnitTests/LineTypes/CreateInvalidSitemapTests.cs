using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateInvalidSitemapTests
{
    [Theory]
    [InlineData("Directive")]
    [InlineData(" Directive ")]
    public void WHEN_Created_with_directive_THEN_Original_directive_is_set_to_provided_value(string directive)
    {
        // Arrange
        var expectedDirective = directive;
        const string value = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: value);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.OriginalDirective);
    }
    
    [Theory]
    [InlineData("Directive", "Directive")]
    [InlineData(" Directive ", "Directive")]
    public void WHEN_Created_with_directive_THEN_Directive_is_set_to_whitespace_trimmed_value(string directive, string expectedDirective)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: value);

        // Assert
        Assert.Equal(expectedDirective, invalidSitemap.Directive);
    }
    
    [Fact]
    public void WHEN_Created_with_directive_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? directive = null;
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidSitemap(directive: directive, sitemapValue: value));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void WHEN_Created_with_directive_set_to_empty_string_THEN_Throw_exception(string directive)
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new InvalidSitemap(directive: directive, sitemapValue: value));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
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

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some random value")]
    [InlineData(" Some random value ")]
    public void WHEN_Created_with_empty_value_THEN_Original_value_is_set_to_provided_value(string value)
    {
        // Arrange
        const string directive = "testdirective";
        var expectedValue = value;

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: expectedValue);

        // Assert
        Assert.Equal(expectedValue, invalidSitemap.OriginalValue);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "")]
    [InlineData("Some random value", "Some random value")]
    [InlineData(" Some random value ", "Some random value")]
    public void WHEN_Created_with_empty_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange
        const string directive = "testdirective";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: expectedValue);

        // Assert
        Assert.Equal(expectedValue, invalidSitemap.Value);
    }
    
    [Fact]
    public void WHEN_Create_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string directive = "testdirective";
        const string? value = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidSitemap(directive: directive, sitemapValue: value));

        // Assert
        Assert.Equal("value", exception.ParamName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void WHEN_Created_with_empty_value_THEN_Throw_exception(string value)
    {
        // Arrange
        const string directive = "testdirective";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new InvalidSitemap(directive: directive, sitemapValue: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        const string directive = "testdirective";
        const string value = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: value);
        
        // Assert
        Assert.Null(invalidSitemap.Comment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some comment")]
    [InlineData(" Some comment ")]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string? comment)
    {
        // Arrange
        const string directive = "testdirective";
        const string value = "Some value";
        var expectedComment = comment;

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidSitemap.OriginalComment);
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("  ", "")]
    [InlineData("Some comment", "Some comment")]
    [InlineData(" Some comment ", "Some comment")]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string? comment, string? expectedComment)
    {
        // Arrange
        const string directive = "testdirective";
        const string value = "Some value";

        // Act
        var invalidSitemap = new InvalidSitemap(directive: directive, sitemapValue: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidSitemap.Comment);
    }
}