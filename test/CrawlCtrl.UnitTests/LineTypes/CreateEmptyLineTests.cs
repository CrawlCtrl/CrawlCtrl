using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateEmptyLineTests
{
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some random value")]
    [InlineData(" Some random value ")]
    public void WHEN_Created_with_value_THEN_Value_is_set_to_provided_value(string value)
    {
        // Arrange
        var expectedValue = value;

        // Act
        var emptyLine = new EmptyLine(value: value);

        // Assert
        Assert.Equal(expectedValue, emptyLine.Value);
    }

    [Fact]
    public void WHEN_Created_with_value_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new EmptyLine(value: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        
        // Act
        var emptyLine = new EmptyLine(value: "Some value");
        
        // Assert
        Assert.Null(emptyLine.Comment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some comment")]
    [InlineData(" Some comment ")]
    public void WHEN_Created_with_comment_THEN_Comment_is_set_to_provided_value(string? comment)
    {
        // Arrange
        var expectedComment = comment;

        // Act
        var emptyLine = new EmptyLine(value: "Some value", comment: comment);

        // Assert
        Assert.Equal(expectedComment, emptyLine.Comment);
    }
}