using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.Operation;

namespace Calculator.Application;

public static class OperationUiToLogicMapper
{
    public static OperationType Map(OperationChoice uiChoice)
    {
        return uiChoice switch
        {
            OperationChoice.Addition => OperationType.Addition,
            OperationChoice.Subtraction => OperationType.Subtraction,
            OperationChoice.Multiplication => OperationType.Multiplication,
            OperationChoice.Division => OperationType.Division,
            OperationChoice.Power => OperationType.Power,
            OperationChoice.SquareRoot => OperationType.SquareRoot,
            OperationChoice.X10 => OperationType.X10,
            OperationChoice.Sine => OperationType.Sine,
            OperationChoice.Cosine => OperationType.Cosine,
            OperationChoice.Tangent => OperationType.Tangent,
            OperationChoice.Cotangent => OperationType.Cotangent,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}