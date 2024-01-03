using Xunit;

namespace CrawlCtrl.UnitTests;

public sealed class SplittingLineIntoComponentsTests
{
    [Fact]
    public void WHEN_Line_is_null_THEN_Throw_exception()
    {
        // Arrange
        string? robotsLine = null;
        
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => robotsLine.SplitIntoLineComponents(includeComment: true));
        
        // Assert
        Assert.Equal("line", exception.ParamName);
    }
    
    [Theory]
    [InlineData("", "")]
    [InlineData("  ","  ")]
    public void WHEN_Line_is_empty_THEN_Only_value_is_set(string inputLine, string expectedValue)
    {
        // Arrange
        
        // Act
        var lineComponents = inputLine.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Null(lineComponents.Directive);
        Assert.Equal(expectedValue, lineComponents.Value);
        Assert.Null(lineComponents.Comment);
    }

    [Theory]
    [InlineData(": https://www.example.com/sitemap.xml", "")]
    [InlineData("  : https://www.example.com/sitemap.xml", "  ")]
    [InlineData("sitemap: https://www.example.com/sitemap.xml", "sitemap")]
    [InlineData("  sitemap: https://www.example.com/sitemap.xml", "  sitemap")]
    [InlineData("sitemap  : https://www.example.com/sitemap.xml", "sitemap  ")]
    [InlineData("  sitemap  : https://www.example.com/sitemap.xml", "  sitemap  ")]
    public void WHEN_Line_has_directive_THEN_Original_directive_is_set_as_directive(string line, string expectedDirective)
    {
        // Arrange
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Equal(expectedDirective, lineComponents.Directive);
    }

    [Fact]
    public void WHEN_Line_has_directive_THEN_Directive_casing_is_untouched()
    {
        // Arrange
        const string line = " sIteMap : https://www.example.com/sitemap.xml";
        const string expectedDirective = " sIteMap ";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Equal(expectedDirective, lineComponents.Directive);
    }

    [Theory]
    [InlineData("sitemap:https://www.example.com/sitemap.xml", "https://www.example.com/sitemap.xml")]
    [InlineData("sitemap: https://www.example.com/sitemap.xml", " https://www.example.com/sitemap.xml")]
    [InlineData("sitemap:https://www.example.com/sitemap.xml ", "https://www.example.com/sitemap.xml ")]
    [InlineData("sitemap: https://www.example.com/sitemap.xml ", " https://www.example.com/sitemap.xml ")]
    public void WHEN_Line_has_directive_THEN_Value_is_set_to_text_after_directive(string line, string expectedValue)
    {
        // Arrange
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Equal(expectedValue, lineComponents.Value);
    }
    
    [Fact]
    public void WHEN_Line_has_directive_and_comment_THEN_Value_is_set_to_value_between_directive_and_comment()
    {
        // Arrange
        const string line = "sitemap: https://www.example.com/sitemap.xml # Hello, World!";
        const string expectedValue = " https://www.example.com/sitemap.xml ";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Equal(expectedValue, lineComponents.Value);
    }

    [Fact]
    public void WHEN_Line_has_no_directive_THEN_Directive_is_not_set_in_split_result()
    {
        // Arrange
        const string line = "Hello, World!";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Null(lineComponents.Directive);
    }
    
    [Theory]
    [InlineData("Hello, World!", "Hello, World!")]
    [InlineData("  Hello, World!", "  Hello, World!")]
    [InlineData("Hello, World!  ", "Hello, World!  ")]
    [InlineData("  Hello, World!  ", "  Hello, World!  ")]
    public void WHEN_Line_has_no_directive_THEN_Value_is_set_to_some_value_as_line(string line, string expectedValue)
    {
        // Arrange
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Equal(expectedValue, lineComponents.Value);
    }

    [Fact]
    public void WHILE_Including_comment_WHEN_Line_has_comment_THEN_Comment_is_set_in_split_result()
    {
        // Arrange
        const string line = "Hello, World! # Some comment";
        const string expectedComment = " Some comment";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: true);
        
        // Assert
        Assert.Equal(expectedComment, lineComponents.Comment);
    }
    
    [Fact]
    public void WHILE_Including_comment_WHEN_Line_does_not_have_comment_THEN_Comment_is_not_set_in_split_result()
    {
        // Arrange
        const string line = "Hello, World!";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: true);
        
        // Assert
        Assert.Null(lineComponents.Comment);
    }
    
    [Fact]
    public void WHILE_Not_including_comment_WHEN_Line_has_comment_THEN_Comment_is_not_set_in_split_result()
    {
        // Arrange
        const string line = "Hello, World! # Some comment";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Null(lineComponents.Comment);
    }
    
    [Fact]
    public void WHILE_Not_including_comment_WHEN_Line_does_not_have_comment_THEN_Comment_is_not_set_in_split_result()
    {
        // Arrange
        const string line = "Hello, World!";
        
        // Act
        var lineComponents = line.SplitIntoLineComponents(includeComment: false);
        
        // Assert
        Assert.Null(lineComponents.Comment);
    }
}