namespace CalculatorLibrary;
public class CalculationDataStore
{
    public static List<double> Results = [];

    public static double AddLastCalculation(double result)
    {
        Results.Add(result);
        return result;
    }
}
