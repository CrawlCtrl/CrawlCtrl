# CrawlCtrl
CrawlCtrl makes it ease to deserialize the content of robots.txt files to objects.

```csharp
var robotsDeserializer = new RobotsDeserializer();
var robotsLines = await robotsDeserializer.GetDeserializedLinesAsync(robotsTxtContentStream);
    
foreach(var robotsLine in robotsLines)
{
    // Handle lines
}
```

The deserialization methods takes an optional `RobotsDeserializerOptions` instance, that defines how the provided robots.txt content should be deserialized.

```csharp
var options = new RobotsDeserializerOptions
{
    IncludeComments = true,
    IncludeSitemaps = true,
    SitemapsInclusionScope = InclusionScope.All
}

var robotsLines = await robotsDeserializer.GetDeserializedLinesAsync(robotsTxtContentStream, options);
```

The following options are available:

| Name                     | Description |
|:-------------------------|:------------|
| IncludeComments          |             |
| IncludeEmptyLines        |             |
| IncludeInvalidLines      |             |
| IncludeUnknownDirectives |             |
| IncludeSitemaps          |             |
| SitemapsInclusionScope   |             |

## Line types
By definition, all robots.txt lines has a `Value` property and an optional `Comment` property. These properties contains the original value and comment as strings, including leading and trailing whitespaces.

The companion properties `TrimmedValue` and `TrimmedComment` contains the same values, with leading and trailing whitespaces removed.

| Name             | Description                                                            | Status    |
|:-----------------|:-----------------------------------------------------------------------|:----------|
| EmptyLine        | An empty line, where `Value` is empty or consists of only whitespaces. | Supported |
| InvalidLine      | A line without directive                                               | Supported |
| UnknownDirective |                                                                        | Supported |
| ValidSitemap     |                                                                        | Supported |
| InvalidSitemap   |                                                                        | Supported |

## Contribute
Coming soon