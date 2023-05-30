using System;
using System.Threading;

namespace MeksCalculator
{
    class Calculator
    {
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;

                case "b":
                    result = num1 - num2;
                    break;

                case "c":
                    result = num1 * num2;
                    break;

                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    break;

                default:
                    break;
            };
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            Console.WriteLine("Mek's Calculator\r");
            Console.WriteLine("----------------------------");

            while (!endApp)
            {
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                Console.Write("Enter First Number and Press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not a valid input. Please eneter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                Console.Write("Enter Second Number and Press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse((string)numInput2, out cleanNum2))
                {
                    Console.Write("This is not a valid input. Please Enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\tb - Subtract");
                Console.WriteLine("\tc - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your Option? ");

                string op = Console.ReadLine();

                try
                {
                    result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your Result: {0:0.##}\n", result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh No! An exception occured trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("---------------------------");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n")
                {
                    endApp = true;
                }
                Console.WriteLine("\n");
            }
            return;
        }
    }
}