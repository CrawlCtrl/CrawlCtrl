using System;

namespace CrawlCtrl
{
    public sealed class UnknownDirective : DirectiveLine
    {
        public UnknownDirective(string directive, string value, string comment = null) : base(directive, comment)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }
    }
}