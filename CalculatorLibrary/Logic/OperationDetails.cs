namespace CalculatorLibrary.Logic;

public record OperationDetails(
    double LeftOperand,
    OperationType OperationType,
    double Result,
    double? RightOperand = null)
{
}