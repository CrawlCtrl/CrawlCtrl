using System;

namespace CrawlCtrl
{
    public sealed class ValidSitemap : Sitemap
    {
        public ValidSitemap(Uri sitemapUri, string comment = null) : this(sitemapUri, Constants.Directives.Sitemap, comment)
        {
        }
            
        public ValidSitemap(Uri sitemapUri, string directive, string comment = null) : base(directive, comment)
        {
            SitemapUri = sitemapUri ?? throw new ArgumentNullException(nameof(sitemapUri));
        }

        public Uri SitemapUri { get; }
    }
}