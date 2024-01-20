using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrawlCtrl.Reader
{
    public interface IRobotsReader
    {
        IReadOnlyCollection<Line> GetLines(string uriString, RobotsReaderOptions options = null);
        Task<IReadOnlyCollection<Line>> GetLinesAsync(string uriString, RobotsReaderOptions options = null);
        IReadOnlyCollection<Line> GetLines(Uri uri, RobotsReaderOptions options = null);
        Task<IReadOnlyCollection<Line>> GetLinesAsync(Uri uri, RobotsReaderOptions options = null);
    }
}