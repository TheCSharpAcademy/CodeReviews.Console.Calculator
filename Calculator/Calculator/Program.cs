using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tc - Perform a calculation");
                Console.WriteLine("\tv - View calculation history");
                Console.WriteLine("\tl - Clear calculation history");
                Console.WriteLine("\tu - Use result from history");
                Console.WriteLine("\tq - Quit the application");
                Console.Write("Your option? ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "c":
                        PerformCalculation(calculator);
                        break;
                    case "v":
                        ViewCalculationHistory(calculator);
                        break;
                    case "l":
                        ClearCalculationHistory(calculator);
                        break;
                    case "u":
                        UseResultFromHistory(calculator);
                        break;
                    case "q":
                        endApp = true;
                        break;
                    default:
                        Console.WriteLine("Error: Unrecognized input.");
                        break;
                }

                Console.WriteLine("------------------------\n");

                // Friendly linespacing.
                Console.WriteLine("\n");
            }

            calculator.Finish(); // Ensure the Finish method is called
            return;
        }

        static void PerformCalculation(Calculator calculator)
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tsqrt - Square Root");
            Console.WriteLine("\t10x - 10^x");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\ttan - Tangent");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|p|sqrt|10x|sin|cos|tan)$"))
            {
                Console.WriteLine("Error: Unrecognized input.");
                return;
            }

            if (op == "sqrt" || op == "10x" || op == "sin" || op == "cos" || op == "tan")
            {
                PerformSingleOperandCalculation(calculator, op);
            }
            else
            {
                PerformDoubleOperandCalculation(calculator, op);
            }
        }

        static void PerformDoubleOperandCalculation(Calculator calculator, string op)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

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

        static void PerformSingleOperandCalculation(Calculator calculator, string op)
        {
            string? numInput = "";
            double result = 0;

            // Ask the user to type the number.
            Console.Write("Type a number, and then press Enter: ");
            numInput = Console.ReadLine();

            double cleanNum = 0;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }

            try
            {
                result = calculator.DoSingleOperation(cleanNum, op);
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

        static void ViewCalculationHistory(Calculator calculator)
        {
            // View calculation history
            var history = calculator.GetCalculationHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("No calculations yet.");
            }
            else
            {
                Console.WriteLine("Calculation History:");
                foreach (var item in history)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static void ClearCalculationHistory(Calculator calculator)
        {
            // Clear calculation history
            calculator.ClearCalculationHistory();
            Console.WriteLine("Calculation history cleared.");
        }

        static void UseResultFromHistory(Calculator calculator)
        {
            var history = calculator.GetCalculationHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("No calculations yet.");
                return;
            }

            Console.WriteLine("Calculation History:");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {history[i]}");
            }

            Console.Write("Select a result number to use: ");
            string? selection = Console.ReadLine();
            int selectedIndex;
            if (int.TryParse(selection, out selectedIndex) && selectedIndex > 0 && selectedIndex <= history.Count)
            {
                string selectedCalculation = history[selectedIndex - 1];
                double selectedResult = ExtractResult(selectedCalculation);

                // Use the selected result for a new calculation
                Console.Write("Type another number, and then press Enter: ");
                string? numInput = Console.ReadLine();

                double cleanNum = 0;
                while (!double.TryParse(numInput, out cleanNum))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput = Console.ReadLine();
                }

                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    double result = 0;
                    try
                    {
                        result = calculator.DoOperation(selectedResult, cleanNum, op);
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
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        static double ExtractResult(string calculation)
        {
           
            var parts = calculation.Split('=');
            return double.Parse(parts[1].Trim());
        }
    }
}
