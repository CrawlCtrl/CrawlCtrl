using System;

namespace CrawlCtrl
{
    public sealed class InvalidLine : Line
    {
        public InvalidLine(string value, string comment = null) : base(comment)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The string cannot be empty or consist of only whitespaces.", nameof(value));
            }

            Value = value;
        }

        public string Value { get; }
    }
}