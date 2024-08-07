using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        // Control the usage of the calculator via a text file
        UsageTracker usageTracker = new UsageTracker();

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            bool endMenu = false;

            while (!endMenu)
            {
                PrintMenu();

                if(Enum.TryParse<UserOption>(Console.ReadLine(), out UserOption optionSelected))
                {
                    switch (optionSelected)
                    {
                        case UserOption.Calculator:
                            endMenu = true;
                            break;
                        case UserOption.NumberOfUses:
                            Console.WriteLine($"Number of uses: {usageTracker.GetNumberUses()}");
                            break;
                        case UserOption.ShowLatestCalculations:
                            int i = 1;
                            foreach (double calc in calculator.Results)
                            {
                                Console.WriteLine("{0}: {1:0.##}", i, calc);
                                i++;
                            }
                            break;
                        case UserOption.DeleteLatestCalculations:
                            calculator.Results.Clear();
                            break;
                        default:
                            InvalidOption();
                            break;
                    }
                }
                else
                {
                    InvalidOption();
                }
            }

            // Ask the user to type the first number.
            Console.Write("Type a number or use *result from the list of results, and then press Enter: ");
            numInput1 = Console.ReadLine();
            numInput1 = FormatResult(numInput1, calculator);

            double cleanNum1 = 0;

            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
                numInput1 = FormatResult(numInput1, calculator);
            }

            // Ask the user to type the second number.
            Console.Write("Type another number or use *result from the list of results, and then press Enter: ");
            numInput2 = Console.ReadLine();
            numInput2 = FormatResult(numInput2, calculator);

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
                numInput2 = FormatResult(numInput2, calculator);
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else 
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        calculator.Results.Add(result);
                    } 
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            usageTracker.IncrementNumberUses();

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") 
            { 
                endApp = true;
                usageTracker.SetNumberUses();
            }

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
    private static void InvalidOption()
    {
        Console.WriteLine("Not a valid option, Try Again!");
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Uses Calculator");
        Console.WriteLine("2. Number Of Uses");
        Console.WriteLine("3. Show Latest Calculations");
        Console.WriteLine("4. Delete Latest Calculations");
        Console.WriteLine();
    }

    // Format the input to make use the Result list
    private static string? FormatResult(string? input, Calculator calculator)
    {
        if (input == null) return input;
        if (!input.StartsWith('*')) return input;

        string index = input.TrimStart('*');
        if(int.TryParse(index, out int resultIndex))
        {
            resultIndex = resultIndex - 1;
            if(resultIndex >= 0 && resultIndex < calculator.Results.Count)
                return calculator.Results.ElementAtOrDefault(resultIndex).ToString();
        }

        return string.Empty;
    }
}


public enum UserOption { Calculator = 1, NumberOfUses = 2, ShowLatestCalculations = 3, DeleteLatestCalculations = 4 }