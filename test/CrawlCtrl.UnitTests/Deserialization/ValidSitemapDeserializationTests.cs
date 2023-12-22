using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class ValidSitemapDeserializationTests
{
    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Line_is_valid_sitemap_WHILE_Including_valid_sitemaps_THEN_Deserialize_to_valid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string validSitemapValue = "https://www.example.com/sitemap.xml";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(value: validSitemapValue, comment: null);
        
        // Assert
        Assert.IsType<ValidSitemap>(deserializedSitemap);
    }
    
    [Theory]
    [InlineData(InclusionScope.InvalidOnly)]
    public void WHEN_Line_is_valid_sitemap_WHILE_Excluding_valid_sitemaps_THEN_Do_not_deserialize_to_valid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string validSitemapValue = "https://www.example.com/sitemap.xml";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(value: validSitemapValue, comment: null);
        
        // Assert
        Assert.Null(deserializedSitemap);
    }

    [Theory]
    [MemberData(nameof(WHEN_Value_is_valid_uri_trimmed_THEN_Value_is_set_to_uri_DATA))]
    public void WHEN_Value_is_valid_uri_trimmed_THEN_Value_is_set_to_uri(string value, Uri expectedSitemapUri)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.ValidOnly
        );

        const string validSitemapValue = "https://www.example.com/sitemap.xml";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(value: validSitemapValue, comment: null);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        Assert.Equal(expectedSitemapUri, validSitemap.SitemapUri);
    }
    
    public static IEnumerable<object[]> WHEN_Value_is_valid_uri_trimmed_THEN_Value_is_set_to_uri_DATA =>
        new List<object[]>
        {
            new object[] { "https://www.example.com/sitemap.xml", new Uri("https://www.example.com/sitemap.xml") },
            new object[] { " https://www.example.com/sitemap.xml", new Uri("https://www.example.com/sitemap.xml") },
            new object[] { "https://www.example.com/sitemap.xml ", new Uri("https://www.example.com/sitemap.xml") },
            new object[] { " https://www.example.com/sitemap.xml ", new Uri("https://www.example.com/sitemap.xml") }
        };
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("  ", "  ")]
    [InlineData("Some comment", "Some comment")]
    public void WHEN_Comment_is_set_THEN_Comment_is_set_to_provided_value(string? comment, string? expectedComment)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.ValidOnly
        );

        const string sitemapValue = "https://www.example.com/sitemap.xml";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(value: sitemapValue, comment: comment);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        Assert.Equal(expectedComment, validSitemap.Comment);
    }
}