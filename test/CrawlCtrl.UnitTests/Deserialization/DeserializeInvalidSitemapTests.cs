using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializeInvalidSitemapTests
{
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        var expectedDirective = directive;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: InvalidSitemapValue, comment: null, line: string.Empty);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedDirective, invalidSitemap.OriginalDirective);
    }
    
    [Fact]
    public void WHEN_Directive_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        const string? directive = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            sitemapLineDeserializer.Deserialize(directive: directive, value: InvalidSitemapValue, comment: null, line: string.Empty)
        );

        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.InvalidOnly)]
    public void WHEN_Value_is_invalid_sitemap_WHILE_Including_invalid_sitemaps_THEN_Deserialize_invalid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: string.Empty);
        
        // Assert
        Assert.IsType<InvalidSitemap>(sitemap);
    }
    
    [Theory]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Value_is_invalid_sitemap_WHILE_Excluding_invalid_sitemaps_THEN_Return_null(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: string.Empty);
        
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        var expectedValue = value;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: value, comment: null, line: string.Empty);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedValue, invalidSitemap.OriginalValue);
    }
    
    [Fact]
    public void WHEN_Value_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

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
    public void WHEN_Comment_is_set_WHILE_Deserializing_invalid_sitemap_THEN_Set_comment_on_invalid_sitemap(string comment)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        var expectedComment = comment;

        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: comment, line: string.Empty);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedComment, invalidSitemap.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Comment_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Set_comment_on_invalid_sitemap()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        const string? comment = null;
        const string? expectedComment = comment;

        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: comment, line: string.Empty);
        
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
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        var expectedLine = line;

        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: line);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedLine, invalidSitemap.FullLine);
    }

    [Fact]
    public void WHEN_Line_is_null_WHILE_Deserializing_invalid_sitemap_THEN_Throw_exception()
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(InclusionScope.InvalidOnly);

        const string? line = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => 
            sitemapLineDeserializer.Deserialize(directive: SitemapDirective, value: InvalidSitemapValue, comment: null, line: line)
        );

        // Assert
        Assert.Equal("line", exception.ParamName);
    }
}