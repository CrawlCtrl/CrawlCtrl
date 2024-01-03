using Xunit;

namespace CrawlCtrl.UnitTests.LineTypes;

public sealed class CreateUnknownDirectiveTests
{
    [Theory]
    [InlineData("Directive")]
    [InlineData(" Directive ")]
    public void WHEN_Created_with_directive_THEN_Original_directive_is_set_to_provided_value(string directive)
    {
        // Arrange
        var expectedDirective = directive;
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value);

        // Assert
        Assert.Equal(expectedDirective, unknownDirective.OriginalDirective);
    }
    
    [Theory]
    [InlineData("Directive", "Directive")]
    [InlineData(" Directive ", "Directive")]
    public void WHEN_Created_with_directive_THEN_Directive_is_set_to_whitespace_trimmed_value(string directive, string expectedDirective)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value);

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

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void WHEN_Created_with_directive_set_to_empty_string_THEN_Throw_exception(string directive)
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new UnknownDirective(directive: directive, value: value));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("Some random value")]
    [InlineData(" Some random value ")]
    public void WHEN_Created_with_empty_value_THEN_Original_value_is_set_to_provided_value(string value)
    {
        // Arrange
        const string directive = "testdirective";
        var expectedValue = value;

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: expectedValue);

        // Assert
        Assert.Equal(expectedValue, unknownDirective.OriginalValue);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ", "")]
    [InlineData("Some random value", "Some random value")]
    [InlineData(" Some random value ", "Some random value")]
    public void WHEN_Created_with_empty_value_THEN_Value_is_set_to_whitespace_trimmed_value(string value, string expectedValue)
    {
        // Arrange
        const string directive = "testdirective";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: expectedValue);

        // Assert
        Assert.Equal(expectedValue, unknownDirective.Value);
    }
    
    [Fact]
    public void WHEN_Create_with_null_value_THEN_Throw_exception()
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
    public void WHEN_Created_without_comment_THEN_Original_comment_is_not_set()
    {
        // Arrange
        const string directive = "testdirective";
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value);
        
        // Assert
        Assert.Null(unknownDirective.OriginalComment);
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
    public void WHEN_Created_with_comment_THEN_Original_comment_is_set_to_provided_value(string? comment)
    {
        // Arrange
        const string directive = "testdirective";
        const string value = "Some value";
        var expectedComment = comment;

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, unknownDirective.OriginalComment);
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
        const string directive = "testdirective";
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value, comment: comment);

        // Assert
        Assert.Equal(expectedComment, unknownDirective.Comment);
    }
}