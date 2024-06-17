using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<double> operations = new List<double>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
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
            writer.WriteStartObject();
            writer.WritePropertyName("Opreand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Opreand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operator");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Addition");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtraction");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiplication");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Division");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "t":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    break;
                case "del":
                    operations.Clear();
                    writer.WriteValue("Delete history");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void AddToList(double result)
        {
            operations.Add(result);
        }

        public List<double> GetOperations()
        {
            return operations;
        }
    }
}
