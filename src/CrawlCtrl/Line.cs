namespace CrawlCtrl
{
    public abstract class Line
    {
        protected Line() : this(null)
        {
            
        }
        
        protected Line(string comment)
        {
            Comment = comment;
        }

        public string Comment { get; }
    }
}