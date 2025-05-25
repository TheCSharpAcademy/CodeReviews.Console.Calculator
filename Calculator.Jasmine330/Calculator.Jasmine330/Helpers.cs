using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace Calculator.Jasmine330
{
    internal class Helpers
    {
        internal static double ValidateInput(string? input)
        {
            double cleanNum;
            while (!double.TryParse(input, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }
            return cleanNum;
        }

        internal static bool IsValidOperator(string? op)
        {
            return op != null && Regex.IsMatch(op, "^(a|s|m|d|sqrt|pow|10x|trig)$");
        }

        internal static void ValidateResult(double result)
        {
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in mathematical error.\n");
            }
            else Console.WriteLine("Your result: {0:0.##}\n", result);
        }

        internal static double[] GetInputs(string? op) 
        {
            double[] numbers = new double[2];
            switch (op)
            {
                case "a":
                case "s":
                case "m":
                case "d":
                case "pow":
                    Console.Write("Type a number, and then press Enter: ");
                    numbers[0] = ValidateInput(Console.ReadLine());

                    Console.Write("Type another number and then press Enter: ");
                    numbers[1] = ValidateInput(Console.ReadLine());
                    break;

                case "sqrt":
                case "10x":
                case "trig":
                    Console.Write("Type a number and then press Enter: ");
                    numbers[0] = ValidateInput(Console.ReadLine());
                    numbers[1] = 0;
                    break;

                default:
                    break;
            }

            return numbers;
        }

        internal static void DisplayResults(List<double> answers)
        {
           foreach( double ans in answers)
            {
                Console.WriteLine("{0:0.##}", ans);
            }

            DeleteHistory(answers);
        }

        private static void DeleteHistory(List<double> answers)
        {
            Console.Write("Would you like to delete the history? (y/n)");
            string? deleteHistory = Console.ReadLine();
            if (deleteHistory == "y")
            {
                answers.Clear();
                Console.WriteLine("History deleted.");
            }
        }
    }
}
