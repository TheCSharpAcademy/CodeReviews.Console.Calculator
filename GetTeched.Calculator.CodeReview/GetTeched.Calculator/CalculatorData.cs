using CalculatorLibrary;

namespace CalculatorProgram;

internal class CalculatorData
{
    JsonParse jsonParse = new();
    internal List<string> CalculatorHistory()
    {
        List<string> previousCalculations = new();
        previousCalculations.AddRange(jsonParse.CalculationHistory());
        return previousCalculations;
    }
    internal void CalculatorStatistics()
    {
        int calculatorUsage = jsonParse.GetCalculatorUsageStats();
        Console.WriteLine($"This calculator has been used {calculatorUsage} of times");
    }
}
