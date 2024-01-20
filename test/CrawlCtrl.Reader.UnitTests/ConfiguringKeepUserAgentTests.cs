using FluentAssertions;
using Xunit;

namespace CrawlCtrl.Reader.UnitTests;

public sealed class ConfiguringKeepUserAgentTests
{
    [Fact]
    public void WHEN_Configuring_keep_user_agent_WHILE_User_agent_not_predefined_THEN_No_user_agent_configured()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();

        var userAgentConfiguration = new KeepUserAgent();
        
        // Act
        httpClient.ConfigureUserAgent(userAgentConfiguration);

        // Assert
        Assert.Empty(httpClient.DefaultRequestHeaders.UserAgent);
    }
    
    [Fact]
    public void WHEN_Configuring_keep_user_agent_WHILE_User_agent_predefined_THEN_Keep_predefined_user_agent()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithUserAgent();

        var userAgentConfiguration = new KeepUserAgent();

        var expectedUserAgent = UserAgentTestHelper.PredefinedUserAgentValues;
        
        // Act
        httpClient.ConfigureUserAgent(userAgentConfiguration);

        // Assert
        httpClient.DefaultRequestHeaders.UserAgent
            .Should()
            .BeEquivalentTo(expectedUserAgent, options => options.WithStrictOrdering());
    }
}