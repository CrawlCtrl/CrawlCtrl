using System;
using System.Collections.Generic;

namespace CrawlCtrl.Deserialization
{
    internal sealed class LineDeserializationCoordinator
    {
        private readonly ImmutableRobotsDeserializerOptions _options;
        private readonly IReadOnlyDictionary<string, ILineDeserializer<Line>> _lineDeserializers;

        public LineDeserializationCoordinator(IReadOnlyDictionary<string, ILineDeserializer<Line>> lineDeserializers, ImmutableRobotsDeserializerOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _lineDeserializers = lineDeserializers ?? throw new ArgumentNullException(nameof(lineDeserializers));
        }

        internal IReadOnlyDictionary<string, ILineDeserializer<Line>> LineDeserializers => _lineDeserializers;

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
                return _options.IncludeEmptyLines 
                    ? new EmptyLine(
                        value: lineComponents.Value,
                        comment: lineComponents.Comment, 
                        fullLine: line
                    )
                    : null;
            }

            if (lineComponents.Directive is null)
            {
                return _options.IncludeInvalidLines
                    ? new InvalidLine(
                        value: lineComponents.Value,
                        comment: lineComponents.Comment,
                        fullLine: line
                    )
                    : null;
            }

            var sanitizedDirective = lineComponents.Directive.Trim().ToLower();

            if (LineDeserializers.TryGetValue(sanitizedDirective, out var lineDeserializer))
            {
                var deserializedLine = lineDeserializer.Deserialize(
                    lineComponents.Directive,
                    lineComponents.Value,
                    lineComponents.Comment,
                    line,
                    _options
                );

                return deserializedLine;
            }

            return _options.IncludeUnknownDirectives ? new UnknownDirective(
                directive: lineComponents.Directive,
                value: lineComponents.Value,
                comment: lineComponents.Comment,
                fullLine: line
            ) : null;
        }
    }
}