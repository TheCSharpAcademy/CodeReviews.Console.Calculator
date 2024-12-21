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

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();
            
            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
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

            // Wait for the user to respond before closing.
            Console.WriteLine("Press 'n' to Enter to close the app" +
                          "\n'l' to view latest calculations" +
                          "\n'd' to delete the list" +
                          "\n't' to see how many times you used the calculator" +
                          "\nor press any other key and Enter to continue:");

            switch (Console.ReadLine())
            {
                case "n":
                    Console.WriteLine($"How many times you used the calculator: {UseCounter.counter}");
                    Thread.Sleep(1000);
                    endApp = true;
                    break;
                case "l":
                    Console.WriteLine("List of latest calculations:");
                    foreach (var calculation in LatestCalculations.latestCalculations)
                    {
                        Console.WriteLine(calculation);
                    }
                    break;
                case "d":
                    LatestCalculations.latestCalculations.Clear();
                    break;
                case "t":
                    Console.WriteLine(UseCounter.counter);
                    break;
            }
            Console.WriteLine("\n"); // Friendly linespacing.
        }

        calculator.Finish();
        return;
    }
}