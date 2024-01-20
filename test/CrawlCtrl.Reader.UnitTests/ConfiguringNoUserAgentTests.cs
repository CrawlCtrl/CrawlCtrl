using Xunit;

namespace CrawlCtrl.Reader.UnitTests;

public sealed class ConfiguringNoUserAgentTests
{
    [Fact]
    public void WHEN_Configuring_no_user_agent_WHILE_User_agent_not_predefined_THEN_No_user_agent_configured()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithoutUserAgent();

        var userAgentConfiguration = new NoUserAgent();
        
        // Act
        httpClient.ConfigureUserAgent(userAgentConfiguration);

        // Assert
        Assert.Empty(httpClient.DefaultRequestHeaders.UserAgent);
    }
    
    [Fact]
    public void WHEN_Configuring_no_user_agent_WHILE_User_agent_predefined_THEN_No_user_agent_configured()
    {
        // Arrange
        var httpClient = UserAgentTestHelper.HttpClientWithUserAgent();

        var userAgentConfiguration = new NoUserAgent();
        
        // Act
        httpClient.ConfigureUserAgent(userAgentConfiguration);

        // Assert
        Assert.Empty(httpClient.DefaultRequestHeaders.UserAgent);
    }
}