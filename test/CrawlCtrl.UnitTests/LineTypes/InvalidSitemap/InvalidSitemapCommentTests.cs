using CrawlCtrl.UnitTests.TestData;
using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes.InvalidSitemap;

public sealed class InvalidSitemapCommentTests
{
    [Theory]
    [MemberData(nameof(CommentTestData.OriginalCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string comment, string expectedOriginalComment)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidSitemap = new CrawlCtrl.InvalidSitemap(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedOriginalComment, invalidSitemap.OriginalComment);
    }
    
    [Theory]
    [MemberData(nameof(CommentTestData.TrimmedCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string comment, string expectedComment)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidSitemap = new CrawlCtrl.InvalidSitemap(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidSitemap.Comment);
    }
    
    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var invalidSitemap = new CrawlCtrl.InvalidSitemap(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidSitemap.OriginalComment);
    }

    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var invalidSitemap = new CrawlCtrl.InvalidSitemap(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidSitemap.Comment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var invalidSitemap = new CrawlCtrl.InvalidSitemap(value: value);
        
        // Assert
        Assert.Null(invalidSitemap.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var invalidSitemap = new CrawlCtrl.InvalidSitemap(value: value);
        
        // Assert
        Assert.Null(invalidSitemap.Comment);
    }
}