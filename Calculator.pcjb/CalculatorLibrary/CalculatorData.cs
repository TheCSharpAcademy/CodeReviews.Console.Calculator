
namespace CalculatorLibrary;

public class CalculatorData
{
    public int UsageCount { get; set; }
    public List<Calculation>? Calculations { get; set; }
    
    public void AddCalculation(Calculation calculation)
    {
        Calculations ??= new List<Calculation>();
        Calculations.Add(calculation);
    }

    internal void DeleteCalculations()
    {
        Calculations?.Clear();
    }
}