using CrawlCtrl.UnitTests.TestData;
using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes.UnknownDirective;

public sealed class UnknownDirectiveCommentTests
{
    [Theory]
    [MemberData(nameof(CommentTestData.OriginalCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string comment, string expectedOriginalComment)
    {
        // Arrange
        const string directive = "directive";
        const string value = "Some value";

        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedOriginalComment, unknownDirective.OriginalComment);
    }
    
    [Theory]
    [MemberData(nameof(CommentTestData.TrimmedCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string comment, string expectedComment)
    {
        // Arrange
        const string directive = "directive";
        const string value = "Some value";

        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, unknownDirective.Comment);
    }
    
    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string directive = "directive";
        const string value = "Some value";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, unknownDirective.OriginalComment);
    }

    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string directive = "directive";
        const string value = "Some value";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, unknownDirective.Comment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string directive = "directive";
        const string value = "Some value";
        
        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value);
        
        // Assert
        Assert.Null(unknownDirective.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string directive = "directive";
        const string value = "Some value";
        
        // Act
        var unknownDirective = new CrawlCtrl.UnknownDirective(directive: directive, value: value);
        
        // Assert
        Assert.Null(unknownDirective.Comment);
    }
}