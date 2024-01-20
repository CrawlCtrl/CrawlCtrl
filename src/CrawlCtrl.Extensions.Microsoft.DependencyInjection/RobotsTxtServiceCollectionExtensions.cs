using System;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace CrawlCtrl
{
    public static class CrawlCtrlServiceCollectionExtensions
    {
        public static IServiceCollection AddCrawlCtrl(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            services.AddTransient<IRobotsDeserializer, RobotsDeserializer>();
            
            return services;
        }
    }
}