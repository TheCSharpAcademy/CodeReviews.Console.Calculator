namespace CalculatorLibrary.Logic;

public static class OperationExecutor
{
    public static double Perform(OperationType operationType, double leftOperand, double rightOperand)
    {
        return operationType switch
        {
            OperationType.Addition => leftOperand + rightOperand,
            OperationType.Subtraction => leftOperand - rightOperand,
            OperationType.Multiplication => leftOperand * rightOperand,
            OperationType.Division => leftOperand / rightOperand,
            OperationType.Power => Math.Pow(leftOperand, rightOperand),
            _ => throw new ArgumentException()
        };
    }

    public static double Perform(OperationType operationType, double leftOperand)
    {
        return operationType switch
        {
            OperationType.SquareRoot => Math.Sqrt(leftOperand),
            OperationType.X10 => leftOperand * 10,
            OperationType.Sine => Math.Sin(leftOperand),
            OperationType.Cosine => Math.Cos(leftOperand),
            OperationType.Tangent => Math.Tan(leftOperand),
            OperationType.Cotangent => Math.Tanh(leftOperand),
            _ => throw new ArgumentException()
        };
    }
}