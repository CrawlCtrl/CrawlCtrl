namespace CrawlCtrl
{
    internal sealed class ImmutableRobotsDeserializerOptions
    {
        internal ImmutableRobotsDeserializerOptions(bool includeComments, bool includeEmptyLines, bool includeInvalidLines, bool includeUnknownDirectives, SitemapPolicy sitemapPolicy)
        {
            IncludeComments = includeComments;
            IncludeEmptyLines = includeEmptyLines;
            IncludeInvalidLines = includeInvalidLines;
            IncludeUnknownDirectives = includeUnknownDirectives;
            SitemapPolicy = sitemapPolicy;
        }

        public bool IncludeComments { get; }
        public bool IncludeEmptyLines { get; }
        public bool IncludeInvalidLines { get; }
        public bool IncludeUnknownDirectives { get; }
        
        public SitemapPolicy SitemapPolicy { get; }
    }
}