// using Xunit;
//
// namespace CrawlCtrl.UnitTests.LineTypes;
//
// public sealed class CreateValidSitemapTests
// {
//     [Fact]
//     public void WHEN_Created_with_sitemap_uri_THEN_Sitemap_uri_is_set_to_provided_value()
//     {
//         // Arrange
//         var expectedSitemapUri = new Uri("https://www.example.com");
//
//         // Act
//         var validSitemap = new ValidSitemap(sitemapUri: expectedSitemapUri);
//
//         // Assert
//         Assert.Equal(expectedSitemapUri, validSitemap.SitemapUri);
//     }
//     
//     [Fact]
//     public void WHEN_Created_with_sitemap_uri_set_to_null_THEN_Throw_exception()
//     {
//         // Arrange
//         Uri? sitemapUri = null;
//         
//         // Act
//         var exception = Assert.Throws<ArgumentNullException>(() => new ValidSitemap(sitemapUri: sitemapUri));
//         
//         // Assert
//         Assert.Equal("sitemapUri", exception.ParamName);
//     }
//
//     [Fact]
//     public void WHEN_Create_without_directive_THEN_Directive_is_set_to_default()
//     {
//         // Arrange
//         var sitemapUri = new Uri("https://www.example.com");
//         var expectedDirective = Constants.Directives.Sitemap;
//
//         // Act
//         var validSitemap = new ValidSitemap(sitemapUri: sitemapUri);
//
//         // Assert
//         Assert.Equal(expectedDirective, validSitemap.Directive);
//     }
//     
//     [Fact]
//     public void WHEN_Create_with_directive_set_to_null_THEN_Throw_exception()
//     {
//         // Arrange
//         var sitemapUri = new Uri("https://www.example.com");
//         const string? directive = null;
//
//         // Act
//         var exception = Assert.Throws<ArgumentNullException>(() => new ValidSitemap(sitemapUri: sitemapUri, directive: directive));
//
//         // Assert
//         Assert.Equal("directive", exception.ParamName);
//     }
//     
//     [Fact]
//     public void WHEN_Create_with_custom_directive_THEN_Directive_is_set_to_provided_value()
//     {
//         // Arrange
//         var sitemapUri = new Uri("https://www.example.com");
//         var expectedDirective = "SitEmAp";
//
//         // Act
//         var validSitemap = new ValidSitemap(sitemapUri: sitemapUri, directive: expectedDirective);
//
//         // Assert
//         Assert.Equal(expectedDirective, validSitemap.Directive);
//     }
//     
//     [Fact]
//     public void WHEN_Created_without_comment_THEN_Comment_is_not_set()
//     {
//         // Arrange
//         var sitemapUri = new Uri("https://www.example.com");
//         
//         // Act
//         var validSitemap = new ValidSitemap(sitemapUri: sitemapUri);
//         
//         // Assert
//         Assert.Null(validSitemap.Comment);
//     }
//
//     [Theory]
//     [InlineData(null)]
//     [InlineData("")]
//     [InlineData("  ")]
//     [InlineData("Some comment")]
//     [InlineData(" Some comment ")]
//     public void WHEN_Created_with_comment_THEN_Comment_is_set_to_provided_value(string? comment)
//     {
//         // Arrange
//         var sitemapUri = new Uri("https://www.example.com");
//         var expectedComment = comment;
//
//         // Act
//         var validSitemap =  new ValidSitemap(sitemapUri: sitemapUri, comment: comment);
//
//         // Assert
//         Assert.Equal(expectedComment, validSitemap.Comment);
//     }
// }