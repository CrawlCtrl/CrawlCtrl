using CrawlCtrl.Deserialization;
using NSubstitute;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializingWithDeserializerTests
{
    private readonly ILineDeserializer<TestLine> _deserializerMock = Substitute.For<ILineDeserializer<TestLine>>();
    private readonly LineDeserializationCoordinator _coordinator;

    public DeserializingWithDeserializerTests()
    {
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        _coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions
            {
                IncludeComments = true
            }.ToImmutableOrDefault()
        );
    }

    [Fact]
    public void WHEN_No_deserializer_exists_for_directive_THEN_Do_not_deserialize_with_deserializer()
    {
        // Arrange
        const string line = $"wrong-directive: test-value # test comment";
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.DidNotReceiveWithAnyArgs().Deserialize(default, default, default, default, default);
    }

    [Fact]
    public void WHEN_Deserializer_exists_for_directive_THEN_Deserialize_with_deserializer()
    {
        // Arrange
        const string line = $"test-directive: test-value # test comment";
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.ReceivedWithAnyArgs(1).Deserialize(default, default, default, default, default);
    }
    
    [Theory]
    [InlineData($"test-directive: test-value # test comment", $"test-directive")]
    [InlineData($" test-directive: test-value # test comment", $" test-directive")]
    [InlineData($"test-directive : test-value # test comment", $"test-directive ")]
    [InlineData($" test-directive : test-value # test comment", $" test-directive ")]
    public void WHEN_Deserializer_exists_for_directive_THEN_Pass_raw_directive_to_deserializer(string line, string expectedDirective)
    {
        // Arrange
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(expectedDirective, Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ImmutableRobotsDeserializerOptions>());
    }

    [Theory]
    [InlineData("test-directive:test-value# test comment", "test-value")]
    [InlineData("test-directive: test-value# test comment", " test-value")]
    [InlineData("test-directive:test-value # test comment", "test-value ")]
    [InlineData("test-directive: test-value # test comment", " test-value ")]
    public void WHEN_Deserializer_exists_for_directive_THEN_Pass_raw_value_to_deserializer(string line, string expectedValue)
    {
        // Arrange
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), expectedValue, Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ImmutableRobotsDeserializerOptions>());
    }
    
    [Theory]
    [InlineData("test-directive: test-value #test comment", "test comment")]
    [InlineData("test-directive: test-value # test comment", " test comment")]
    [InlineData("test-directive: test-value #test comment ", "test comment ")]
    [InlineData("test-directive: test-value # test comment ", " test comment ")]
    public void WHEN_Deserializer_exists_for_directive_THEN_Pass_raw_comment_to_deserializer(string line, string expectedComment)
    {
        // Arrange
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), expectedComment, Arg.Any<string>(), Arg.Any<ImmutableRobotsDeserializerOptions>());
    }
    
    [Theory]
    [InlineData("test-directive: test-value # Test comment")]
    [InlineData(" test-directive: test-value # Test comment")]
    [InlineData("test-directive: test-value # Test comment ")]
    [InlineData(" test-directive: test-value # Test comment ")]
    public void WHEN_Deserializer_exists_for_directive_THEN_Pass_original_line_to_deserializer(string line)
    {
        // Arrange
        var expectedFullLine = line;
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), expectedFullLine, Arg.Any<ImmutableRobotsDeserializerOptions>());
    }

    [Theory]
    [InlineData("test-directive: test-value # test comment")]
    [InlineData(" test-directive: test-value # test comment")]
    [InlineData("test-directive : test-value # test comment")]
    [InlineData(" test-directive : test-value # test comment")]
    [InlineData(" TEST-directive : test-value # test comment")]
    [InlineData("TEST-DIRECTIVE: test-value # test comment")]
    public void WHEN_Deserializer_exists_for_directive_THEN_Ignore_leading_and_trailing_whitespaces_in_directive_when_finding_deserializer(string line)
    {
        // Arrange
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ImmutableRobotsDeserializerOptions>());
    }
    
    [Theory]
    [InlineData("test-directive: test-value # test comment")]
    [InlineData("TEST-directive: test-value # test comment")]
    [InlineData("TEST-DIRECTIVE: test-value # test comment")]
    [InlineData("TeSt-DiReCtIvE: test-value # test comment")]
    public void WHEN_Deserializer_exists_for_directive_THEN_Ignore_casing_in_directive_when_finding_deserializer(string line)
    {
        // Arrange
        
        // Act
        _coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ImmutableRobotsDeserializerOptions>());
    }

    internal sealed class TestLine : Line
    {
        public TestLine(string value, string? comment = null) : base(value, comment)
        {
            
        }
    }
}