// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text.Json;
using CrawlCtrl;

Console.Write("Enter a url for a robots.txt file: ");

var stringUrl = Console.ReadLine();

if (Uri.TryCreate(stringUrl, UriKind.Absolute, out var robotsTxtUri) is false)
{
    Console.WriteLine("Unable to parse the string to a url. Terminating.");
    Environment.Exit(1);
}

using var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CrawlCtrl", "1.0.0"));
using var stream = await httpClient.GetStreamAsync(robotsTxtUri);

var robotsDeserializer = new RobotsDeserializer();
var lines = await robotsDeserializer.GetDeserializedLinesAsync(stream, new RobotsDeserializerOptions()
{
    IncludeComments = true,
    IncludeSitemaps = true,
    IncludeEmptyLines = true,
    IncludeInvalidLines = true,
    IncludeUnknownDirectives = true,
    SitemapsInclusionScope = InclusionScope.All
});

Console.WriteLine();
Console.WriteLine("Result:");
Console.WriteLine();
foreach (var line in lines)
{
    switch (line)
    {
        case EmptyLine emptyLine:
            Console.WriteLine(JsonSerializer.Serialize(emptyLine));
            break;
        case InvalidLine invalidLine:
            Console.WriteLine(JsonSerializer.Serialize(invalidLine));
            break;
        case UnknownDirective unknownDirective:
            Console.WriteLine(JsonSerializer.Serialize(unknownDirective));
            break;
        case ValidSitemap validSitemap:
            Console.WriteLine(JsonSerializer.Serialize(validSitemap));
            break;
        case InvalidSitemap invalidSitemap:
            Console.WriteLine(JsonSerializer.Serialize(invalidSitemap));
            break;
    }
}