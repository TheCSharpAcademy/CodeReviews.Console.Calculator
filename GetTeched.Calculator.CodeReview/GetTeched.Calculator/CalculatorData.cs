using CalculatorLibrary;

namespace CalculatorProgram;

internal class CalculatorData
{
    
    internal void CalculatorHistory()
    {
        //CalculatorMenu claculatorMenu = new();
        JsonParse jsonParse = new();
        //claculatorMenu.RecordResultsJSON();

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
}
