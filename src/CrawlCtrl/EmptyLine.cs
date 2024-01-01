using System;

namespace CrawlCtrl
{
    public sealed class EmptyLine : Line
    {
        public EmptyLine(string value, string comment = null) : base(comment)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value) is false)
            {
                throw new ArgumentException("The string must be empty or consist of only whitespaces.", nameof(value));
            }

            Value = value;
        }
        
        public string Value { get; }
    }
}