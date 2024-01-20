using System;
using System.Net.Http;

namespace CrawlCtrl.Reader
{
    internal static class HttpClientExtensions
    {
        public static HttpClient ConfigureUserAgent(this HttpClient httpClient, IUserAgent userAgent)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (userAgent is null)
            {
                throw new ArgumentNullException(nameof(userAgent));
            }

            if (userAgent is KeepUserAgent)
            {
                return httpClient;
            }
            
            httpClient.DefaultRequestHeaders.UserAgent.Clear();

            switch (userAgent)
            {
                case StringUserAgent userAgentData when userAgentData.Validate:
                    httpClient.DefaultRequestHeaders.Add("User-Agent", userAgentData.UserAgent);
                    break;
                case StringUserAgent userAgentData:
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgentData.UserAgent);
                    break;
                case ProductInfoUserAgent userAgentData:
                    foreach (var value in userAgentData.Values)
                    {
                        httpClient.DefaultRequestHeaders.UserAgent.Add(value);
                    }
                    break;
            }
            
            return httpClient;
        }
    }
}