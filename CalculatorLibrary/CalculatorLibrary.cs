//using System.Diagnostics;
namespace CalculatorLibrary
{
    public class Calculator
    {
        private static int CalculatorUsedCount;
        private static List<(double num1, double num2, string op, double result)> CalculationHistory = new();
        public Calculator()
        {
        }
        public double DoOperation(double num1, double num2, string op)
        {

            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;
                case "s":
                    result = num1 - num2;
                    break;
                case "m":
                    result = num1 * num2;
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 == 0)
                    {
                        throw new DivideByZeroException("You can't divide by zero!");
                    }

                    result = num1 / num2;
                    break;
                case "r":
                    if (num1 < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(num1), num1, "You can't sqrt negative number! (i mean... you can but not with this basic calculator)");
                    }
                    num2 = double.NaN;
                    result = Math.Sqrt(num1);
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    break;
                case "t":
                    num2 = double.NaN;
                    result = num1 * 10;
                    break;
                case "sin":
                    num2 = double.NaN;
                    result = Math.Sin(num1);
                    break;
                case "cos":
                    num2 = double.NaN;
                    result = Math.Cos(num1);
                    break;
                case "tan":
                    num2 = double.NaN;
                    result = Math.Tan(num1);
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            if (!double.IsNaN(result))
            {
                CalculationHistory.Add((num1, num2, op, result));
            }
            CalculatorUsedCount++;
            return result;
        }

        public void ClearHistory()
        {
            CalculationHistory.Clear();
        }

        public double GetResultFromHistory(int index)
        {
            double result = double.NaN;

            if (index >= 0 && index < CalculationHistory.Count)
            {
                result = CalculationHistory[index].result;
            }

            return result;
        }

        public bool HasHistory()
        {
            return CalculationHistory.Count > 0;
        }

        public void DisplayHistory()
        {
            Console.WriteLine($"Calculator used: {CalculatorUsedCount}");
            for (int i = 0; i < CalculationHistory.Count; i++)
            {
                (double num1, double num2, string op, double result) calculation = CalculationHistory[i];
                Console.WriteLine($"index {i}: {calculation.num1} {calculation.op} {(!double.IsNaN(calculation.num2) ? calculation.num2.ToString() : "")} equals {calculation.result}");
            }
        }

    }
}