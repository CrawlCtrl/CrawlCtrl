using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializeInvalidSitemapTests
{
    private readonly SitemapLineDeserializer _sitemapLineDeserializer = new ();
    private readonly ImmutableRobotsDeserializerOptions _options =
        new RobotsDeserializerOptions { SitemapPolicy = SitemapPolicy.OnlyInvalid }.ToImmutableOrDefault();
    
    private const string SitemapDirective = "sitemap";
    private const string InvalidSitemapValue = "";
    
    [Theory]
    [InlineData("sitemap")]
    [InlineData(" sitemap")]
    [InlineData("sitemap ")]
    [InlineData(" sitemap ")]
    public void WHEN_Directive_is_set_WHILE_Deserializing_invalid_sitemap_THEN_Set_directive_on_invalid_sitemap(string directive)
    {
        // Arrange
        var expectedDirective = directive;
        
        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: directive, value: InvalidSitemapValue, comment: null, line: string.Empty, _options);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedDirective, invalidSitemap.OriginalDirective);
    }
    
    [Fact]
    public void WHEN_Directive_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        const string? directive = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _sitemapLineDeserializer.Deserialize(directive: directive, value: InvalidSitemapValue, comment: null, line: string.Empty, _options)
        );

        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Theory]
    [InlineData(SitemapPolicy.All)]
    [InlineData(SitemapPolicy.OnlyInvalid)]
    public void WHEN_Value_is_invalid_sitemap_WHILE_Including_invalid_sitemaps_THEN_Deserialize_invalid_sitemap(SitemapPolicy sitemapPolicy)
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            SitemapPolicy = sitemapPolicy
        }.ToImmutableOrDefault();

        // Act
        var sitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: string.Empty, options);
        
        // Assert
        Assert.IsType<InvalidSitemap>(sitemap);
    }
    
    [Theory]
    [InlineData(SitemapPolicy.OnlyValid)]
    public void WHEN_Value_is_invalid_sitemap_WHILE_Excluding_invalid_sitemaps_THEN_Return_null(SitemapPolicy sitemapPolicy)
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            SitemapPolicy = sitemapPolicy
        }.ToImmutableOrDefault();

        // Act
        var sitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: string.Empty, options);
        
        // Assert
        Assert.Null(sitemap);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("invalid sitemap")]
    [InlineData(" invalid sitemap")]
    [InlineData("invalid sitemap ")]
    [InlineData(" invalid sitemap ")]
    public void WHEN_Value_is_set_WHILE_Deserializing_invalid_sitemap_THEN_Set_value_on_invalid_sitemap(string value)
    {
        // Arrange
        var expectedValue = value;
        
        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty, _options);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedValue, invalidSitemap.OriginalValue);
    }
    
    [Fact]
    public void WHEN_Value_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty, _options)
        );

        // Assert
        Assert.Equal("value", exception.ParamName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some comment")]
    [InlineData(" Some comment")]
    [InlineData("Some comment ")]
    [InlineData(" Some comment ")]
    public void WHEN_Comment_is_set_WHILE_Deserializing_invalid_sitemap_THEN_Set_comment_on_invalid_sitemap(string comment)
    {
        // Arrange
        var expectedComment = comment;

        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: comment, line: string.Empty, _options);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedComment, invalidSitemap.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Comment_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Set_comment_on_invalid_sitemap()
    {
        // Arrange
        const string? comment = null;
        const string? expectedComment = comment;

        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: comment, line: string.Empty, _options);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedComment, invalidSitemap.OriginalComment);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(" Some comment ")]
    public void WHEN_Line_is_set_WHILE_Deserializing_invalid_sitemap_THEN_Set_full_line_on_invalid_sitemap(string line)
    {
        // Arrange
        var expectedLine = line;

        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: line, _options);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedLine, invalidSitemap.FullLine);
    }

    [Fact]
    public void WHEN_Line_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        const string? line = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: line, _options)
        );

        // Assert
        Assert.Equal("line", exception.ParamName);
    }
}