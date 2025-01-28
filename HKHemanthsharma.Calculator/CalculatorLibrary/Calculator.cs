using System.Formats.Asn1;
using System.Xml;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public static int usedCount = 0;
        JsonWriter writer;

        public Calculator()
        {

            StreamWriter logFile = File.CreateText("calculatorlog.json");
            /*Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
            */
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Newtonsoft.Json.Formatting.Indented;
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
        public double DoOperation(double num1, double num2, string op)
        {
            usedCount++;
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

            // Use a switch statement to do the math.

            writer.WriteStartObject();
            writer.WritePropertyName("operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("operation");
            switch (op)
            {
                case "a":
                    writer.WriteValue("Add");
                    result = num1 + num2;
                    break;
                case "s":
                    writer.WriteValue("subtract");
                    result = num1 - num2;
                    break;
                case "m":
                    writer.WriteValue("multiply");
                    result = num1 * num2;
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.

                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:

                    break;
            }
            writer.WritePropertyName("result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }

    }
}