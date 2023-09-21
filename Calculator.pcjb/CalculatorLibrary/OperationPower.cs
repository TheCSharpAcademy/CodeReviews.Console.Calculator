namespace CalculatorLibrary;

public class OperationPower : Operation
{
    public OperationPower() : base("p", "Power", 2)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount)
        {
            return double.NaN;
        }
        return Math.Pow(numbers[0], numbers[1]);
    }

    public override string Format(double[] numbers)
    {
        return String.Format("{0}^{1}", numbers[0], numbers[1]);
    }
}