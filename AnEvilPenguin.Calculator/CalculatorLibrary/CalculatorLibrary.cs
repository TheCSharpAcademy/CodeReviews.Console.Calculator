using Newtonsoft.Json;
using System.Diagnostics;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private List<Double> pastResults = new List<Double>();
        private JsonWriter writer;

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

        public void ClearHistory()
        {
            writer.WriteStartObject();
            writer.WritePropertyName("ClearedHistory");
            writer.WriteStartArray();

            pastResults.ForEach(result => { writer.WriteValue(result); });

            writer.WriteEndArray();
            writer.WriteEndObject();

            pastResults.Clear();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
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
                    writer.WriteValue("Add");
                    break;

                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;

                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;

                case "d":

                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;

                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power of");
                    break;

                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;

                case "x":
                    result = num1 * 10;
                    writer.WriteValue("10x");
                    break;

                case "i":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;

                case "c":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;

                case "t":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    break;

                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            pastResults.Add(result);

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public List<Double> GetHistory() => new List<double>(pastResults);
    }
}