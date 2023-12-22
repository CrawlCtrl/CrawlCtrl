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

        public Sitemap Deserialize(string value, string comment)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var trimmedValue = value.Trim();

            if (Uri.TryCreate(trimmedValue, UriKind.Absolute, out var sitemapUri))
            {
                return ReturnValidSitemaps ? new ValidSitemap(sitemapUri, comment) : null;
            }

            return ReturnInvalidSitemaps ? new InvalidSitemap(value, comment) : null;
        }
    }
}