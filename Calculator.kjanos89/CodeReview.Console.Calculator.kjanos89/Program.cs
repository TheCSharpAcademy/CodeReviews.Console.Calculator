using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            Console.WriteLine("Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                string? input1 = "";
                string? input2 = "";
                double result = 0;
                Console.Write("Type a number, and then press Enter: ");
                input1 = Console.ReadLine();

                double num1 = 0;
                while (!double.TryParse(input1, out num1))
                {
                    Console.Write("Invalid input. Please enter an numeric value: ");
                    input1 = Console.ReadLine();
                }
                Console.Write("Type another number, and then press Enter: ");
                input2 = Console.ReadLine();

                double num2 = 0;
                while (!double.TryParse(input2, out num2))
                {
                    Console.Write("Invalid input. Please enter an numeric value: ");
                    input2 = Console.ReadLine();
                }
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");

                string? operation = Console.ReadLine();
                if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Wrong input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(num1, num2, operation);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");
                Console.Write("Press 'n' and Enter to quit the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("\n"); 
            }
            return;
        }
    }
}