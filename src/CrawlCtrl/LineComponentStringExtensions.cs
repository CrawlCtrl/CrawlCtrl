using System;

namespace CrawlCtrl
{
    internal static class LineComponentStringExtensions
    {
        public static LineComponents SplitIntoLineComponents(this string line, bool includeComment)
        {
            if (line is null)
            {
                throw new ArgumentNullException(nameof(line));
            }
            
            string comment = null;
            
            var commentSplit = line.Split(new[] { '#' }, 2);

            if (commentSplit.Length == 2 && includeComment)
            {
                comment = commentSplit[1];
            }

            var directiveSplit = commentSplit[0].Split(new[] { ':' }, 2);

            if (directiveSplit.Length == 1)
            {
                return new LineComponents(
                    directive: null,
                    value: directiveSplit[0],
                    comment: comment
                );
            }

            var directive = directiveSplit[0];
            
            return new LineComponents(
                directive: directive,
                value: directiveSplit[1],
                comment: comment
            );
        }
    }
    
    internal sealed class LineComponents
    {
        public LineComponents(string directive, string value, string comment)
        {
            Directive = directive;
            Value = value;
            Comment = comment;
        }

        public string Directive { get; }
        public string Value { get; }
        public string Comment { get; }
    }
}