using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.Operation;
using FluentAssertions;

namespace TestCalculator.UI.Operation;

public class OperationTypeToPresentationMapperTests
{
    [Test]
    [TestCaseSource(nameof(_cases))]
    public void WillMapToCorrectString((OperationType type, string expectedResult) caseTuple)
    {
        OperationTypeToPresentationMapper.Map(caseTuple.type).Should().Be(caseTuple.expectedResult);
    }

    private static (OperationType, string)[] _cases =
    [
        (OperationType.Addition, "+"),
        (OperationType.Subtraction, "-"),
        (OperationType.Division, "/"),
        (OperationType.Multiplication, "*"),
        (OperationType.SquareRoot, "\u221a"),
        (OperationType.Power, "^"),
        (OperationType.X10, "x10"),
        (OperationType.Sine, "sin"),
        (OperationType.Cosine, "cos"),
        (OperationType.Tangent, "tan"),
        (OperationType.Cotangent, "cot")
    ];
}