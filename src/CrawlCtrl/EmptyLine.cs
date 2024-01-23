using System;

namespace CrawlCtrl
{
    public sealed class EmptyLine : Line
    {
        public EmptyLine(string value, string comment = null, string fullLine = null)
            : base(value, comment, fullLine)
        {
            if (string.IsNullOrWhiteSpace(value) is false)
            {
                throw new ArgumentException("The argument must be empty or contain only whitespaces.", nameof(value));
            }
        }
    }
}