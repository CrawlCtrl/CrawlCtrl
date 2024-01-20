using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace CrawlCtrl.Reader
{
    public interface IUserAgent
    {
        
    }

    public sealed class StringUserAgent : IUserAgent
    {
        public StringUserAgent(string userAgent, bool validate = true)
        {
            UserAgent = userAgent ?? throw new ArgumentNullException(nameof(userAgent));
            Validate = validate;
        }

        public string UserAgent { get; }
        public bool Validate { get; }
    }

    public sealed class ProductInfoUserAgent : IUserAgent
    {
        public ProductInfoUserAgent(IEnumerable<ProductInfoHeaderValue> values)
        {
            Values = values?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(values));
        }
        
        public IReadOnlyCollection<ProductInfoHeaderValue> Values { get; }
    }

    public sealed class NoUserAgent : IUserAgent
    {
        
    }

    public sealed class KeepUserAgent : IUserAgent
    {
        
    }
}