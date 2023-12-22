using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class InvalidSitemapDeserializationTests
{
    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.InvalidOnly)]
    public void WHEN_Line_is_invalid_sitemap_WHILE_Including_invalid_sitemaps_THEN_Deserialize_to_invalid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string invalidSitemapValue = "invalid sitemap";
        
        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(value: invalidSitemapValue, comment: null);
        
        // Assert
        Assert.IsType<InvalidSitemap>(sitemap);
    }
    
    [Theory]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Line_is_invalid_sitemap_WHILE_Excluding_invalid_sitemaps_THEN_Do_not_deserialize_to_invalid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string invalidSitemapValue = "invalid sitemap";
        
        // Act
        var sitemap = sitemapLineDeserializer.Deserialize(value: invalidSitemapValue, comment: null);
        
        // Assert
        Assert.Null(sitemap);
    }
    
    [Theory]
    [InlineData("invalid sitemap", "invalid sitemap")]
    [InlineData(" invalid sitemap", " invalid sitemap")]
    [InlineData("invalid sitemap ", "invalid sitemap ")]
    [InlineData(" invalid sitemap ", " invalid sitemap ")]
    public void WHEN_Value_is_invalid_uri_THEN_Value_is_set_to_provided_value(string sitemapValue, string expectedSitemapValue)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.InvalidOnly
        );
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(value: sitemapValue, comment: null);
        
        // Assert
        var validSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        Assert.Equal(expectedSitemapValue, validSitemap.SitemapValue);
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("  ", "  ")]
    [InlineData("Some comment", "Some comment")]
    public void WHEN_Comment_is_set_THEN_Comment_is_set_to_provided_value(string? comment, string? expectedComment)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.InvalidOnly
        );

        const string sitemapValue = "invalid sitemap";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(value: sitemapValue, comment: comment);
        
        // Assert
        var validSitemap = Assert.IsType<InvalidSitemap>(deserializedSitemap);
        Assert.Equal(expectedComment, validSitemap.Comment);
    }
}