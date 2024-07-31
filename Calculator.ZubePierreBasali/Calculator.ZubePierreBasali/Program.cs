using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            string? readResult = "";
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Display Menu of option for the calculator and process user input
                do
                {
                    Console.WriteLine("press 'p' and Enter to print data.");
                    Console.WriteLine("press 'd' and Enter to delete data from the data list");
                    Console.WriteLine("press 'n' and Enter to quit");
                    Console.WriteLine("press Enter to continue to calculation.");
                    readResult = Data.MainMenu();
                    if (readResult == "n") endApp = true;
                } while (readResult.ToLower() == "d" || readResult.ToLower() == "p");

                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                if (!endApp)
                {
                    double result = 0;

                    // Ask the user to type the first number.
                    double cleanNum1 = Data.GetNumber();
                    // Ask the user to type the second number.
                    double cleanNum2 = Data.GetNumber();
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.Write("Your option? ");

                    string? op = Console.ReadLine();

                    // Validate input is not null, and matches the pattern
                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
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
                            else Console.WriteLine("Your result: {0:0.##}\n", result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }

                    Console.WriteLine("------------------------\n");

                    Console.WriteLine($"Calculator was used {calculator.numberOfUse} time(s).\n");

                    Console.WriteLine("------------------------\n");

                    // Wait for the user to respond before closing.
                    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "n") endApp = true;

                    Console.WriteLine("\n"); // Friendly linespacing.
                }
            }
            calculator.Finish();
            return;
        }
    }
}