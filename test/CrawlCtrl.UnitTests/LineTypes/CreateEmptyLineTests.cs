using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateEmptyLineTests
{
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void WHEN_Created_with_empty_value_THEN_Original_value_is_set_to_provided_value(string value)
    {
        // Arrange
        var expectedValue = value;

        // Act
        var emptyLine = new EmptyLine(value: value);

        // Assert
        Assert.Equal(expectedValue, emptyLine.OriginalValue);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "")]
    public void WHEN_Created_with_empty_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange

        // Act
        var emptyLine = new EmptyLine(value: value);

        // Assert
        Assert.Equal(expectedValue, emptyLine.Value);
    }

    [Fact]
    public void WHEN_Created_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new EmptyLine(value: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_with_non_empty_value_THEN_Throw_exception()
    {
        // Arrange
        const string value = "Hello, World!";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new EmptyLine(value: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_not_set()
    {
        // Arrange
        const string value = "";
        
        // Act
        var emptyLine = new EmptyLine(value: value);
        
        // Assert
        Assert.Null(emptyLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        const string value = "";
        
        // Act
        var emptyLine = new EmptyLine(value: value);
        
        // Assert
        Assert.Null(emptyLine.Comment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some comment")]
    [InlineData(" Some comment ")]
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string? comment)
    {
        // Arrange
        var expectedComment = comment;

        // Act
        var emptyLine = new EmptyLine(value: "", comment: comment);

        // Assert
        Assert.Equal(expectedComment, emptyLine.OriginalComment);
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData("  ", "")]
    [InlineData("Some comment", "Some comment")]
    [InlineData(" Some comment ", "Some comment")]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_whitespace_trimmed_value(string? comment, string? expectedComment)
    {
        // Arrange

        // Act
        var emptyLine = new EmptyLine(value: "", comment: comment);

        // Assert
        Assert.Equal(expectedComment, emptyLine.Comment);
    }
}