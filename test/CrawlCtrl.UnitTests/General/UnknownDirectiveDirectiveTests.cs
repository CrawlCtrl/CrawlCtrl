using CrawlCtrl.UnitTests.TestData;
using Xunit;

namespace CrawlCtrl.UnitTests.General;

public sealed class UnknownDirectiveDirectiveTests
{
    [Theory]
    [MemberData(nameof(DirectiveTestData.OriginalDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_THEN_Original_directive_is_set_to_provided_directive(string directive, string expectedOriginalDirective)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value);

        // Assert
        Assert.Equal(expectedOriginalDirective, unknownDirective.OriginalDirective);
    }
    
    [Theory]
    [MemberData(nameof(DirectiveTestData.TrimmedDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_THEN_Directive_is_set_to_whitespace_trimmed_directive(string directive, string expectedDirective)
    {
        // Arrange
        const string value = "Some value";

        // Act
        var unknownDirective = new UnknownDirective(directive: directive, value: value);

        // Assert
        Assert.Equal(expectedDirective, unknownDirective.Directive);
    }
    
    [Theory]
    [MemberData(nameof(DirectiveTestData.EmptyDirectiveTestData), MemberType = typeof(DirectiveTestData))]
    public void WHEN_Created_with_directive_set_to_empty_string_THEN_Throw_exception(string directive)
    {
        // Arrange
        const string value = "Some value";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => new UnknownDirective(directive: directive, value: value));
        
        // Assert
        Assert.Equal("directive", exception.ParamName);
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
}