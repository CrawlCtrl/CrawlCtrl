using System;

namespace CrawlCtrl
{
    public abstract class Sitemap : DirectiveLine
    {
        protected Sitemap(string directive, string comment) : base(directive, comment)
        {
            
        }
    }
}