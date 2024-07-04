using CalculatorLibrary;
using System;
using System.Text.RegularExpressions;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            CalculatorHandler calculatorHandler = new CalculatorHandler();

            while (!endApp)
            {
                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\troot - Square root, x^0.5");
                Console.WriteLine("\tpow - Exponentiation, x^y");
                Console.WriteLine("\tpow10 - Raise 10 to the power x, 10^x");
                Console.WriteLine("\tsin - sin(x), x as DEG");
                Console.WriteLine("\tcos - cos(x), x as DEG");
                Console.WriteLine("\ttan - tan(x), x as DEG");
                Console.WriteLine("\tcot - cot(x), x as DEG");
                Console.Write("Your option? ");

                string? menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "c":
                        calculatorHandler.ClearLatestCalculations();
                        break;
                    case not null when Regex.IsMatch(menuOption, "^([asmd]|pow)$"): // Validate input is not null, and matches the pattern
                        try
                        {
                            double operandLeft = calculatorHandler.InputOperandHandler();
                            double operandRight = calculatorHandler.InputOperandHandler();
                            double result = calculatorHandler.DoOperation(operandLeft, operandRight, menuOption);
                            DisplayResult(result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                        break;
                    case not null when Regex.IsMatch(menuOption, "^(root|pow10|sin|cos|tan|cot)$"):
                        try
                        {
                            double operandLeft = calculatorHandler.InputOperandHandler();
                            double result = calculatorHandler.DoMathFunction(operandLeft, menuOption);
                            DisplayResult(result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Error: Unrecognized input.");
                        break;
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }

            calculatorHandler.Finish();
            return;
        }

        public static void DisplayResult(double num)
        {
            if (double.IsNaN(num))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", num);
            }
        }
    }
}
