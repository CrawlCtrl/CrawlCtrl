using FluentAssertions;
using Xunit;

namespace CrawlCtrl.Reader.UnitTests;

public sealed class ConfiguringUserAgentWithStringTests
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
        var userAgent = new StringUserAgent(UserAgentTestHelper.NewValidUserAgentString);
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
        var userAgent = new StringUserAgent(UserAgentTestHelper.NewValidUserAgentString);
        httpClient.ConfigureUserAgent(userAgent);

        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedEndUserAgent, options => options.WithStrictOrdering());
    }

    [Fact]
    public void WHEN_User_agent_is_valid_WHEN_Validating_user_agent_THEN_User_agent_is_set()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();

        var expectedUserAgent = UserAgentTestHelper.NewValidUserAgentValues;

        // Act
        var userAgent = new StringUserAgent(
            userAgent: UserAgentTestHelper.NewValidUserAgentString,
            validate: true
        );
        httpClient.ConfigureUserAgent(userAgent);

        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedUserAgent, options => options.WithStrictOrdering());
    }
    
    [Fact]
    public void WHEN_User_agent_is_valid_WHEN_Not_validating_user_agent_THEN_User_agent_is_set()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();

        var expectedUserAgent = UserAgentTestHelper.NewValidUserAgentValues;

        // Act
        var userAgent = new StringUserAgent(
            userAgent: UserAgentTestHelper.NewValidUserAgentString,
            validate: false
        );
        httpClient.ConfigureUserAgent(userAgent);

        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedUserAgent, options => options.WithStrictOrdering());
    }
    
    [Fact]
    public void WHEN_User_agent_is_invalid_WHEN_Validating_user_agent_THEN_Throw_exception()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();
        
        // Act & Assert
        var userAgent = new StringUserAgent(
            userAgent: UserAgentTestHelper.NewInvalidUserAgentString,
            validate: true
        );
        Assert.Throws<FormatException>(() => httpClient.ConfigureUserAgent(userAgent));
    }
    
    [Fact]
    public void WHEN_User_agent_is_invalid_WHEN_Not_validating_user_agent_THEN_User_agent_is_not_set()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();
        
        // Act
        var userAgent = new StringUserAgent(
            userAgent: UserAgentTestHelper.NewInvalidUserAgentString,
            validate: false
        );
        httpClient.ConfigureUserAgent(userAgent);
        
        // Assert
        Assert.Empty(httpClient.DefaultRequestHeaders.UserAgent);
    }
}