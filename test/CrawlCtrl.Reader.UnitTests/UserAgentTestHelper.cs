using System.Net.Http.Headers;

namespace CrawlCtrl.Reader.UnitTests;

internal sealed class UserAgentTestHelper
{
    public const string NewValidUserAgentString = "New/3.2.1 (It is very awesome)";
    public static ProductInfoHeaderValue[] NewValidUserAgentValues =
    [
        new ProductInfoHeaderValue("New", "3.2.1"),
        new ProductInfoHeaderValue("(It is very awesome)")
    ];

    public const string NewInvalidUserAgentString = "I'm an idiot sandwich // OMG";

    public const string PredefinedUserAgentString = "Predefined/1.2.3 (It is awesome)";
    public static ProductInfoHeaderValue[] PredefinedUserAgentValues =
    [
        new ProductInfoHeaderValue("Predefined", "1.2.3"),
        new ProductInfoHeaderValue("(It is awesome)")
    ];
    
    public static HttpClient HttpClientWithUserAgent() {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", PredefinedUserAgentString);
        return httpClient;
    }
    
    public static HttpClient HttpClientWithoutUserAgent() {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        return httpClient;
    }
}