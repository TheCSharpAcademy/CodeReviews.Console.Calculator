using System.Text.RegularExpressions;
using CalculatorLibrary;

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

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                if (calculator.operations.Count > 0)
                {
                    Console.WriteLine($"Number of operations: {calculator.operations.Count}");
                    Console.WriteLine("Previous operations:");
                    for (int i = 0; i < calculator.operations.Count; i++)
                    {
                        Console.WriteLine($"{i}:\t {calculator.operations[i]}");
                    }
                    Console.Write("Press an index number from previous calculation, to use it in new calculation(Write n for none): ");
                    string? previousCalculation = Console.ReadLine();
                    int cleanPreviousCalculation;

                    while (!int.TryParse(previousCalculation, out cleanPreviousCalculation) || cleanPreviousCalculation < 0 || cleanPreviousCalculation >= calculator.operations.Count)
                    {
                        if (previousCalculation == "n")
                        {
                            cleanPreviousCalculation = -1;
                            break;
                        }
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        previousCalculation = Console.ReadLine();
                    }
                    if (cleanPreviousCalculation >= 0)
                    {
                        numInput1 = calculator.operations[cleanPreviousCalculation].ToString();
                    }

                }
                if (numInput1 == "")
                {
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                }

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
                Console.WriteLine("\tr - Square Root(Only 1st entered number used)");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\tt - 10x");
                Console.WriteLine("\tsin - Sin(Only 1st entered number used)");
                Console.WriteLine("\tcos - Cos(Only 1st entered number used)");
                Console.WriteLine("\ttan - Tan(Only 1st entered number used)");
                Console.WriteLine("\tdel - Delete history");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|t|sin|cos|tan|del]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        if (op == "r")
                        {
                            result = Math.Sqrt(cleanNum1);
                        }
                        else
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        }

                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            calculator.AddToList(result);
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                        }
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

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}
