using System;

namespace CrawlCtrl
{
    public sealed class InvalidLine : Line
    {
        public InvalidLine(string value, string comment = null, string fullLine = null)
            : base(value, comment, fullLine)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(value));
            }
        }
    }
}