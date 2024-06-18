using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        private int operationCount = 0;

        private List<string> history = new List<string>();

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
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            string symbol = "";

            operationCount++;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    symbol = "+";
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    symbol = "-";
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    symbol = "*";
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    symbol = "/";
                    writer.WriteValue("Divide");
                    break;
                case "sr":
                    result = Math.Sqrt(num1);
                    symbol = "√";
                    writer.WriteValue("SquareRoot");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    symbol = "^";
                    writer.WriteValue("Power");
                    break;
                case "p10":
                    result = Math.Pow(10, num1);
                    symbol = "10^";
                    writer.WriteValue("PowerOf10");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            if (op == "sr" || op == "p10")
                history.Add($"{symbol}{num1} = {result}");
            else
                history.Add($"{num1} {symbol} {num2} = {result}");

            return result;
        }

        public void ClearHistory()
        {
            history.Clear();
        }

        public List<string> GetHistory()
        {
            return history;
        }

        public void Finish()
        {
            writer.WriteEndArray();

            writer.WritePropertyName("OperationCount");
            writer.WriteValue(operationCount);

            writer.WriteEndObject();
            writer.Close();
        }
    }
}