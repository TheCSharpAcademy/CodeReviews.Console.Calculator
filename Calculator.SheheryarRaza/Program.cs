using System.Text.RegularExpressions;
using CalculatorLibrary;
using System;
using System.Linq; // Added for LINQ operations

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
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter (or 'h' for history): ");
                numInput1 = Console.ReadLine();

                if (numInput1?.ToLower() == "h")
                {
                    DisplayHistory(calculator);
                    continue; // Skip current calculation and re-prompt
                }

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number (only for binary operations).
                // Or type the single number for unary operations.
                Console.Write("Type another number, and then press Enter (for power, or leave blank for unary ops): ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                // We'll allow numInput2 to be empty for unary operations
                if (!string.IsNullOrWhiteSpace(numInput2))
                {
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }
                }


                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tq - Square Root (uses first number)");
                Console.WriteLine("\tp - Power (first number to the power of second)");
                Console.WriteLine("\tt - 10x (uses first number)");
                Console.WriteLine("\tsin - Sine (uses first number in degrees)");
                Console.WriteLine("\tcos - Cosine (uses first number in degrees)");
                Console.WriteLine("\ttan - Tangent (uses first number in degrees)");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|q|p|t|sin|cos|tan]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        if (op == "q" || op == "t" || op == "sin" || op == "cos" || op == "tan")
                        {
                            result = calculator.DoOperation(cleanNum1, 0, op); // Pass 0 for num2 for unary ops
                        }
                        else
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        }


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

                // Display usage count
                Console.WriteLine($"Calculator has been used {calculator.GetUsageCount()} times.");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, 'c' to clear history, or press any other key and Enter to continue: ");
                string? exitOption = Console.ReadLine()?.ToLower();
                if (exitOption == "n")
                {
                    endApp = true;
                }
                else if (exitOption == "c")
                {
                    calculator.ClearCalculationHistory();
                    Console.WriteLine("Calculation history cleared.");
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        static void DisplayHistory(Calculator calculator)
        {
            List<CalculationEntry> history = calculator.GetCalculationHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("No calculation history available.");
                return;
            }

            Console.WriteLine("\n--- Calculation History ---");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {history[i]}");
            }
            Console.WriteLine("---------------------------\n");

            Console.Write("Enter the number of a calculation to use its result, or press Enter to return to main calculator: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= history.Count)
            {
                // Set the result of the chosen history entry as the first operand for the next calculation
                CalculationEntry selectedEntry = history[choice - 1];
                Console.WriteLine($"Using result {selectedEntry.Result} as the first number.");
                Console.WriteLine("------------------------\n");

                // Now, prompt for the second number and operation as if starting a new calculation
                // but pre-filling the first number with the selected history result.
                HandleNewCalculationWithHistoryResult(calculator, selectedEntry.Result);
            }
        }

        static void HandleNewCalculationWithHistoryResult(Calculator calculator, double initialNum)
        {
            double cleanNum1 = initialNum;
            string? numInput2 = "";
            double result = 0;

            Console.Write("Type another number, and then press Enter (for power, or leave blank for unary ops): ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            if (!string.IsNullOrWhiteSpace(numInput2))
            {
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tq - Square Root (uses first number)");
            Console.WriteLine("\tp - Power (first number to the power of second)");
            Console.WriteLine("\tt - 10x (uses first number)");
            Console.WriteLine("\tsin - Sine (uses first number in degrees)");
            Console.WriteLine("\tcos - Cosine (uses first number in degrees)");
            Console.WriteLine("\ttan - Tangent (uses first number in degrees)");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|q|p|t|sin|cos|tan]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    if (op == "q" || op == "t" || op == "sin" || op == "cos" || op == "tan")
                    {
                        result = calculator.DoOperation(cleanNum1, 0, op);
                    }
                    else
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    }

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
        }
    }
}