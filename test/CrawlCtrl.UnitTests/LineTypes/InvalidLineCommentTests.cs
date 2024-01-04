using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class InvalidLineCommentTests
{
    [Theory]
    [MemberData(nameof(CommentTestData.OriginalCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string comment, string expectedOriginalComment)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidLine = new InvalidLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedOriginalComment, invalidLine.OriginalComment);
    }
    
    [Theory]
    [MemberData(nameof(CommentTestData.TrimmedCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string comment, string expectedComment)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var invalidLine = new InvalidLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidLine.Comment);
    }
    
    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var invalidLine = new InvalidLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidLine.OriginalComment);
    }

    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var invalidLine = new InvalidLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidLine.Comment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var invalidLine = new InvalidLine(value: value);
        
        // Assert
        Assert.Null(invalidLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var invalidLine = new InvalidLine(value: value);
        
        // Assert
        Assert.Null(invalidLine.Comment);
    }
}