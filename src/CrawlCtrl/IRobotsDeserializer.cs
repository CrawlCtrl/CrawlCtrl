using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrawlCtrl
{
    public interface IRobotsDeserializer
    {
        IReadOnlyCollection<Line> GetDeserializedLines(Stream robotsStream, RobotsDeserializerOptions options = null);
        IReadOnlyCollection<Line> GetDeserializedLines(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null);
        Task<IReadOnlyCollection<Line>> GetDeserializedLinesAsync(Stream robotsStream, RobotsDeserializerOptions options = null);
        IEnumerable<Line> EnumerateDeserializedLines(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null);
        Task<IReadOnlyCollection<Line>> GetDeserializedLinesAsync(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null);
        
#if NETSTANDARD2_1_OR_GREATER
        IAsyncEnumerable<Line> EnumerateDeserializedLinesAsync(Stream robotsStream, RobotsDeserializerOptions options = null);
        IAsyncEnumerable<Line> EnumerateDeserializedLinesAsync(StreamReader robotsStreamReader, RobotsDeserializerOptions options = null);
#endif
    }
}