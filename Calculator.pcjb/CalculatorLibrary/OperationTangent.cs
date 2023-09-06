namespace CalculatorLibrary;

public class OperationTangent : Operation
{
    public OperationTangent() : base("t", "Tangent", 1)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount)
        {
            return double.NaN;
        }
        return Math.Tan(numbers[0]);
    }

    public override string Format(double[] numbers)
    {
        return String.Format("tan({0})", numbers[0]);
    }
}