namespace CalculatorLibrary;

public class OperationDivide : Operation
{
    public OperationDivide() : base("d", "Divide", 2)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount || numbers[1] == 0)
        {
            return double.NaN;
        }
        return numbers[0] / numbers[1];
    }

    public override string Format(double[] numbers)
    {
        return String.Format("{0} / {1}", numbers[0], numbers[1]);
    }
}