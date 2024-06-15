using CalculatorLibrary;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
namespace CalculatorProgram;

class Program
{    static void Main()
    {
        bool endApp = false;
        int operationCount = 0;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask user if they want to view history
            Console.Clear();
            Console.WriteLine("Select from the list below: ");
            Console.WriteLine("1 - View History");
            Console.WriteLine("2 - Calculator");
            Console.WriteLine("----------------------------");
            numInput1 = Console.ReadLine();

            Console.Clear();            
            
            switch (numInput1)
            {
                case "1":
                    // Checks if list is empty
                    operationCount = Helpers.TotalOperations();
                    if (operationCount == 0)
                    {
                        Console.WriteLine("There are no past operations to view.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadLine();
                    }
                    else
                    {
                        // Give user option to delete history
                        Helpers.PrintLog();
                        Console.WriteLine("Select from the list below: ");
                        Console.WriteLine("1 - Delete History"); 
                        Console.WriteLine("2 - Continue");
                        numInput1 = Console.ReadLine();

                        if (numInput1 == "1")
                        {
                            Helpers.ClearLogs();
                            Console.WriteLine("History cleared.");
                        }
                        else if (numInput1 == "2")
                        {
                            continue;
                        }
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadLine();
                    }
                    Console.Clear();
                    break;
                default:
                    break;
            }

            // Ask the user to type the first number or view history
            Console.Clear();
            Console.WriteLine("Press H to view history.\n");
            double cleanNum1 = Helpers.InputNumber(numInput1);
            Console.WriteLine($"\nFirst Number: {cleanNum1}\n");

            // Ask the user to type the second number or view history
            double cleanNum2 = Helpers.InputNumber(numInput2);
            Console.WriteLine($"\nSecond Number: {cleanNum2}\n");


            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power Of");
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
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            operationCount = Helpers.TotalOperations();
            Console.WriteLine($"Current operation count: {operationCount}\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}