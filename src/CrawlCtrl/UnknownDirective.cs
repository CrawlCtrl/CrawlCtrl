namespace CrawlCtrl
{
    public sealed class UnknownDirective : DirectiveLine
    {
        public UnknownDirective(string directive, string value, string comment = null, string fullLine = null)
            : base(directive, value, comment, fullLine)
        {
        }
    }
}