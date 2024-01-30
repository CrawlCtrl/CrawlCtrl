using System;

namespace CrawlCtrl.Deserialization
{
    internal sealed class SitemapLineDeserializer : ILineDeserializer<Sitemap>
    {
        public Sitemap Deserialize(string directive, string value, string comment, string line, ImmutableRobotsDeserializerOptions options)
        {
            if (directive is null)
            {
                throw new ArgumentNullException(nameof(directive));
            }
            
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            if (line is null)
            {
                throw new ArgumentNullException(nameof(line));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (Uri.TryCreate(value, UriKind.Absolute, out var sitemapUri))
            {
                return options.ReturnValidSitemaps()
                    ? new ValidSitemap(directive: directive, uri: sitemapUri, comment: comment, fullLine: line) 
                    : null;
            }

            return options.ReturnInvalidSitemaps()
                ? new InvalidSitemap(directive: directive, value: value, comment: comment, fullLine: line)
                : null;
        }
    }
    
    internal static class SitemapPolicyCheckExtensions
    {
        public static bool ReturnValidSitemaps(this ImmutableRobotsDeserializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return options.SitemapPolicy == SitemapPolicy.All || options.SitemapPolicy == SitemapPolicy.OnlyValid;
        }
        
        public static bool ReturnInvalidSitemaps(this ImmutableRobotsDeserializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return options.SitemapPolicy == SitemapPolicy.All || options.SitemapPolicy == SitemapPolicy.OnlyInvalid;
        }
    }
}