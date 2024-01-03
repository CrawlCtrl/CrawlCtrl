using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateInvalidLineTests
{
    [Theory]
    [InlineData("Some random value")]
    [InlineData(" Some random value ")]
    public void WHEN_Created_with_value_THEN_Original_value_is_set_to_provided_value(string value)
    {
        // Arrange
        var expectedValue = value;

        // Act
        var invalidLine = new InvalidLine(value: value);

        // Assert
        Assert.Equal(expectedValue, invalidLine.OriginalValue);
    }
    
    [Theory]
    [InlineData("Some random value", "Some random value")]
    [InlineData(" Some random value ", "Some random value")]
    public void WHEN_Created_with_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange

        // Act
        var invalidLine = new InvalidLine(value: value);

        // Assert
        Assert.Equal(expectedValue, invalidLine.Value);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void WHEN_Created_with_empty_value_THEN_Throw_exception(string value)
    {
        // Arrange
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new InvalidLine(value: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_with_null_value_THEN_Throw_exception()
    {
        // Arrange
        const string? value = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new InvalidLine(value: value));
        
        // Assert
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void WHEN_Created_without_comment_THEN_Original_comment_is_not_set()
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var invalidLine = new InvalidLine(value: value);
        
        // Assert
        Assert.Null(invalidLine.OriginalComment);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var invalidLine = new InvalidLine(value: value);
        
        // Assert
        Assert.Null(invalidLine.Comment);
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
        var invalidLine = new InvalidLine(value: "Some value", comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidLine.OriginalComment);
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
        var invalidLine = new InvalidLine(value: "Some value", comment: comment);

        // Assert
        Assert.Equal(expectedComment, invalidLine.Comment);
    }
}