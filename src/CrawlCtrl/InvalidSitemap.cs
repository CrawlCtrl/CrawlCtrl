using System;

namespace CrawlCtrl
{
    public sealed class InvalidSitemap : Sitemap
    {
        public InvalidSitemap(string sitemapValue, string comment = null) : this(sitemapValue, Constants.Directives.Sitemap, comment)
        {
        }
            
        public InvalidSitemap(string sitemapValue, string directive, string comment = null) : base(directive, comment)
        {
            SitemapValue = sitemapValue ?? throw new ArgumentNullException(nameof(sitemapValue));
        }

        public string SitemapValue { get; }
    }
}