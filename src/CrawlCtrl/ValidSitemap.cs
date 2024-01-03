using System;

namespace CrawlCtrl
{
    public sealed class ValidSitemap : Sitemap
    {
        public ValidSitemap(Uri sitemapUri, string comment = null) : this(Constants.Directives.Sitemap, sitemapUri, comment)
        {
        }
            
        public ValidSitemap(string directive, Uri sitemapUri, string comment = null) : base(directive, sitemapUri?.AbsoluteUri, comment)
        {
            SitemapUri = sitemapUri ?? throw new ArgumentNullException(nameof(sitemapUri));
        }
        
        internal ValidSitemap(string directive, string value, Uri sitemapUri, string comment = null) : base(directive, value, comment)
        {
            SitemapUri = sitemapUri ?? throw new ArgumentNullException(nameof(sitemapUri));
        }

        public Uri SitemapUri { get; }
    }
}