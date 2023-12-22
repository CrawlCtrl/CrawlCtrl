using System.Collections.Generic;

namespace CrawlCtrl.Deserialization
{
    internal static class LineDeserializationCoordinatorFactory
    {
        public static LineDeserializationCoordinator Build(RobotsDeserializerOptions options)
        {
            if (options is null)
            {
                options = new RobotsDeserializerOptions();
            }
            
            var lineInterpreters = new Dictionary<string, ILineDeserializer<Line>>();

            if (options.IncludeSitemaps)
            {
                lineInterpreters[Constants.Directives.Sitemap] = new SitemapLineDeserializer(options.SitemapsInclusionScope);
            }
            
            return new LineDeserializationCoordinator(lineInterpreters, options);
        }
    }
}