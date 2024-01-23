using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Sitemap.Deserialization;

public sealed class InvalidSitemapDeserializationTests
{
    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.InvalidOnly)]
    public void WHEN_Value_is_invalid_sitemap_WHILE_Including_invalid_sitemaps_THEN_Deserialize_to_invalid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string directive = "sitemap";
        const string value = "invalid sitemap";
        
        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: null);
        
        // Assert
        Assert.IsType<InvalidSitemap>(sitemap);
    }
    
    [Theory]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Value_is_invalid_sitemap_WHILE_Excluding_invalid_sitemaps_THEN_Do_not_deserialize_to_invalid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string directive = "sitemap";
        const string value = "invalid sitemap";
        
        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: null);
        
        // Assert
        Assert.Null(sitemap);
    }
    
    [Theory]
    [InlineData("sitemap", "sitemap", "sitemap")]
    [InlineData(" sitemap", "sitemap", " sitemap")]
    [InlineData("sitemap ", "sitemap", "sitemap ")]
    [InlineData(" sitemap ", "sitemap", " sitemap ")]
    public void WHEN_Directive_is_set_on_invalid_sitemap_THEN_Directive_is_set_on_returned_invalid_sitemap(string directive, string expectedDirective, string expectedOriginalDirective)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.InvalidOnly
        );
        
        const string value = "invalid sitemap";
        const string? comment = null;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedDirective, invalidSitemap.Directive);
        Assert.Equal(expectedOriginalDirective, invalidSitemap.OriginalDirective);
    }
    
    [Theory]
    [InlineData("invalid sitemap", "invalid sitemap", "invalid sitemap")]
    [InlineData(" invalid sitemap", "invalid sitemap"," invalid sitemap")]
    [InlineData("invalid sitemap ", "invalid sitemap","invalid sitemap ")]
    [InlineData(" invalid sitemap ", "invalid sitemap"," invalid sitemap ")]
    public void WHEN_Value_is_invalid_uri_THEN_Value_is_set_on_returned_invalid_sitemap(string value, string expectedValue, string expectedOriginalValue)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.InvalidOnly
        );
        
        const string directive = "sitemap";
        const string? comment = null;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedValue, invalidSitemap.Value);
        Assert.Equal(expectedOriginalValue, invalidSitemap.OriginalValue);
    }
    
    [Theory]
    [InlineData(null, null, null)]
    [InlineData("", "", "")]
    [InlineData("  ", "", "  ")]
    [InlineData(" Some comment", "Some comment", " Some comment")]
    public void WHEN_Comment_is_set_on_invalid_sitemap_THEN_Comment_is_set_on_returned_invalid_sitemap(string? comment, string? expectedComment, string? expectedOriginalComment)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.InvalidOnly
        );

        const string directive = "sitemap";
        const string value = "invalid sitemap";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        var invalidSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedOriginalComment, invalidSitemap.OriginalComment);
        Assert.Equal(expectedComment, invalidSitemap.Comment);
    }
}