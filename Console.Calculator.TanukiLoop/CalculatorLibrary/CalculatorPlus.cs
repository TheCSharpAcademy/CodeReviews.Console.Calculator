namespace CalculatorLibrary;

public class CalculatorPlus: Calculator
{
    public List<MathOperation> History { get; init; } = new();
    public override double DoOperation(double num1, double num2, string op)
    {
        var result =  base.DoOperation(num1,num2,op);
        
        History.Add(new MathOperation(num1, num2, op, result));
        
        return result;

    }

    public void ClearHistory()
    {
        History.Clear();
    }
    
}

public record MathOperation(double operand1, double operand2, string op, double result)
{
    public override string ToString()
    {
        var opSymbol = op switch
        {
            "a" => "+",
            "s" => "-",
            "m" => "*",
            "d" => "/",
            _ => throw new InvalidOperationException("Invalid op")
        };
        return $"{operand1} {opSymbol} {operand2} = {result}";

    }
}
