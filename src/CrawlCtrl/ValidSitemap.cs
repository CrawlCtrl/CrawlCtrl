using System;

namespace CrawlCtrl
{
    public sealed class ValidSitemap : Sitemap
    {
        public ValidSitemap(Uri uri, string comment = null)
            : this(Constants.Directives.Sitemap, uri, comment)
        {
        }

        public ValidSitemap(string directive, Uri uri, string comment = null, string fullLine = null)
            : base(directive, uri?.OriginalString ?? string.Empty, comment, fullLine)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public Uri Uri { get; }
    }
}