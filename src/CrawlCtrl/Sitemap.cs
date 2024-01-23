namespace CrawlCtrl
{
    public abstract class Sitemap : DirectiveLine
    {
        protected Sitemap(string directive, string value, string comment, string fullLine)
            : base(directive, value, comment, fullLine)
        {
        }
    }
}