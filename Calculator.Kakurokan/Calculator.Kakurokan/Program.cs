using System;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new ();
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            while (!endApp)
            {

                Console.Write("Type a number, and then press Enter: ");
                string num1Input = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(num1Input, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    num1Input = Console.ReadLine();
                };

                Console.Write("Type another number, and then press Enter: ");
                string num2Input = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(num2Input, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    num2Input = Console.ReadLine();
                };

                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");
                string op = Console.ReadLine();

                try
                {
                    double result = calculator.Calculate(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine($"Your result: {result:0.##}\n");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Oh no! An error occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("\n");

            }
            calculator.Finish();
            return;
        }
    }
}