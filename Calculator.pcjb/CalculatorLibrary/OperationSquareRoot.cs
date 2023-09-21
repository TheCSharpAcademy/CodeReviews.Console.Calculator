namespace CalculatorLibrary;

public class OperationSquareRoot : Operation
{
    public OperationSquareRoot() : base("r", "Square Root", 1)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount)
        {
            return double.NaN;
        }
        return Math.Sqrt(numbers[0]);
    }

    public override string Format(double[] numbers)
    {
        return String.Format("sqrt({0})", numbers[0]);
    }
}