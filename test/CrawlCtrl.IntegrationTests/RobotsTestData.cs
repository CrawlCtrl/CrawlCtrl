using System.Collections.ObjectModel;

namespace CrawlCtrl.IntegrationTests;

internal static class RobotsTestData
{
    public static IReadOnlyList<Line> EmptyRobotsTxt = new List<Line>
    {
        
    }.AsReadOnly();
    
    public static IReadOnlyList<Line> CompleteRobotsTxt = new List<Line>
    {
        new UnknownDirective("User-agent", " *"),
        new UnknownDirective("Disallow", " /disallowed/"),
        new UnknownDirective("Allow", " /disallowed/allowed-anyways"),
        new EmptyLine(""),
        new ValidSitemap(new Uri("https://www.example.com/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/en/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/da/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/de/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/es/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/fr/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/it/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/sitemap.xml"))
    }.AsReadOnly();
    
    public static IReadOnlyList<Line> OnlySitemapsRobotsTxt = new List<Line>
    {
        new ValidSitemap(new Uri("https://www.example.com/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/en/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/da/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/de/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/es/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/fr/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/it/sitemap_index.xml")),
        new ValidSitemap(new Uri("https://www.example.com/sitemap.xml"))
    }.AsReadOnly();

    public static StreamReader GetEmptyRobotsStreamReader() => GetStreamReaderForFile("./robots_empty.txt");
    public static StreamReader GetCompleteRobotsStreamReader() => GetStreamReaderForFile("./robots_complete.txt");
    public static StreamReader GetOnlySitemapsRobotsStreamReader() => GetStreamReaderForFile("./robots_only_sitemaps.txt");

    private static StreamReader GetStreamReaderForFile(string path)
    {
        return new StreamReader(File.OpenRead(path));
    }
}