using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateUnknownDirectiveTests
{
    [Fact]
    public void WHEN_Created_with_directive_THEN_Directive_is_set_to_provided_value()
    {
        // Arrange
        const string expectedDirective = "testdirective";
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: expectedDirective, value: value);

        // Assert
        Assert.Equal(expectedDirective, unknownDirective.Directive);
    }
    
    [Fact]
    public void WHEN_Created_with_directive_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string? directive = null;
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new UnknownDirective(directive: directive, value: value));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Fact]
    public void WHEN_Create_with_value_THEN_Value_is_set_to_provided_value()
    {
        // Arrange
        const string directive = "testdirective";
        const string expectedValue = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: expectedValue);

        // Assert
        Assert.Equal(expectedValue, unknownDirective.Value);
    }
    
    [Fact]
    public void WHEN_Create_with_value_set_to_null_THEN_Throw_exception()
    {
        // Arrange
        const string directive = "testdirective";
        const string? value = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => new UnknownDirective(directive: directive, value: value));

        // Assert
        Assert.Equal("value", exception.ParamName);
    }
    
    [Fact]
    public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
    {
        // Arrange
        const string directive = "testdirective";
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value);
        
        // Assert
        Assert.Null(unknownDirective.Comment);
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
        const string directive = "testdirective";
        const string value = "Some value";
        var expectedComment = comment;

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, unknownDirective.Comment);
    }
}