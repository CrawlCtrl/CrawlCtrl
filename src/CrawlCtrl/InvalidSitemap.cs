namespace CrawlCtrl
{
    public sealed class InvalidSitemap : Sitemap
    {
        public InvalidSitemap(string value, string comment = null) : this(Constants.Directives.Sitemap, value, comment)
        {
        }
            
        public InvalidSitemap(string directive, string value, string comment = null) : base(directive, value, comment)
        {
        }
    }
}