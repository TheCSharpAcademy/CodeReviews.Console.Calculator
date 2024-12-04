using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Input must be a number. Try again and enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                Console.Write("Type second number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Input must be a number. Try again and enter a numeric value: ");
                    numInput2 = Console.ReadLine();
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
                    Console.WriteLine("Error: Input is not valid.");
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
                        else Console.WriteLine("Result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error - Something went wrong.\n - Details: " + e.Message);
                    }
                } 
                Console.WriteLine("------------------------\n");
                Console.Write("Type n and press enter if you want to Close the app. Press any key to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }
            calculator.Finish();
            return;
        }
    }
}