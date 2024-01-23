using System;

namespace CrawlCtrl
{
    public abstract class DirectiveLine : Line
    {
        protected DirectiveLine(string directive, string value, string comment, string fullLine)
            : base(value, comment, fullLine)
        {
            if (directive is null)
            {
                throw new ArgumentNullException(nameof(directive));
            }
            
            if (string.IsNullOrWhiteSpace(directive))
            {
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(directive));
            }

            OriginalDirective = directive;
        }
        
        public string OriginalDirective { get; }
        public string Directive => OriginalDirective.Trim();
    }
}