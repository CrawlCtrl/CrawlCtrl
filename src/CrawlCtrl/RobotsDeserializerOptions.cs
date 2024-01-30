namespace CrawlCtrl
{
    public sealed class RobotsDeserializerOptions
    {
        public bool IncludeComments { get; set; } = false;
        public bool IncludeEmptyLines { get; set; } = false;
        public bool IncludeInvalidLines { get; set; } = false;
        public bool IncludeUnknownDirectives { get; set; } = false;
        
        public SitemapPolicy SitemapPolicy { get; set; } = SitemapPolicy.OnlyValid;
    }
}