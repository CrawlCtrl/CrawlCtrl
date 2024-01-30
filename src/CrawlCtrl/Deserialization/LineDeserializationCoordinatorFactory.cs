using System;
using System.Collections.Generic;

namespace CrawlCtrl.Deserialization
{
    internal static class LineDeserializationCoordinatorFactory
    {
        public static LineDeserializationCoordinator Build(ImmutableRobotsDeserializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            var lineInterpreters = new Dictionary<string, ILineDeserializer<Line>>();

            if (options.SitemapPolicy != SitemapPolicy.Ignore)
            {
                lineInterpreters[Constants.Directives.Sitemap] = new SitemapLineDeserializer();
            }
            
            return new LineDeserializationCoordinator(lineInterpreters, options);
        }
    }
}