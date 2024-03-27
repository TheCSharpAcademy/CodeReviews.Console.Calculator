namespace CalculatorLibrary.Models;

internal class Operation()
{
    public DateTime Date { get; set; }
    public double Number1 { get; set; }
    public double Number2 { get; set; }
    public double Result { get; set; }
    public OperationType Type { get; set; }
}

public enum OperationType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
    SquareRoot,
    TakingThePower,
    x10,
    Sin,
    Cos,
    Tan
}
