using System;

namespace CrawlCtrl.Reader
{
    internal sealed class ImmutableRobotsReaderOptions
    {
        public ImmutableRobotsReaderOptions(IUserAgent userAgent, RobotsDeserializerOptions deserializerOptions)
        {
            UserAgent = userAgent ?? throw new ArgumentNullException(nameof(userAgent));
            DeserializerOptions = deserializerOptions ?? throw new ArgumentNullException(nameof(deserializerOptions));
        }

        public IUserAgent UserAgent { get; }
        public RobotsDeserializerOptions DeserializerOptions { get; }
    }
}