using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CrawlCtrl.Reader
{
    public sealed class RobotsReader : IRobotsReader
    {
        private readonly IRobotsDeserializer _robotsDeserializer;
        private readonly IHttpClientFactory _httpClientFactory;

        public RobotsReader(IRobotsDeserializer robotsDeserializer, IHttpClientFactory httpClientFactory)
        {
            _robotsDeserializer = robotsDeserializer ?? throw new ArgumentNullException(nameof(robotsDeserializer));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public IReadOnlyCollection<Line> GetLines(string uriString, RobotsReaderOptions options = null)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }
            
            return GetLinesAsync(uriString, options).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        public Task<IReadOnlyCollection<Line>> GetLinesAsync(string uriString, RobotsReaderOptions options = null)
        {
            if (uriString is null)
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            var uri = new Uri(uriString);

            return GetLinesAsync(uri, options);
        }
        
        public IReadOnlyCollection<Line> GetLines(Uri uri, RobotsReaderOptions options = null)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            
            return GetLinesAsync(uri, options).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        public Task<IReadOnlyCollection<Line>> GetLinesAsync(Uri uri, RobotsReaderOptions options = null)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return LoadAndDeserializeFileAsync(uri, options);
        }

        private async Task<IReadOnlyCollection<Line>> LoadAndDeserializeFileAsync(Uri uri, RobotsReaderOptions options = null)
        {
            var immutableOptions = (options ?? new RobotsReaderOptions()).ToImmutable();
            
            var httpClient = _httpClientFactory.CreateClient(Constants.HttpClient.Name);
            httpClient.ConfigureUserAgent(immutableOptions.UserAgent);
            
            using (var contentStream = await httpClient.GetStreamAsync(uri))
            {
                return await _robotsDeserializer.GetDeserializedLinesAsync(contentStream, immutableOptions.DeserializerOptions);
            }
        }
    }
}