using System;

namespace CrawlCtrl
{
    public sealed class InvalidLine : Line
    {
        public InvalidLine(string value, string comment = null) : base(value, comment)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The argument cannot be empty or contain only whitespaces.", nameof(value));
            }
        }
    }
}