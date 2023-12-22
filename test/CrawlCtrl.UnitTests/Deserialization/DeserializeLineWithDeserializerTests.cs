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
        _deserializerMock.DidNotReceiveWithAnyArgs().Deserialize(default, default);
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
        _deserializerMock.ReceivedWithAnyArgs(1).Deserialize(default, default);
    }

    [Fact]
    public void WHEN_Line_has_value_WHILE_Deserializer_exists_for_directive_THEN_Pass_value_unchanged_to_deserializer()
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
        _deserializerMock.Received(1).Deserialize(expectedValue, Arg.Any<string>());
    }
    
    [Fact]
    public void WHEN_Line_has_comment_WHILE_Deserializer_exists_for_directive_THEN_Pass_comment_unchanged_to_deserializer()
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
        _deserializerMock.Received(1).Deserialize(Arg.Any<string>(), expectedComment);
    }

    public sealed class TestLine : Line
    {
    }
}