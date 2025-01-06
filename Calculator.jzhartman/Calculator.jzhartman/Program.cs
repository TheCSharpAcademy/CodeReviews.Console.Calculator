using CalculatorLibrary;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CalculatorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                string? numInput1 = string.Empty;
                string? numInput2 = string.Empty;
                double result = 0;

                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("ERROR: Invalid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }


                Console.Write("Type another number, then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("ERROR: Invalid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Mulitply");
                Console.WriteLine("\td - Divide");

                Console.Write("\nYour selection: ");

                string? operation = Console.ReadLine();

                if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d]"))
                {
                    Console.WriteLine("ERROR: Invalid input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, operation);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation results in a mathematical error!");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Oh no! An exception occurred while trying to do the math!\n - Details: {e.Message}");

                    }
                }
                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue.");

                if (Console.ReadLine().ToLower() == "n") endApp = true;

                Console.WriteLine("\n");

            }

            calculator.Finish();
            return;
        }
    }
}