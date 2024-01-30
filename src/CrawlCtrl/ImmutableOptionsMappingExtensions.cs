namespace CrawlCtrl
{
    internal static class ImmutableOptionsMappingExtensions
    {
        public static ImmutableRobotsDeserializerOptions ToImmutableOrDefault(this RobotsDeserializerOptions options)
        {
            if (options is null)
            {
                options = new RobotsDeserializerOptions();
            }
            
            var immutableOptions = new ImmutableRobotsDeserializerOptions(
                includeComments: options.IncludeComments,
                includeEmptyLines: options.IncludeEmptyLines,
                includeInvalidLines: options.IncludeInvalidLines,
                includeUnknownDirectives: options.IncludeUnknownDirectives,
                sitemapPolicy: options.SitemapPolicy
            );

            return immutableOptions;
        }
    }
}