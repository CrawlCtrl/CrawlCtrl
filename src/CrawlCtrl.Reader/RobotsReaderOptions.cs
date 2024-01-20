using System;

namespace CrawlCtrl.Reader
{
    public sealed class RobotsReaderOptions
    {
        private IUserAgent _userAgent = new KeepUserAgent();
        private RobotsDeserializerOptions _deserializerOptions = new RobotsDeserializerOptions();

        public IUserAgent UserAgent
        {
            get => _userAgent;
            set => _userAgent = value ?? throw new ArgumentNullException(nameof(value));
        }

        public RobotsDeserializerOptions DeserializerOptions
        {
            get => _deserializerOptions;
            set => _deserializerOptions = value ?? throw new ArgumentNullException(nameof(value));
        }

        internal ImmutableRobotsReaderOptions ToImmutable()
        {
            return new ImmutableRobotsReaderOptions(
                userAgent: _userAgent,
                deserializerOptions: _deserializerOptions
            );
        }
    }
}