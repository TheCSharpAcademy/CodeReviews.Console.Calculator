namespace CalculatorLibrary;

public class OperationCosine : Operation
{
    public OperationCosine() : base("c", "Cosine", 1)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount)
        {
            return double.NaN;
        }
        return Math.Cos(numbers[0]);
    }

    public override string Format(double[] numbers)
    {
        return String.Format("cos({0})", numbers[0]);
    }
}