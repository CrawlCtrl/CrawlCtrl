using CrawlCtrl.Deserialization;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class SanitizingDirectiveStringTests
{
    [Theory]
    [InlineData(" directive")]
    [InlineData("   directive")]
    public void WHEN_Sanitizing_directive_WHILE_Directive_has_leading_whitespace_THEN_Trim_leading_whitespace(string directive)
    {
        // Arrange
        const string expectedSanitizedDirective = "directive";

        // Act
        var actualSanitizedDirective = DirectiveStringSanitizer.SanitizeDirective(directive);

        // Assert
        Assert.Equal(expectedSanitizedDirective, actualSanitizedDirective);
    }
    
    [Theory]
    [InlineData("directive ")]
    [InlineData("directive   ")]
    public void WHEN_Sanitizing_directive_WHILE_Directive_has_trailing_whitespace_THEN_Trim_trailing_whitespace(string directive)
    {
        // Arrange
        const string expectedSanitizedDirective = "directive";

        // Act
        var actualSanitizedDirective = DirectiveStringSanitizer.SanitizeDirective(directive);

        // Assert
        Assert.Equal(expectedSanitizedDirective, actualSanitizedDirective);
    }
    
    [Theory]
    [InlineData("Directive")]
    [InlineData("DIRECTIVE")]
    [InlineData("DirEcTivE")]
    public void WHEN_Sanitizing_directive_WHILE_Directive_has_upper_case_letters_THEN_Convert_to_lower_case(string directive)
    {
        // Arrange
        const string expectedSanitizedDirective = "directive";

        // Act
        var actualSanitizedDirective = DirectiveStringSanitizer.SanitizeDirective(directive);

        // Assert
        Assert.Equal(expectedSanitizedDirective, actualSanitizedDirective);
    }
}