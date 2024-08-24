using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

[SuppressMessage("Performance", "SYSLIB1045:Convert to \'GeneratedRegexAttribute\'.")]
class Program
{
    private static int _usageCount;
    private static readonly List<string[]> CalculationHistory = [];
    
    [SuppressMessage("Performance", "SYSLIB1045:Convert to \'GeneratedRegexAttribute\'.")]
    private static void Main()
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        var calculator = new Calculator();
        
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine

            DeleteListOfPreviousCalculations();

            double cleanNum1 = SubmitInputNumber("first");
            double cleanNum2 = SubmitInputNumber("second");

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || ! Regex.IsMatch(op, "[a|s|m|d|r]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            { 
                try
                {
                    if (op.Equals("r"))
                    {
                        Console.WriteLine("Only the first number entered will be used for the square root operation.");
                    }
                    CarryOutCalculation(cleanNum1, cleanNum2, op, calculator);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
            
            Console.WriteLine($"The calculator has been used {_usageCount} {(_usageCount > 1 ? "times" : "time")}.\n"); // Friendly line spacing.
        }
        
        calculator.Finish();
    }

    private static double SubmitInputNumber(string position)
    {
        string? numInput;
        string? decision;
        double cleanNum;
        
        Console.WriteLine($"You will now enter the {position} number");
        if (CalculationHistory.Count > 0)
        {
            Console.WriteLine("Do you want to use the result of previous calculations?");
            Console.Write("Type y for yes or n for no, and then press Enter: ");
            decision = Console.ReadLine();
            
            while (decision == null || !Regex.IsMatch(decision, "[y|n]")) 
            {
                Console.WriteLine("Error: Unrecognized input.");
                Console.Write("Type y for yes or n for no, and then press Enter: ");
                decision = Console.ReadLine();
            }

            if (decision.Equals("y"))
            {
                Console.WriteLine("\nPrevious Results");
                Console.WriteLine("------------------------");
                HashSet<double> valuesSet = [];
                foreach (string[] calculation in CalculationHistory)
                {
                    Console.WriteLine(calculation[3]);
                    valuesSet.Add(double.Parse(calculation[3]));
                }
                Console.Write("\nChoose a value from the ones listed above, and then press Enter: ");
                numInput = Console.ReadLine();
                
                while (!double.TryParse(numInput, out cleanNum) || !valuesSet.Contains(cleanNum))
                {
                    Console.WriteLine("This is not a valid input.");
                    Console.Write("Choose a value from the ones listed above, and then press Enter: ");
                    numInput = Console.ReadLine();
                }
            }
            else
            {
                // Ask the user to type a number.
                Console.Write("\nType a number, and then press Enter: ");
                numInput = Console.ReadLine();
            
                while (!double.TryParse(numInput, out cleanNum))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput = Console.ReadLine();
                }
            }
        }
        else
        {
            // Ask the user to type a number.
            Console.Write("Type a number, and then press Enter: ");
            numInput = Console.ReadLine();
            
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }
        }

        return cleanNum;
    }

    private static void DeleteListOfPreviousCalculations()
    {
        if (CalculationHistory.Count <= 0) return;
        
        Console.WriteLine("Previous calculations exist, do you want to delete them?");
        Console.Write("Type y to delete or n to retain, and then press Enter: ");
        string? decision = Console.ReadLine();
                
        while (decision == null || !Regex.IsMatch(decision, "[y|n]")) 
        {
            Console.WriteLine("Error: Unrecognized input.");
            Console.Write("Type y to delete or n to retain, and then press Enter: ");
            decision = Console.ReadLine();
        }

        if (decision.Equals("y"))
            CalculationHistory.Clear();
        
        Console.WriteLine();
    }

    private static void CarryOutCalculation(double cleanNum1, double cleanNum2, string op, Calculator calculator)
    {
        string[] calculation = [
            cleanNum1.ToString(CultureInfo.InvariantCulture), 
            cleanNum2.ToString(CultureInfo.InvariantCulture), 
            GetOpSymbol(op),
            ""
        ];
        
        double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
        
        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error.\n");
        }
        else
        {
            Console.WriteLine("Your result: {0:0.##}\n", result);
            calculation[3] = result.ToString("0.##");
            CalculationHistory.Add(calculation);
        }
        
        _usageCount++;
    }

    private static string GetOpSymbol(string op)
    {
        string symbol = "";
        
        // ReSharper disable once ConvertSwitchStatementToSwitchExpression
        switch (op)
        {
            case "a":
                symbol = "+";
                break;
            case "s":
                symbol = "-";
                break;
            case "m":
                symbol = "*";
                break;
            case "d":
                symbol = "/";
                break;
        }

        return symbol;
    }
}
