namespace CalculatorLibrary;

public class OperationSine : Operation
{
    public OperationSine() : base("i", "Sine", 1)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount)
        {
            return double.NaN;
        }
        return Math.Sin(numbers[0]);
    }

    public override string Format(double[] numbers)
    {
        return String.Format("sin({0})", numbers[0]);
    }
}