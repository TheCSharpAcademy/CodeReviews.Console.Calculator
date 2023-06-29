using Calculator.Models;
using System.Diagnostics.Metrics;
namespace Calculator
{
    internal class Helpers
    {
        internal static List<Calculations> calcs = new List<Calculations>();
        internal static void AddToHistory(double num1, double num2, string operation, double result, int counter)
        {
            calcs.Add(new Calculations
            {
                Num1 = num1,
                Num2 = num2,
                Operation = operation,
                Result = result,
                Count = counter
            });
        }

        internal static void DisplayHistory()
        {
            Console.WriteLine("To use the result, type the corresponding number to use or press c to clear:");
            foreach (var calc in calcs)
            {
                if (calc.Operation == "sqrt")
                {
                    Console.WriteLine($"{calc.Count} - {calc.Result} Equation: {calc.Operation} {calc.Num1} = {calc.Result}");
                }
                else Console.WriteLine($"{calc.Count} - {calc.Result} Equation: {calc.Num1} {calc.Operation} {calc.Num2} = {calc.Result}");
            }
        }

        internal static string GetPastResult()
        {
            string answer = Console.ReadLine();
            if (answer == "c")
            {
                ClearHistory();
                return answer;
            }
            double result = calcs[Int32.Parse(answer) - 1].Result;
            answer = result.ToString();
            return answer;
        }
        internal static void ClearHistory()
        {
            calcs.Clear();
        }
    }

}
