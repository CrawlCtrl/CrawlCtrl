using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrawlCtrl.Deserialization;

namespace CrawlCtrl
{
    public sealed class RobotsDeserializer : IRobotsDeserializer
    {
        public IReadOnlyCollection<Line> GetDeserializedLines(Stream robotsStream, RobotsDeserializerOptions options = null)
        {
            if (robotsStream is null)
            {
                throw new ArgumentNullException(nameof(robotsStream));
            }
            
            return GetDeserializedLinesAsync(robotsStream, options).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        public IReadOnlyCollection<Line> GetDeserializedLines(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null)
        {
            if (robotsStreamReader is null)
            {
                throw new ArgumentNullException(nameof(robotsStreamReader));
            }
            
            return GetDeserializedLinesAsync(robotsStreamReader, options).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        public async Task<IReadOnlyCollection<Line>> GetDeserializedLinesAsync(Stream robotsStream, RobotsDeserializerOptions options = null)
        {
            if (robotsStream is null)
            {
                throw new ArgumentNullException(nameof(robotsStream));
            }
            
            using (var robotsStreamReader = new StreamReader(robotsStream))
            {
                return await GetDeserializedLinesAsync(robotsStreamReader, options);
            }
        }
        
        public async Task<IReadOnlyCollection<Line>> GetDeserializedLinesAsync(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null)
        {
            if (robotsStreamReader is null)
            {
                throw new ArgumentNullException(nameof(robotsStreamReader));
            }

            var lineDeserializationCoordinator = LineDeserializationCoordinatorFactory.Build(options);

            var lines = new List<Line>();
            while (robotsStreamReader.EndOfStream is false)
            {
                var lineText = await robotsStreamReader.ReadLineAsync();
                var line = lineDeserializationCoordinator.Deserialize(lineText);

                if (line != null)
                {
                    lines.Add(line);
                }
            }

            return lines.AsReadOnly();
        }
        
        public IEnumerable<Line> EnumerateDeserializedLines(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null)
        {
            if (robotsStreamReader is null)
            {
                throw new ArgumentNullException(nameof(robotsStreamReader));
            }
            
            var lineDeserializationCoordinator = LineDeserializationCoordinatorFactory.Build(options);
            
            while (robotsStreamReader.EndOfStream is false)
            {
                var lineText = robotsStreamReader.ReadLine();
                var line = lineDeserializationCoordinator.Deserialize(lineText);

                if (line != null)
                {
                    yield return line;
                }
            }
        }
        
#if NETSTANDARD2_1_OR_GREATER

        public IAsyncEnumerable<Line> EnumerateDeserializedLinesAsync(Stream robotsStream, RobotsDeserializerOptions options = null)
        {
            using var robotsStreamReader = new StreamReader(robotsStream);
            return EnumerateDeserializedLinesAsync(robotsStreamReader, options);
        }

        public async IAsyncEnumerable<Line> EnumerateDeserializedLinesAsync(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null)
        {
            if (robotsStreamReader is null)
            {
                throw new ArgumentNullException(nameof(robotsStreamReader));
            }
            
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var lineDeserializationCoordinator = LineDeserializationCoordinatorFactory.Build(options);
            
            while (robotsStreamReader.EndOfStream is false)
            {
                var lineText = await robotsStreamReader.ReadLineAsync();
                var line = lineDeserializationCoordinator.Deserialize(lineText);

                if (line != null)
                {
                    yield return line;
                }
            }
        }
#endif
    }
}