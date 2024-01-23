using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Sitemap.Deserialization;

public sealed class ValidSitemapDeserializationTests
{
    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Value_is_valid_sitemap_WHILE_Including_valid_sitemaps_THEN_Deserialize_to_valid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string directive = "sitemap";
        const string value = "https://www.example.com/sitemap.xml";
        const string? comment = null;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        Assert.IsType<ValidSitemap>(deserializedSitemap);
    }
    
    [Theory]
    [InlineData(InclusionScope.InvalidOnly)]
    public void WHEN_Value_is_valid_sitemap_WHILE_Excluding_valid_sitemaps_THEN_Do_not_deserialize_to_valid_sitemap(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: inclusionScope
        );

        const string directive = "sitemap";
        const string value = "https://www.example.com/sitemap.xml";
        const string? comment = null;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        Assert.Null(deserializedSitemap);
    }
    
    [Theory]
    [InlineData("sitemap", "sitemap", "sitemap")]
    [InlineData(" sitemap", "sitemap", " sitemap")]
    [InlineData("sitemap ", "sitemap", "sitemap ")]
    [InlineData(" sitemap ", "sitemap", " sitemap ")]
    public void WHEN_Directive_is_set_on_valid_sitemap_THEN_Directive_is_set_on_returned_valid_sitemap(string directive, string expectedDirective, string expectedOriginalDirective)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.ValidOnly
        );
        
        const string value = "https://www.example.com/sitemap.xml";
        const string? comment = null;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedDirective, validSitemap.Directive);
        Assert.Equal(expectedOriginalDirective, validSitemap.OriginalDirective);
    }

    [Theory]
    [MemberData(nameof(WHEN_Value_is_valid_uri_THEN_Sitemap_uri_is_set_to_uri_DATA))]
    public void WHEN_Value_is_valid_uri_THEN_Sitemap_uri_is_set_to_uri(string value, Uri expectedSitemapUri)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.ValidOnly
        );
        
        const string directive = "sitemap";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: null);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedSitemapUri, validSitemap.Uri);
    }
    
    public static IEnumerable<object[]> WHEN_Value_is_valid_uri_THEN_Sitemap_uri_is_set_to_uri_DATA =>
        new List<object[]>
        {
            new object[] { "https://www.example.com/sitemap.xml", new Uri("https://www.example.com/sitemap.xml") },
            new object[] { " https://www.example.com/sitemap.xml", new Uri("https://www.example.com/sitemap.xml") },
            new object[] { "https://www.example.com/sitemap.xml ", new Uri("https://www.example.com/sitemap.xml") },
            new object[] { " https://www.example.com/sitemap.xml ", new Uri("https://www.example.com/sitemap.xml") }
        };
    
    [Theory]
    [InlineData("https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData(" https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml"," https://www.example.com/sitemap.xml")]
    [InlineData("https://www.example.com/sitemap.xml ", "https://www.example.com/sitemap.xml","https://www.example.com/sitemap.xml ")]
    [InlineData(" https://www.example.com/sitemap.xml ", "https://www.example.com/sitemap.xml"," https://www.example.com/sitemap.xml ")]
    public void WHEN_Value_is_valid_uri_THEN_Value_is_set_on_returned_valid_sitemap(string value, string expectedValue, string expectedOriginalValue)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.ValidOnly
        );
        
        const string directive = "sitemap";
        const string? comment = null;
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedValue, validSitemap.Value);
        Assert.Equal(expectedOriginalValue, validSitemap.OriginalValue);
    }
    
    [Theory]
    [InlineData(null, null, null)]
    [InlineData("", "", "")]
    [InlineData("  ", "", "  ")]
    [InlineData(" Some comment", "Some comment", " Some comment")]
    public void WHEN_Comment_is_set_on_valid_sitemap_THEN_Comment_is_set_on_returned_valid_sitemap(string? comment, string? expectedComment, string? expectedOriginalComment)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(
            inclusionScope: InclusionScope.ValidOnly
        );

        const string directive = "sitemap";
        const string value = "https://www.example.com/sitemap.xml";
        
        // Act
        var deserializedSitemap = sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment);
        
        // Assert
        var validSitemap = Assert.IsType<ValidSitemap>(deserializedSitemap);
        
        Assert.Equal(expectedOriginalComment, validSitemap.OriginalComment);
        Assert.Equal(expectedComment, validSitemap.Comment);
    }
}