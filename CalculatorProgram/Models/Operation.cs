namespace CalculatorLibrary.Models;

internal class Operation(OperationType operationType)
{
    public OperationType OperationType { get; set; }
    public double Operand1 { get; set; }
    public double? Operand2 { get; set; } // Optional for unary operations
    public double Result { get; set; }
}

public enum OperationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    Exponentiation,
    SquareRoot,
    Sine,
    Cosine,
    Tangent,
}
