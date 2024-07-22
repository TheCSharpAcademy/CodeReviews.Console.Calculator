using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            int timesUsed = 0;

            while (!endApp)
            {
                
                double firstNum = calculator.getInput(timesUsed);
                double secondNum = calculator.getInput(timesUsed);
                double result = 0;
  
                    // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\te - Exponent");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();
                timesUsed++;

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|e]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(firstNum, secondNum, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                        calculator.calculations.Add(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                    Console.WriteLine("------------------------\n");

                    // Wait for the user to respond before closing.
                    Console.Write("Press 'n' and Enter to close the app, or 'v' and Enter to view prior calculations; press any other key and Enter to continue: ");
                switch (Console.ReadLine())
                {
                    case "n":
                        endApp = true;
                        Console.WriteLine($"Calculator was used {timesUsed} times");
                        break;
                    case "v":
                        calculator.PrintCalculations();
                        Console.WriteLine("Press 'c' to clear the results and end the game, or any other key to continue the game.");

                        if (Console.ReadLine() == "c")
                        {
                            calculator.calculations.Clear();
                            endApp = true;
                        }
                        break;
                }
            }
            calculator.Finish();
            return;
        }
    }
}