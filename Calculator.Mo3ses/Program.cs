using System;
using MathOperations = Calculator.Mo3ses.MathOperations;

namespace Calculator.Mo3ses
{
    public class Program
    {
        public static void Main(string[] args)
        {

            int answer = 0;
            bool verify = false;
            double value1 = 0;
            double value2 = 0;

            while (verify == false)
            {
                Menu.StartMenu();
                if (int.TryParse(Console.ReadLine(), out answer))
                {
                    verify = true;
                }
                else
                {
                    Console.WriteLine("Invalid Option, Try Again.");
                }
                if (verify)
                {
                    Console.WriteLine("Choose 1 Value:");
                    value1 = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Choose 2 Value:");
                    value2 = Convert.ToDouble(Console.ReadLine());
                    while (answer == 4 && value2 == 0)
                    {
                        Console.WriteLine("Enter a non-zero divisor: ");
                        value2 = Convert.ToDouble(Console.ReadLine());
                    }
                }
            }

            switch (answer)
            {
                case 1:
                    MathOperations.Sum(value1, value2);
                    break;
                case 2:
                    MathOperations.Subtract(value1, value2);
                    break;
                case 3:
                    MathOperations.Multiply(value1, value2);
                    break;
                case 4:
                    MathOperations.Divide(value1, value2);
                    break;
                case 9:
                    break;
                default:
                    Console.WriteLine("Invalid Option, Try Again.");
                    break;
            }
        }
    }
}