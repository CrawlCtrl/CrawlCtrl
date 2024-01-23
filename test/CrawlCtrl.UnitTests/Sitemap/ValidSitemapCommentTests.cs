using CrawlCtrl.UnitTests.TestData;
using Xunit;

namespace CrawlCtrl.UnitTests.Sitemap;

public sealed class ValidSitemapCommentTests
{
    [Theory]
    [MemberData(nameof(CommentTestData.OriginalCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string comment, string expectedOriginalComment)
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");

        // Act
        var validSitemap = new ValidSitemap(uri: uri, comment: comment);

        // Assert
        Assert.Equal(expectedOriginalComment, validSitemap.OriginalComment);
    }
    
    [Theory]
    [MemberData(nameof(CommentTestData.TrimmedCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string comment, string expectedComment)
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");

        // Act
        var validSitemap = new ValidSitemap(uri: uri, comment: comment);

        // Assert
        Assert.Equal(expectedComment, validSitemap.Comment);
    }
    
    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var validSitemap = new ValidSitemap(uri: uri, comment: comment);

        // Assert
        Assert.Equal(expectedComment, validSitemap.OriginalComment);
    }

    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Comment_is_null()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var validSitemap = new ValidSitemap(uri: uri, comment: comment);

        // Assert
        Assert.Equal(expectedComment, validSitemap.Comment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        
        // Act
        var validSitemap = new ValidSitemap(uri: uri);
        
        // Assert
        Assert.Null(validSitemap.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_null()
    {
        // Arrange
        var uri = new Uri("https://www.example.com/sitemap.xml");
        
        // Act
        var validSitemap = new ValidSitemap(uri: uri);
        
        // Assert
        Assert.Null(validSitemap.Comment);
    }
}