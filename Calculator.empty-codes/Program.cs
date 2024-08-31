using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int usageCount = 0;
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
                double cleanNum1 = 0;
                double cleanNum2 = 0;

                Console.WriteLine("Select operation type using the options below:");
                Console.WriteLine("\ta - Addition");
                Console.WriteLine("\ts - Subtraction");
                Console.WriteLine("\tm - Multiplication");
                Console.WriteLine("\td - Division");
                Console.WriteLine("\tsq - Square Root");
                Console.WriteLine("\tp - Taking the Power");
                Console.WriteLine("\tt - Raising 10 to the Power of x");
                Console.WriteLine("\tsin - Sine");
                Console.WriteLine("\tcos - Cosine");
                Console.WriteLine("\ttan - Tangent");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sq|p|t|sin|cos|tan]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    if (op == "a" || op == "s" || op == "m" || op == "d" || op == "p") GetTwoOperands();
                    if (op == "sq" || op == "t" || op == "sin" || op == "cos" || op == "tan") GetOneOperand();

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

                void GetOneOperand()
                {
                    if (op == "sq")
                    {
                        Console.WriteLine("√x");
                    }
                    if (op == "t")
                    {
                        Console.WriteLine("10 \r\nx\r\n ");
                    }
                    if (op == "sin" || op == "cos" || op == "tan")
                    {
                        Console.WriteLine("sin(x) or cos(x) or tan(x) where x is an angle in degrees.");
                    }
                    // Ask the user to type the first number.
                    Console.Write("Type a number x, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }

                void GetTwoOperands()
                {
                    if (op == "p")
                    {
                        Console.Write("This number will be the base.");
                    }
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }

                    if (op == "p")
                    {
                        Console.Write("This number will be the exponent.");
                    }
                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                }

                usageCount++;
                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish(usageCount);
            return;
        }
    }
}