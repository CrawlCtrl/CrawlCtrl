using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializeValidSitemapTests
{
    private readonly SitemapLineDeserializer _sitemapLineDeserializer = new ();
    private readonly ImmutableRobotsDeserializerOptions _options =
        new RobotsDeserializerOptions { SitemapPolicy = SitemapPolicy.OnlyValid }.ToImmutableOrDefault();
    
    private const string SitemapDirective = "sitemap";
    private const string ValidSitemapValue = "https://www.example.com/sitemap.xml";
    
    [Theory]
    [InlineData("sitemap")]
    [InlineData(" sitemap")]
    [InlineData("sitemap ")]
    [InlineData(" sitemap ")]
    public void WHEN_Directive_is_set_WHILE_Deserializing_valid_sitemap_THEN_Set_directive_on_valid_sitemap(string directive)
    {
        // Arrange
        var expectedDirective = directive;
        
        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: directive, value: ValidSitemapValue, comment: null, line: string.Empty, _options);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedDirective, validSitemap.OriginalDirective);
    }
    
    [Fact]
    public void WHEN_Directive_is_null_WHILE_Deserializing_valid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        const string? directive = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _sitemapLineDeserializer.Deserialize(directive: directive, value: ValidSitemapValue, comment: null, line: string.Empty, _options)
        );

        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Theory]
    [InlineData(SitemapPolicy.All)]
    [InlineData(SitemapPolicy.OnlyValid)]
    public void WHEN_Value_is_valid_sitemap_WHILE_Including_valid_sitemaps_THEN_Deserialize_valid_sitemap(SitemapPolicy sitemapPolicy)
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            SitemapPolicy = sitemapPolicy
        }.ToImmutableOrDefault();
        
        // Act
        var sitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: string.Empty, options);
        
        // Assert
        Assert.IsType<ValidSitemap>(sitemap);
    }
    
    [Theory]
    [InlineData(SitemapPolicy.OnlyInvalid)]
    public void WHEN_Value_is_valid_sitemap_WHILE_Excluding_valid_sitemaps_THEN_Return_null(SitemapPolicy sitemapPolicy)
    {
        // Arrange
        var options = new RobotsDeserializerOptions
        {
            SitemapPolicy = sitemapPolicy
        }.ToImmutableOrDefault();

        // Act
        var sitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: string.Empty, options);
        
        // Assert
        Assert.Null(sitemap);
    }
    
    [Theory]
    [InlineData("https://www.example.com/sitemap.xml")]
    [InlineData(" https://www.example.com/sitemap.xml")]
    [InlineData("https://www.example.com/sitemap.xml ")]
    [InlineData(" https://www.example.com/sitemap.xml ")]
    public void WHEN_Value_is_set_WHILE_Deserializing_valid_sitemap_THEN_Set_value_on_valid_sitemap(string value)
    {
        // Arrange
        var expectedValue = value;
        
        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty, _options);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedValue, validSitemap.OriginalValue);
    }
    
    [Fact]
    public void WHEN_Value_is_set_WHILE_Deserializing_valid_sitemap_THEN_Set_rui_on_valid_sitemap()
    {
        // Arrange
        const string value = "https://www.example.com/sitemap.xml";
        var expectedUri = new Uri(value);
        
        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty, _options);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedUri, validSitemap.Uri);
    }
    
    [Fact]
    public void WHEN_Value_is_null_WHILE_Deserializing_valid_sitemap_THEN_Throw_exception()
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
    public void WHEN_Comment_is_set_WHILE_Deserializing_valid_sitemap_THEN_Set_comment_on_valid_sitemap(string comment)
    {
        // Arrange
        var expectedComment = comment;

        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: comment, line: string.Empty, _options);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedComment, validSitemap.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Comment_is_null_WHILE_Deserializing_valid_sitemap_THEN_Set_comment_on_valid_sitemap()
    {
        // Arrange
        const string? comment = null;
        const string? expectedComment = comment;

        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: comment, line: string.Empty, _options);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedComment, validSitemap.OriginalComment);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(" Some comment ")]
    public void WHEN_Line_is_set_WHILE_Deserializing_valid_sitemap_THEN_Set_full_line_on_valid_sitemap(string line)
    {
        // Arrange
        var expectedLine = line;

        // Act
        var deserializedSitemap = _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: line, _options);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedLine, validSitemap.FullLine);
    }

    [Fact]
    public void WHEN_Line_is_null_WHILE_Deserializing_valid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        const string? line = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            _sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: line, _options)
        );

        // Assert
        Assert.Equal("line", exception.ParamName);
    }
}