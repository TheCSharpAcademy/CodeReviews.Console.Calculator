using System.Diagnostics;
namespace CalculatorLibrary
{
    public class Calculator
    {
        private static int CalculatorUsedCount = 0;
        private static List<(double num1, double num2, string op, double result)> CalculationHistory = new();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("Calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator log");
            Trace.WriteLine(String.Format("Started {0}", DateTime.Now.ToString()));
        }
        public double DoOperation(double num1, double num2, string op)
        {

            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    break;
                case "s":
                    result = num1 - num2;
                    Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                    break;
                case "m":
                    result = num1 * num2;
                    Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 == 0)
                    {
                        throw new DivideByZeroException("You can't divide by zero!");
                    }

                    result = num1 / num2;
                    Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                    break;
                case "r":
                    if (num1 < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(num1), num1, "You can't sqrt negative number! (i mean... you can but not with this basic calculator)");
                    }
                    num2 = double.NaN;
                    result = Math.Sqrt(num1);
                    Trace.WriteLine(String.Format("SQRT{0} = {1}", num1, result));
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    Trace.WriteLine(String.Format("{0} Raised to the power of {1} = {2}", num1, num2, result));
                    break;
                case "t":
                    num2 = double.NaN;
                    result = num1 * 10;
                    Trace.WriteLine(String.Format("{0} x10 = {1}", num1, result));
                    break;
                case "sin":
                    num2 = double.NaN;
                    result = Math.Sin(num1);
                    Trace.WriteLine(String.Format("{0} Sin = {1}", num1, result));
                    break;
                case "cos":
                    num2 = double.NaN;
                    result = Math.Cos(num1);
                    Trace.WriteLine(String.Format("{0} Cos = {1}", num1, result));
                    break;
                case "tan":
                    num2 = double.NaN;
                    result = Math.Tan(num1);
                    Trace.WriteLine(String.Format("{0} Tan = {1}", num1, result));
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            if (!double.IsNaN(result))
            {
                CalculationHistory.Add((num1, num2, op, result));
            }
            Trace.Write($"Calc used no. {++CalculatorUsedCount}");
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
            for (int i = 0; i < CalculationHistory.Count; i++)
            {
                (double num1, double num2, string op, double result) calculation = CalculationHistory[i];
                Console.WriteLine($"index {i}: {calculation.num1} {calculation.op} {(!double.IsNaN(calculation.num2) ? calculation.num2.ToString() : "")} equals {calculation.result}");
            }
        }

    }
}