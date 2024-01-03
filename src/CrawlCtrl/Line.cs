using System;

namespace CrawlCtrl
{
    public abstract class Line
    {
        protected Line(string value, string originalComment = null)
        {
            OriginalValue = value ?? throw new ArgumentNullException(nameof(value));
            OriginalComment = originalComment;
        }
        
        public string OriginalValue { get; }
        public string Value => OriginalValue.Trim();

        public string OriginalComment { get; }
        public string Comment => OriginalComment?.Trim();
    }
}