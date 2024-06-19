using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int numCalculations = 0;
        // Intitial calculator title
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare intial empty math variables
            // Using Nullable types to match type of System.Console.Readline
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            Console.Clear();
            // Prompt user to choose math operator
            string op = calculator.GetMathOperator();
            double cleanNum1;

            if (op == "v")
            {
                cleanNum1 = calculator.GetPreviousResults(); // Assign cleanNum1 based on previous calculation selected
                if (double.IsNaN(cleanNum1)) // If they didn't select previous calculation, continue back to top of while loop
                    continue;
                op = calculator.GetMathOperator(); // If they did select a previous calc, get the math operator to use
            }
            else
            {
                // Ask user for first math number
                Console.WriteLine("Type a number, then press Enter: ");
                numInput1 = Console.ReadLine();

                // Prompt for input until a number is input, save it in cleanNum1
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
            }

            double cleanNum2 = 0;

            // Only ask user for second number if operation requires one
            if (!Regex.IsMatch(op, calculator.pattern)) // \b matches a word boundary, ensuring the pattern matches "sr" as a whole word
            {
                Console.WriteLine("Type second number, then press Enter: ");
                numInput2 = Console.ReadLine();

                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }
            }


            try
            {
                // Display error if math operation is invalid, else display the result
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result)) Console.WriteLine("This operation will result in a mathematical error.\n");
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result); // 0 = mandatory place, # = optional place
                    numCalculations++;
                }
            }
            catch (Exception e)
            {
                // Catch any other error that wasn't caught
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
            Console.WriteLine($"You've performed {numCalculations} successful calculations");
            Console.WriteLine("------------------------\n");

            // Wait for user response before closing
            Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Spacing for better styling
        }
        // Close JSON writer before return
        calculator.Finish();
        return;
    }
}