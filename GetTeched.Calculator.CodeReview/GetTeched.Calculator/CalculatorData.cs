using CalculatorLibrary;

namespace CalculatorProgram;

internal class CalculatorData
{
    JsonParse jsonParse = new();
    internal List<string> CalculatorHistory()
    {
        foreach (string test in jsonParse.CalculationHistory())
        { 
            Console.WriteLine(test);
        }

        Console.WriteLine("Would you like to clear this list? Type Yes to clear list or any other key to continue.");
        string? userInput = Console.ReadLine().ToLower().Trim();
        if (userInput == null)
        {
            if(userInput == "yes")
            {
                jsonParse.CalculationHistory().Clear();
                Console.WriteLine("Calculation history has been cleard. Press any key to return to the Main Menu.");
            }
            Console.WriteLine("Please try again.");
        }
        return jsonParse.CalculationHistory();
    }
    internal void CalculatorStatistics()
    {
        int calculatorUsage = jsonParse.GetCalculatorUsageStats();
        Console.WriteLine($"This calculator has been used {calculatorUsage} of times");
    }
}
