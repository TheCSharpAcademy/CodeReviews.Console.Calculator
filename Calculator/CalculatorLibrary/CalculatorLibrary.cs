// CalculatorLibrary.cs
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
        public double SquareRoot(double a)
        {
            if (a < 0) throw new ArgumentException("Cannot calculate square root of a negative number.");
            return Math.Sqrt(a);
        }
        public double Power(double baseValue, double exponent) => Math.Pow(baseValue, exponent);
        public double TenToThePower(double a) => Math.Pow(10, a);
        public double TrigonometricFunction(string function, double angle)
        {
            if (function == null)
            {
                throw new ArgumentNullException("Null Function Argument passed");
            }
            else
            {
                switch (function.ToLower())
                {

                    case "sin": return Math.Sin(angle);
                    case "cos": return Math.Cos(angle);
                    case "tan": return Math.Tan(angle);
                    default: throw new ArgumentException("Invalid trigonometric function.");


                }
            }
        }
        public static int NumberOfTimes { get; set; }

        public int GetNumberOfTimes()
        {
            return NumberOfTimes;
        }

        public void SetNumberOfTimes()
        {
            NumberOfTimes++;
        }


        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }


        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }


        public double DoOperation(string op, [Optional] double num1, [Optional] double num2, [Optional] string functionName, [Optional] double angle)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.


            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(Convert.ToString(num1));
            writer.WritePropertyName("Operand2");
            writer.WriteValue(Convert.ToString(num2));
            writer.WritePropertyName("Operand3");
            writer.WriteValue(functionName);
            writer.WritePropertyName("Operand4");
            writer.WriteValue(Convert.ToString(angle));
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = Add(num1, num2);
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = Subtract(num1, num2);
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = Multiply(num1, num2);
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    result = Divide(num1, num2);
                    writer.WriteValue("Divide");
                    break;
                case "r":
                    result = SquareRoot(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "p":
                    result = Power(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "x":
                    result = TenToThePower(num1);
                    writer.WriteValue("Ten To The Power");
                    break;
                case "t":
                    result = TrigonometricFunction(functionName, angle);
                    writer.WriteValue("Trigonometric Fuction");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Number Of Times");
            writer.WriteValue(Calculator.NumberOfTimes);
            writer.WriteEndObject();


            return result;
        }
    }

    public class CalculationHistory
    {
        private List<string> history = new List<string>();

        public void AddToHistory(string calculation) => history.Add(calculation);

        public List<string> GetHistory() => new List<string>(history);

        public void ClearHistory() => history.Clear();

        public double ReuseResult(int index)
        {
            if (index < 0 || index >= history.Count)
                throw new IndexOutOfRangeException("Invalid history index.");

            // Extract the result from the string, assuming the format is "operation = result"
            string[] parts = history[index].Split('=');
            if (parts.Length < 2)
                throw new FormatException("Invalid calculation format in history.");

            // Parse and return the result
            return double.Parse(parts[1].Trim());
        }
    }
}
