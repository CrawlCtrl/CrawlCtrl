using System;

namespace CrawlCtrl
{
    public abstract class DirectiveLine : Line
    {
        protected DirectiveLine(string directive, string comment = null) : base(comment)
        {
            Directive = directive ?? throw new ArgumentNullException(nameof(directive));
        }
        
        public string Directive { get; }
        public string TrimmedDirective => Directive.Trim().ToLower();
    }
}