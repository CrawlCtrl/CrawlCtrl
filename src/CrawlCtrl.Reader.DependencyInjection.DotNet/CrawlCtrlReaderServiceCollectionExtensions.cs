using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;

namespace CrawlCtrl.Reader.DependencyInjection.DotNet
{
    public static class CrawlCtrlReaderServiceCollectionExtensions
    {
        public static IServiceCollection AddCrawlCtrlReader(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddCrawlCtrl();
            services.AddTransient<IRobotsReader, RobotsReader>();
            services.AddHttpClient(Constants.HttpClient.Name, client =>
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("", ""));
            });

            return services;
        }
    }
}