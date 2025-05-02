
namespace CalculatorLibrary.Models;

public class Operation
{
    public double Num1 { get; set; }
    public double Num2 { get; set; }
    public Operator Operator { get; set; }
    public double Result { get; set; }

    public string GetOperator() => Operator switch
    {
        Operator.Addition => "+",
        Operator.Subtraction => "-",
        Operator.Division => "/",
        Operator.Multiplication => "*",
        _ => ""
    };

    public double GetResult()
    {
        return Result;
    }
}

public enum Operator
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}
