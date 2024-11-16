using CalculatorLibrary.Logic;

namespace CalculatorLibrary.UI.Operation;

public static class OperationTypeToPresentationMapper
{
    public static string Map(OperationType operationType)
    {
        return operationType switch
        {
            OperationType.Addition => OperationTypePresentation.Addition,
            OperationType.Subtraction => OperationTypePresentation.Subtraction,
            OperationType.Multiplication => OperationTypePresentation.Multiplication,
            OperationType.Division => OperationTypePresentation.Division,
            OperationType.SquareRoot => OperationTypePresentation.SquareRoot,
            OperationType.Power => OperationTypePresentation.Power,
            OperationType.X10 => OperationTypePresentation.X10,
            OperationType.Sine => OperationTypePresentation.Sine,
            OperationType.Cosine => OperationTypePresentation.Cosine,
            OperationType.Tangent => OperationTypePresentation.Tangent,
            OperationType.Cotangent => OperationTypePresentation.Cotangent,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}