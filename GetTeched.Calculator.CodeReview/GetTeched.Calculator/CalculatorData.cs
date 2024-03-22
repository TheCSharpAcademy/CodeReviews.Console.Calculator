using CalculatorLibrary;

namespace CalculatorProgram;

internal class CalculatorData
{
    JsonParse jsonParse = new();
    internal void CalculatorHistory()
    {
        foreach (string test in jsonParse.CalculationHistory())
        { 
            Console.WriteLine(test); 
        }

        Console.WriteLine("Would you like to clear this list? Type Yes to clear list or any other key to continue.");
        string? userInput = Console.ReadLine().ToLower().Trim();
        if (userInput == null)
        {
            Console.WriteLine("Please try again.");
        }
    }
    internal void CalculatorStatistics()
    {
        int calculatorUsage = jsonParse.GetCalculatorUsageStats();
        Console.WriteLine($"This calculator has been used {calculatorUsage} of times");
    }
}
