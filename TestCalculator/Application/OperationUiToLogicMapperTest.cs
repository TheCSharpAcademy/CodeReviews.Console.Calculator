using Calculator.Application;
using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.Operation;
using FluentAssertions;

namespace TestCalculator.Application;

public class OperationUiToLogicMapperTest
{
    [Test]
    public void WillMapFromUiToLogic()
    {
        OperationUiToLogicMapper.Map(OperationChoice.Addition).Should().Be(OperationType.Addition);
        OperationUiToLogicMapper.Map(OperationChoice.Subtraction).Should().Be(OperationType.Subtraction);
        OperationUiToLogicMapper.Map(OperationChoice.Multiplication).Should().Be(OperationType.Multiplication);
        OperationUiToLogicMapper.Map(OperationChoice.Division).Should().Be(OperationType.Division);
        OperationUiToLogicMapper.Map(OperationChoice.Power).Should().Be(OperationType.Power);
        OperationUiToLogicMapper.Map(OperationChoice.SquareRoot).Should().Be(OperationType.SquareRoot);
        OperationUiToLogicMapper.Map(OperationChoice.X10).Should().Be(OperationType.X10);
        OperationUiToLogicMapper.Map(OperationChoice.Sine).Should().Be(OperationType.Sine);
        OperationUiToLogicMapper.Map(OperationChoice.Tangent).Should().Be(OperationType.Tangent);
        OperationUiToLogicMapper.Map(OperationChoice.Cotangent).Should().Be(OperationType.Cotangent);
    }
}