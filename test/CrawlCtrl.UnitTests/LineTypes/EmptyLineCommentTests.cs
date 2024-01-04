using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class EmptyLineCommentTests
{
    [Theory]
    [MemberData(nameof(CommentTestData.OriginalCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string comment, string expectedOriginalComment)
    {
        // Arrange
        const string value = "";

        // Act
        var emptyLine = new EmptyLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedOriginalComment, emptyLine.OriginalComment);
    }
    
    [Theory]
    [MemberData(nameof(CommentTestData.TrimmedCommentTestData), MemberType = typeof(CommentTestData))]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string comment, string expectedComment)
    {
        // Arrange
        const string value = "";

        // Act
        var emptyLine = new EmptyLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, emptyLine.Comment);
    }
    
    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string value = "";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var emptyLine = new EmptyLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, emptyLine.OriginalComment);
    }

    [Fact]
    public void WHEN_Created_with_null_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string value = "";
        const string? comment = null;

        const string? expectedComment = null;

        // Act
        var emptyLine = new EmptyLine(value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, emptyLine.Comment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_null()
    {
        // Arrange
        const string value = "";
        
        // Act
        var emptyLine = new EmptyLine(value: value);
        
        // Assert
        Assert.Null(emptyLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_null()
    {
        // Arrange
        const string value = "";
        
        // Act
        var emptyLine = new EmptyLine(value: value);
        
        // Assert
        Assert.Null(emptyLine.Comment);
    }
}