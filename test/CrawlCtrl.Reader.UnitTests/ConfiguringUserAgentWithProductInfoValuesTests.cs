using System.Net.Http.Headers;
using FluentAssertions;
using Xunit;

namespace CrawlCtrl.Reader.UnitTests;

public sealed class ConfiguringUserAgentWithProductInfoValuesTests
{
    [Fact]
    public void WHILE_Http_client_has_user_agent_THEN_User_agent_is_replaced()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithUserAgent();

        var expectedStartUserAgent = UserAgentTestHelper.PredefinedUserAgentValues;
        var expectedEndUserAgent = UserAgentTestHelper.NewValidUserAgentValues;
        
        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedStartUserAgent, options => options.WithStrictOrdering());

        // Act
        var userAgent = new ProductInfoUserAgent(UserAgentTestHelper.NewValidUserAgentValues);
        httpClient.ConfigureUserAgent(userAgent);

        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedEndUserAgent, options => options.WithStrictOrdering());
    }

    [Fact]
    public void WHILE_Http_client_has_no_user_agent_THEN_User_agent_is_set()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();

        var expectedEndUserAgent = UserAgentTestHelper.NewValidUserAgentValues;
        
        // Assert
        Assert.Empty(httpClient.DefaultRequestHeaders.UserAgent);

        // Act
        var userAgent = new ProductInfoUserAgent(UserAgentTestHelper.NewValidUserAgentValues);
        httpClient.ConfigureUserAgent(userAgent);

        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedEndUserAgent, options => options.WithStrictOrdering());
    }

    [Fact]
    public void WHEN_Values_are_empty_THEN_User_agent_is_not_set()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();
        
        // Act
        var userAgent = new ProductInfoUserAgent(Enumerable.Empty<ProductInfoHeaderValue>());
        httpClient.ConfigureUserAgent(userAgent);
        
        // Assert
        Assert.Empty(httpClient.DefaultRequestHeaders.UserAgent);
    }
}