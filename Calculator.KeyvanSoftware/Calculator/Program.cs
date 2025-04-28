using System.Text.RegularExpressions;
using CalculatorLibrary;
using static CalculatorLibrary.Helpers;

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
            // Ask the user to choose an operator.
            string? op = GetOperator();

            // Declare variables and set to empty.
            double result = 0;
            double cleanNum2 = 0;

            // Ask the user to type the first number.
            double cleanNum1 = GetNumber("Type the first number, then press Enter: ");

            // Ask the user to type the second number.
            if (Helpers.BinaryOperatorLetters.Contains(op))
                cleanNum2 = GetNumber("Type the second number, then press Enter: ");

            // Validate input is not null, and matches the pattern
            try
            {
                result = calculator.PerformOperationWithCheck(cleanNum1, cleanNum2, op);
                if (GetYesOrNo("Would you like to remove the last calculation?"))
                    calculator.RemoveLastCalculation();

                if (GetYesOrNo("Would you like to use previous calculations?"))
                {
                    if (calculator.PreviousCalculations.Count == 0)
                    {
                        Console.WriteLine("No previous calculations to show.");
                    }
                    else
                    {
                        Console.WriteLine("\nPrevious Calculations:");
                        calculator.DisplayCalculations();

                        ReusePreviousCalculation(calculator);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}