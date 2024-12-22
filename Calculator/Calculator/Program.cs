// Program.cs
using CalculatorLibrary;
using Data.Models;
using Helpers;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            int usageCount = 0;
            bool endApp = false;
            List<CalculationHistory> calculationHistory = new List<CalculationHistory>();
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                double numInput1 = 0;
                double numInput2 = 0;
                double result = 0;
                bool historyAvailable = (calculationHistory.Count > 0) ? true : false;

                // Ask the user to type the first number.
                numInput1 = Helper.GetInput(historyAvailable, calculationHistory);

                // Ask the user to type the second number.
                numInput2 = Helper.GetInput(historyAvailable, calculationHistory);

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - X to power of Y");
                Console.WriteLine("\tsr - Square Root -- Operation perfomed on first number only");
                Console.WriteLine("\tt - Tangent -- Operation perfomed on first number only");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|sr|t]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        if (op == "sr")
                        {
                            result = Math.Sqrt(numInput1);
                        }
                        else if (op == "t")
                        {
                            result = Math.Tan(numInput1);
                        }
                        else
                        {
                            result = calculator.DoOperation(numInput1, numInput2, op);

                        }

                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            usageCount++;
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            Console.WriteLine("Performed {0} calculations this session\n", usageCount);
                            Helper.AddToHistory(usageCount, numInput1, numInput2, op, result, calculationHistory);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, 'v' to view history, or press any other key and Enter to continue: ");
                string reply = Console.ReadLine();
                if (reply == "n")
                {
                    endApp = true;
                }
                else if (reply == "v")
                {
                    Helper.ViewHistory(calculationHistory);
                }

                //Console.WriteLine("\n"); // Friendly linespacing.
                Console.Clear();
            }
            calculator.Finish();
            return;
        }
    }
}