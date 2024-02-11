using System;

namespace CrawlCtrl.Deserialization
{
    internal static class DirectiveStringSanitizer
    {
        public static string SanitizeDirective(string directive)
        {
            if (directive is null)
            {
                throw new ArgumentNullException(nameof(directive));
            }

            return directive.Trim().ToLower();
        }
    }
}