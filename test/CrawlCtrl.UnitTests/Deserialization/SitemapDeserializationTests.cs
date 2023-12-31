using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class SitemapDeserializationTests
{
    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.InvalidOnly)]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Directive_is_null_THEN_Throw_exception(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(inclusionScope);

        const string? directive = null;
        const string value = "sitemap value";
        const string comment = "Some comment";
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment));

        // Assert
        Assert.Equal("directive", exception.ParamName);
    }
    
    [Theory]
    [InlineData(InclusionScope.All)]
    [InlineData(InclusionScope.InvalidOnly)]
    [InlineData(InclusionScope.ValidOnly)]
    public void WHEN_Value_is_null_THEN_Throw_exception(InclusionScope inclusionScope)
    {
        // Arrange
        var sitemapLineDeserializer = new SitemapLineDeserializer(inclusionScope);

        const string directive = "directive";
        const string? value = null;
        const string comment = "Some comment";
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => sitemapLineDeserializer.Deserialize(directive: directive, value: value, comment: comment));

        // Assert
        Assert.Equal("value", exception.ParamName);
    }
}