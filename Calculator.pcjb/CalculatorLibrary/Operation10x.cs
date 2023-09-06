namespace CalculatorLibrary;

public class Operation10x : Operation
{
    public Operation10x() : base("x", "10x", 1)
    { }

    public override double GetResult(double[] numbers)
    {
        if (numbers == null || numbers.Length < ParamCount)
        {
            return double.NaN;
        }
        return 10 * numbers[0];
    }

    public override string Format(double[] numbers)
    {
       return String.Format("10*{0}",  numbers[0]);
    }
}