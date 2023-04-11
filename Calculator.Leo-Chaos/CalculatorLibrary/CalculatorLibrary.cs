

namespace CalculatorLibrary
{
    public class Calculator
    {
        
        public static double DoOperation(double num1, double? num2, string op)
        {
            double result = double.NaN;

            switch (op)
            {
                case "a":
                    result = num1 + num2.Value;
                    break;
                case "s":
                    result = num1 - num2.Value;
                    break;
                case "m":
                    result = num1 * num2.Value;
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2.Value;
                    }
                    break;
                case "q":
                    result = Math.Sqrt(num1);
                    break;
                case "p":
                    result = Math.Pow(num1, num2.Value);
                    break;
                case "x":
                    result = num1 * 10;
                    break;
                case "i":
                    result = Math.Sin(num1);
                    break;
                case "c":
                    result = Math.Cos(num1);
                    break;
                case "t":
                    result = Math.Tan(num1);
                    break;
                case "h":
                    HistoryFunctions.HistoryMenu();
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}