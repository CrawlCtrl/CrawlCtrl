using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Sitemap.Deserialization;

public sealed class DeserializingValidSitemapTests
{
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        var expectedDirective = directive;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: ValidSitemapValue, comment: null, line: string.Empty);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedDirective, validSitemap.OriginalDirective);
    }
    
    [Fact]
    public void WHEN_Directive_is_null_WHILE_Deserializing_valid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        const string? directive = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            sitemapLineDeserializer.Deserialize(directive: directive, value: ValidSitemapValue, comment: null, line: string.Empty)
        );

        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Value_is_valid_sitemap_WHILE_Including_valid_sitemaps_THEN_Deserialize_valid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: string.Empty);
        
        // Assert
        Assert.IsType<ValidSitemap>(sitemap);
    }
    
    [Theory]
    [InlineData(InclusionScope.InvalidOnly)]
    public void WHEN_Value_is_valid_sitemap_WHILE_Excluding_valid_sitemaps_THEN_Return_null(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: string.Empty);
        
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        var expectedValue = value;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedValue, validSitemap.OriginalValue);
    }
    
    [Fact]
    public void WHEN_Value_is_set_WHILE_Deserializing_valid_sitemap_THEN_Set_rui_on_valid_sitemap()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        const string value = "https://www.example.com/sitemap.xml";
        var expectedUri = new Uri(value);
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedUri, validSitemap.Uri);
    }
    
    [Fact]
    public void WHEN_Value_is_null_WHILE_Deserializing_valid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty)
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        var expectedComment = comment;

        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: comment, line: string.Empty);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedComment, validSitemap.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Comment_is_null_WHILE_Deserializing_valid_sitemap_THEN_Set_comment_on_valid_sitemap()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        const string? comment = null;
        const string? expectedComment = comment;

        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: comment, line: string.Empty);
        
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        var expectedLine = line;

        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: line);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedLine, validSitemap.FullLine);
    }

    [Fact]
    public void WHEN_Line_is_null_WHILE_Deserializing_valid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.ValidOnly);

        const string? line = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: ValidSitemapValue, comment: null, line: line)
        );

        // Assert
        Assert.Equal("line", exception.ParamName);
    }
}