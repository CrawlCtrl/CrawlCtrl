using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateInvalidLineTests
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
        var invalidLine = new InvalidLine(value: value);

        // Assert
        Assert.Equal(expectedValue, invalidLine.Value);
    }

    [Fact]
    public void WHEN_Created_with_value_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidLine(value: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        
        // Act
        var invalidLine = new InvalidLine(value: "Some value");
        
        // Assert
        Assert.Null(invalidLine.Comment);
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
        var invalidLine = new InvalidLine(value: "Some value", comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidLine.Comment);
    }
}