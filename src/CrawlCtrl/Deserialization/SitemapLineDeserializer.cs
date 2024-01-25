using System;

namespace CrawlCtrl.Deserialization
{
    internal sealed class SitemapLineDeserializer : ILineDeserializer<Sitemap>
    {
        private readonly InclusionScope _inclusionScope;

        public SitemapLineDeserializer(InclusionScope inclusionScope)
        {
            _inclusionScope = inclusionScope;
        }
        
        private bool ReturnValidSitemaps => _inclusionScope == InclusionScope.All || _inclusionScope == InclusionScope.ValidOnly;
        private bool ReturnInvalidSitemaps => _inclusionScope == InclusionScope.All || _inclusionScope == InclusionScope.InvalidOnly;

        public Sitemap Deserialize(string directive, string value, string comment, string line)
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

            if (Uri.TryCreate(value, UriKind.Absolute, out var sitemapUri))
            {
                return ReturnValidSitemaps ? new ValidSitemap(directive: directive, uri: sitemapUri, comment: comment, fullLine: line) : null;
            }

            return ReturnInvalidSitemaps ? new InvalidSitemap(directive: directive, value: value, comment: comment, fullLine: line) : null;
        }
    }
}