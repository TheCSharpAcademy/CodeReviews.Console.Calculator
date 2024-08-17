using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calc = new Calculator();
        while (!endApp)
        {
            Console.Clear();
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            double cleanNum1;
            double cleanNum2;
            double result;

            string choice = Helpers.GetChoice();

            double res = Helpers.StartProgram(choice);

            if (res == 0)
            {
                cleanNum1 = Helpers.GetNumber();
            }
            else
            {
                cleanNum1 = res;
            }

            // Ask the user to type the second number.
            cleanNum2 = Helpers.GetNumber();

            // Ask the user to choose an operator.
            string op = Helpers.GetOperator();

            try
            {
                result = calc.DoOperation(cleanNum1, cleanNum2, op);
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

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            endApp = Helpers.IsCalculationComplete();

            Console.WriteLine("\n"); // Friendly linespacing;
        }
        calc.Finish();
        return;
    }
}

