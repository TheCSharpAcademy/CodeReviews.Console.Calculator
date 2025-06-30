using Newtonsoft.Json;
using Spectre.Console;

namespace CalculatorLibrary;

internal class Helpers
{
    internal static List<string> calculationList = new();

    public static void PrintTwoNumberCalculation(
        double num1,
        double num2,
        string operationType,
        double result
    )
    {
        Console.WriteLine($"\t The result of {num1} {operationType} {num2} is {result}\n");
    }

    public static void PrintSingleNumberCalculation(double num, string operationType, double result)
    {
        Console.WriteLine($"\t The result of {operationType} {num} is {result}\n");
    }

    public static string AddToCalculationList(string calculation)
    {
        calculationList.Add(calculation);
        return calculation;
    }

    public static double GetPreviousResult(List<double> previousResults)
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<double>()
                .Title("Please choose a result: ")
                .AddChoices(previousResults)
        );

        return option;
    }

    public static void PrintCalculationList()
    {
        Console.Clear();
        if (calculationList.Count == 0)
        {
            AnsiConsole.WriteLine("No calculations have been performed yet.");
            return;
        }

        AnsiConsole.WriteLine("----------------------------------------------------\n");
        AnsiConsole.WriteLine("\t Calculation History:\n");
        foreach (var calculation in calculationList)
            AnsiConsole.WriteLine($"\t {calculation}");

        AnsiConsole.WriteLine("\n----------------------------------------------------\n");
        PrintCalculationCount();
        DeleteCalculationList();
    }

    public static void DeleteCalculationList()
    {
        AnsiConsole.WriteLine("\n----------------------------------------------------\n");
        var clearHistory = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to clear the calculation history?")
                .AddChoices("Yes", "No")
        );

        if (clearHistory == "Yes")
        {
            calculationList.Clear();
            CalculatorEngine.Results.Clear();
            AnsiConsole.WriteLine("Calculation history cleared.");
        }
        else if (clearHistory == "No")
        {
            AnsiConsole.WriteLine("Calculation history retained.");
        }
    }

    public static double[] GetTwoNumbers()
    {
        var input1 = "";
        var input2 = "";
        var result = new double[2];

        if (calculationList.Count == 0)
        {
            Console.WriteLine("Enter your first number: ");
            input1 = Console.ReadLine();
        }
        else
        {
            var usePreviousResult = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to use a previous result?")
                    .AddChoices("Yes", "No")
            );

            if (usePreviousResult == "Yes")
            {
                input1 = GetPreviousResult(CalculatorEngine.Results).ToString();
            }
            else
            {
                Console.WriteLine("Enter your first number: ");
                input1 = Console.ReadLine();
            }
        }

        double cleanNum1 = 0;
        while (!double.TryParse(input1, out cleanNum1))
        {
            Console.Write("Invalid input. Please enter a numeric value: ");
            input1 = Console.ReadLine();
        }

        Console.WriteLine("Enter your second number: ");
        input2 = Console.ReadLine();

        double cleanNum2 = 0;
        while (!double.TryParse(input2, out cleanNum2))
        {
            Console.Write("Invalid input. Please enter a numeric value: ");
            input2 = Console.ReadLine();
        }

        result[0] = cleanNum1;
        result[1] = cleanNum2;
        return result;
    }

    public static double GetSingleNumber()
    {
        var input = "";
        double cleanNum = 0;

        if (calculationList.Count == 0)
        {
            Console.WriteLine("Enter your first number: ");
            input = Console.ReadLine();
        }
        else
        {
            var usePreviousResult = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Do you want to use a previous result?")
                    .AddChoices("Yes", "No")
            );

            if (usePreviousResult == "Yes")
            {
                input = GetPreviousResult(CalculatorEngine.Results).ToString();
            }
            else
            {
                Console.WriteLine("Enter your first number: ");
                input = Console.ReadLine();
            }
        }

        while (!double.TryParse(input, out cleanNum))
        {
            Console.Write("Invalid input. Please enter a numeric value: ");
            input = Console.ReadLine();
        }

        return cleanNum;
    }

    internal static void PrintCalculationCount()
    {
        AnsiConsole.WriteLine($"Calculator used {calculationList.Count} times");
    }

    public static void SaveCalculationHistoryToFile(string filePath)
    {
        try
        {
            var json = JsonConvert.SerializeObject(calculationList, Formatting.Indented);
            File.WriteAllText(filePath, json);
            AnsiConsole.WriteLine("Calculation history saved successfully.");
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteLine($"Error saving calculation history: {ex.Message}");
        }
    }
}
