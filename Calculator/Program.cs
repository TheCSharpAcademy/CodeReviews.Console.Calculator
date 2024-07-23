using System.Text.RegularExpressions;
using CalculatorLib;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            WriteLine("Console Calculator in C#\r");
            WriteLine("------------------------\n");

            Calculator calculator = new();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Write("Type a number, and then press Enter: ");
                numInput1 = ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = ReadLine();
                }

                // Ask the user to type the second number.
                Write("Type another number, and then press Enter: ");
                numInput2 = ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = ReadLine();
                }

                // Ask the user to choose an operator.
                WriteLine("Choose an operator from the following list:");
                WriteLine("\ta - Add");
                WriteLine("\ts - Subtract");
                WriteLine("\tm - Multiply");
                WriteLine("\td - Divide");
                Write("Your option? ");

                string? op = ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (ReadLine() == "n") endApp = true;

                WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }
    }
}
