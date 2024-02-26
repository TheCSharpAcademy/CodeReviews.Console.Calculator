using System;
using System.Linq;
using CalculatorLibrary;

namespace CalculatorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool endCalc = false; 
            Calculator calculator = new();
            while (!endCalc)
            {
                Console.WriteLine("Console Calculator");
                Console.WriteLine("---------------------------------------");

                Console.WriteLine("Enter your first number:");

                // como funcionaria esse try parse para validação de input?
                float n1, n2;
                while (!float.TryParse(Console.ReadLine(), out n1))
                {
                    Console.WriteLine("Enter a valid number");
                }
                Console.Clear();

                Console.WriteLine("Enter the second number:");
                while (!float.TryParse(Console.ReadLine(), out n2))
                {
                    Console.WriteLine("Enter a valid number");
                }

                Console.Clear();

                Console.WriteLine("Choose an operation:");
                Console.WriteLine("A - Add");
                Console.WriteLine("S - Subtract");
                Console.WriteLine("M - Multiply");
                Console.WriteLine("D - Divide");
                string operation = Console.ReadLine().ToLower().Trim();
                string[] operations = { "a", "s", "m", "d" };
                while (!operations.Contains(operation))
                {
                    Console.WriteLine("Enter a valid operation");
                    operation = Console.ReadLine().ToLower().Trim();
                }

                calculator.Calculate(n1, n2, operation);

                Console.WriteLine("Do you want another operation? (Y/N)");
                string again = Console.ReadLine().ToLower().Trim();
                while (again != "y" && again != "n")
                {
                    Console.WriteLine("Enter a valid input");
                    again = Console.ReadLine().ToLower().Trim();
                }
                endCalc = again != "y";
                Console.Clear();

            }
            calculator.Finish();
            return;
        }
    }
}
