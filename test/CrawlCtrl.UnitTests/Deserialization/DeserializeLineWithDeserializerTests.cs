using CrawlCtrl.Deserialization;
using NSubstitute;
using Xunit;

namespace CrawlCtrl.UnitTests.Deserialization;

public sealed class DeserializeLineWithDeserializerTests
{
    private readonly ILineDeserializer<TestLine> _deserializerMock = Substitute.For<ILineDeserializer<TestLine>>();

    [Fact]
    public void WHEN_No_deserializer_exists_for_directive_THEN_Do_not_deserialize_with_deserializer()
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "wrong-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions()
        );

        const string line = "test-directive: test-value # test comment";
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.DidNotReceiveWithAnyArgs().Deserialize(default, default, default, default);
    }

    [Fact]
    public void WHEN_Deserializer_exists_for_directive_THEN_Deserialize_with_deserializer()
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions()
        );

        const string line = "test-directive: test-value # test comment";
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.ReceivedWithAnyArgs(1).Deserialize(default, default, default, default);
    }
    
    [Fact]
    public void WHEN_Line_has_directive_WHILE_Deserializer_exists_for_directive_THEN_Pass_raw_directive_to_deserializer()
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions()
        );

        const string line = " test-directive: test-value # test comment";
        const string expectedDirective = " test-directive";
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(expectedDirective, Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
    }
    
    [Theory]
    [InlineData("test-directive: test-value # Test comment", "test-directive: test-value # Test comment")]
    [InlineData(" test-directive: test-value # Test comment", " test-directive: test-value # Test comment")]
    [InlineData("test-directive: test-value # Test comment ", "test-directive: test-value # Test comment ")]
    [InlineData(" test-directive: test-value # Test comment ", " test-directive: test-value # Test comment ")]
    public void WHEN_Line_has_directive_WHILE_Deserializer_exists_for_directive_THEN_Pass_line_to_deserializer(string line, string expectedFullLine)
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions()
        );
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), expectedFullLine);
    }

    [Fact]
    public void WHEN_Line_has_value_WHILE_Deserializer_exists_for_directive_THEN_Pass_raw_value_to_deserializer()
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions()
        );

        const string line = "test-directive: test-value # test comment";
        const string expectedValue = " test-value ";
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), expectedValue, Arg.Any<string>(), Arg.Any<string>());
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Deserializer_exists_for_directive_THEN_Pass_raw_comment_to_deserializer()
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions
            {
                IncludeComments = true
            }
        );

        const string line = "test-directive: test-value # test comment";
        const string expectedComment = " test comment";
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), expectedComment, Arg.Any<string>());
    }

    [Theory]
    [InlineData("test-directive: test-value # test comment")]
    [InlineData(" test-directive: test-value # test comment")]
    [InlineData("test-directive : test-value # test comment")]
    [InlineData(" test-directive : test-value # test comment")]
    [InlineData(" TEST-directive : test-value # test comment")]
    public void WHEN_Line_has_directive_THEN_Use_trimmed_and_lower_case_directive_to_identify_deserializer(string line)
    {
        // Arrange
        var lineDeserializers = new Dictionary<string, ILineDeserializer<Line>>
        {
            { "test-directive", _deserializerMock }
        };
        
        var coordinator = new LineDeserializationCoordinator(
            lineDeserializers: lineDeserializers,
            options: new RobotsDeserializerOptions()
        );
        
        // Act
        coordinator.Deserialize(line);

        // Assert
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
    }

    public sealed class TestLine : Line
    {
        public TestLine(string value, string? comment = null) : base(value, comment)
        {
            
        }
    }
}