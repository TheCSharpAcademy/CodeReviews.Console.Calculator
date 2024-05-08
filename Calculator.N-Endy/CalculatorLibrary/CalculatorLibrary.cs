using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{

    public class Calculator
    {
        readonly JsonWriter writer;
        public Calculator()
        {
            // StreamWriter logFile = File.CreateText("calculator.log");
            // Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            // Trace.AutoFlush = true;
            // Trace.WriteLine("Starting Calculator Log");
            // Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));

            StreamWriter logFile = File.CreateText("calculator.json");
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

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    //Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    //Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    //Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        //Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                        writer.WriteValue("Divide");
                    }
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndArray();

            return result;
        }
    }
}
