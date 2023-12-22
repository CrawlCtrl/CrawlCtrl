using System;

namespace CrawlCtrl
{
    public sealed class EmptyLine : Line
    {
        public EmptyLine(string value, string comment = null) : base(comment)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public string Value { get; }
    }
}