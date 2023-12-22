using System;
using System.Collections.Generic;

namespace CrawlCtrl.Deserialization
{
    internal class LineDeserializationCoordinator
    {
        private readonly RobotsDeserializerOptions _options;

        public LineDeserializationCoordinator(IReadOnlyDictionary<string, ILineDeserializer<Line>> lineDeserializers, RobotsDeserializerOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            
            LineDeserializers = lineDeserializers ?? throw new ArgumentNullException(nameof(lineDeserializers));
        }
        
        public readonly IReadOnlyDictionary<string, ILineDeserializer<Line>> LineDeserializers;

        public Line Deserialize(string line)
        {
            if (line is null)
            {
                throw new ArgumentNullException(nameof(line));
            }
            
            var lineComponents = line.SplitIntoLineComponents(
                includeComment: _options.IncludeComments
            );

            if (lineComponents.Directive is null && string.IsNullOrWhiteSpace(lineComponents.Value))
            {
                return _options.IncludeEmptyLines ? new EmptyLine(lineComponents.Value, lineComponents.Comment) : null;
            }

            if (lineComponents.Directive is null)
            {
                return _options.IncludeInvalidLines ? new InvalidLine(lineComponents.Value, lineComponents.Comment) : null;
            }

            if (LineDeserializers.TryGetValue(lineComponents.Directive.ToLower(), out var lineDeserializer))
            {
                var deserializedLine = lineDeserializer.Deserialize(
                    lineComponents.Value,
                    lineComponents.Comment
                );

                return deserializedLine;
            }

            return _options.IncludeUnknownDirectives ? new UnknownDirective(
                directive: lineComponents.Directive,
                value: lineComponents.Value,
                comment: lineComponents.Comment
            ) : null;
        }
    }
}