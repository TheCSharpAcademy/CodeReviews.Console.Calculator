using CalculatorLibrary;

using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("     Console Calculator in C#");
                Console.WriteLine("==================================");
                Console.WriteLine();
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine();
                Console.WriteLine("  a  - Add                d  - Divide             tan - Tangent              w  - 10x");
                Console.WriteLine("  s  - Subtract           p  - Power              sin - Sine                 u  - UsageCount");
                Console.WriteLine("  m  - Multiply           r  - Square Root        cos - Cosine               l  - LatestRusult");
                Console.WriteLine();
                Console.Write("Your option? ");

                string? op = Console.ReadLine().ToLower();

                
                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|r|tan|cos|sin|w|u|l|c]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                    continue;
                }

                if (op == "u")
                {
                    Console.WriteLine($"Calculator usage count is {usageCount} times.");
                }
                
                else if (op == "l" )
                {
                    calculator.DoOperation(0, 0, "l", ref usageCount);
                }
                else if (op == "c")
                {
                    calculator.DoOperation(0, 0, "c", ref usageCount);
                }

                else 
                {// Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    double cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }

                    // Ask the user to type the second number.
                    if (op != "r" && op != "w" && op != "sin" && op != "cos" && op != "tan")
                    {
                        Console.Write("Type another number, and then press Enter: ");

                        numInput2 = Console.ReadLine();

                        double cleanNum2 = 0;
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter an integer value: ");
                            numInput2 = Console.ReadLine();
                        }                   
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2 , op, ref usageCount);
                            if (double.IsNaN(result) && op != "u" && op !="l")
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }

                            else if (op != "u" && op != "l")
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    else
                    {
                        // For operations like 'r', 'w', 'sin', 'cos', 'tan'
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, 0, op, ref usageCount); // Passing 0 for unused operand
                            if (double.IsNaN(result) && op != "u" && op != "l")
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else if (op != "u" && op != "l")
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true ;

                Console.Clear();                
            }
            calculator.Finish();
            return;
        }
    }
}
    