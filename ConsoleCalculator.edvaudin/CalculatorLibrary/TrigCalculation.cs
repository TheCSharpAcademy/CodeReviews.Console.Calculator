namespace CalculatorLibrary;

public class TrigCalculation : Calculation
{
    public TrigCalculation(double num1, double num2, double result, string op) : base(num1, num2, result, op)
    {
        this.num1 = num1;
        this.num2 = num2;
        Result = result;
        operatorSymbol = op;
    }

    public override string ToString()
    {
        return $"{operatorSymbol}({num1}) = {Result}\n";
    }
}
