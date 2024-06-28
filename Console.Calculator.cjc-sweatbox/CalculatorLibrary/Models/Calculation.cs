// -------------------------------------------------------------------------------------------------
// CalculatorLibrary.Models.Calculation
// -------------------------------------------------------------------------------------------------
// Represents a calculation: one or two operands, an operator and the result.
// -------------------------------------------------------------------------------------------------
using CalculatorLibrary.Constants;

namespace CalculatorLibrary.Models;

public class Calculation
{
    public double FirstNumber { get; init; }
    
    public double SecondNumber { get; init; }

    public char Option { get; init; }

    public string Symbol
    {
        get => char.ToLower(Option) switch
        {
            '+' => OperationSymbol.Addition,
            '-' => OperationSymbol.Subtraction,
            '*' => OperationSymbol.Multiplication,
            '/' => OperationSymbol.Division,
            'r' => OperationSymbol.SquareRoot,
            'e' => OperationSymbol.Exponentiation,
            'p' => OperationSymbol.Power,
            's' => OperationSymbol.Sine,
            'c' => OperationSymbol.Cosine,
            't' => OperationSymbol.Tangent,
            _ => throw new ArgumentOutOfRangeException(nameof(Option))
        };
    }
    
    public double Result { get; set; }

    public override string ToString()
    {
        if (AllowedChars.OneNumberCalculation.Contains(Option))
        {
            return $"{Symbol} {FirstNumber} = {Result}";
        }
        else
        {
            return $"{FirstNumber} {Symbol} {SecondNumber} = {Result}";
        }
    }
}
