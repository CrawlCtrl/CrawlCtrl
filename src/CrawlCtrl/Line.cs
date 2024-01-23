using System;

namespace CrawlCtrl
{
    public abstract class Line
    {
        protected Line(string value, string comment = null, string fullLine = null)
        {
            OriginalValue = value ?? throw new ArgumentNullException(nameof(value));
            OriginalComment = comment;
            FullLine = fullLine;
        }
        
        public string OriginalValue { get; }
        public string Value => OriginalValue.Trim();

        public string OriginalComment { get; }
        public string Comment => OriginalComment?.Trim();
        
        public string FullLine { get; }
    }
}