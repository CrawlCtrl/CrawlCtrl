using System;

namespace CrawlCtrl
{
    public sealed class InvalidLine : Line
    {
        public InvalidLine(string value, string comment = null) : base(comment)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; }
    }
}