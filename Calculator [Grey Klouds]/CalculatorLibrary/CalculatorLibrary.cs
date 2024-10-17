using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

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
        public double DoOperation(double num1, double num2, string op, int usageCount)
        {
            List<double> history = new List<double>();  
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
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
                    history.Add(result);
                    writer.WriteValue("Add");
                    usageCount++;
                    break;
                case "s":
                    result = num1 - num2;
                    history.Add(result);
                    writer.WriteValue("Subtraction");
                    usageCount++;
                    break;
                case "m":
                    result = num1 * num2;
                    history.Add(result);
                    writer.WriteValue("Multiply");
                    usageCount++;
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        history.Add(result);
                        writer.WriteValue("Divide");
                    }
                    usageCount++;
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

        public List<double> getHistory(List<double> list)
        {
            Console.WriteLine("\t\t\t Calculation history:");
            Console.WriteLine("\t\t\t-----------------");

            foreach (double item in list)
            {
                Console.WriteLine(item);
            }
            return list;
        }

        public void ShowUses(int count)
        {
            Console.WriteLine($"You have used this application {count} times.");
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
