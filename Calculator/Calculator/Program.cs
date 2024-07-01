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
            int numberOfCalcs = 0;
            List<string> calcsPerformed = new List<string>();

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                string? opSymbol = "";

                // Ask the user to type the first number.
                Console.WriteLine("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.WriteLine("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second numer.
                Console.WriteLine("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.WriteLine("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - Power");
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
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            numberOfCalcs++;
                            switch (op)
                            {
                                case "a":
                                    opSymbol = "+";
                                    break;
                                case "s":
                                    opSymbol = "-";
                                    break;
                                case "m":
                                    opSymbol = "*";
                                    break;
                                case "d":
                                    opSymbol = "/";
                                    break;
                                case "p":
                                    opSymbol = "^";
                                    break;
                                default:
                                    opSymbol = "Error: Unrecognized input.";
                                    break;
                            }
                            calcsPerformed.Add($"{cleanNum1} {opSymbol} {cleanNum2} = {result}");
                            Console.WriteLine($"Calculations Performed ({numberOfCalcs}):");
                            foreach (string calc in calcsPerformed)
                            {
                                Console.WriteLine(calc);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("-------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Options:\n");
                Console.WriteLine("1) Continue");
                Console.WriteLine("2) Delete your history of Calculations Performed and continue"); 
                Console.WriteLine("3) Close the app");
                string? input;
                do
                {
                    Console.Write("Enter 1, 2, or 3: ");
                    input = Console.ReadLine();
                    if (input == "1")
                    {
                        continue;
                    }
                    else if (input == "2")
                    {
                        calcsPerformed.Clear();
                        numberOfCalcs = 0;
                    }
                    else if (input == "3")
                    {
                        endApp = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                } while (input == null || !Regex.IsMatch(input, "[1|2|3|]"));

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }
    }
}