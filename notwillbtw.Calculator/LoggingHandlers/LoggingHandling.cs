using Newtonsoft.Json;

namespace LoggingHandlers
{
    public class LoggingHandling
    {
        internal JsonWriter writer;

        public void Start()
        {
            StreamWriter logFile = File.CreateText("PastCalculations.txt");
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

        public void LogCalculation(string op, double operand1, double operand2, double result)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(operand1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(operand2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    writer.WriteValue("Add");
                    break;
                case "s":
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    writer.WriteValue("Divide");
                    break;
                case "r":
                    writer.WriteValue("Square Root");
                    break;
                case "p":
                    writer.WriteValue("power");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
        }
    }
}
