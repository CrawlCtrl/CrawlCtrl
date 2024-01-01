namespace CrawlCtrl
{
    public abstract class Line
    {
        protected Line(string value, string comment = null)
        {
            Comment = comment;
        }
        
        public string Value { get; }
        public string TrimmedValue => Value.Trim();

        public string Comment { get; }
        public string TrimmedComment => Comment?.Trim();
    }
}