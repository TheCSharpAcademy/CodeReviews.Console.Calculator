using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class CalculatorBrain
    {
        JsonWriter jsonWriter;

        public CalculatorBrain()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            jsonWriter = new JsonTextWriter(logFile);
            jsonWriter.Formatting = Formatting.Indented;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Operations");
            jsonWriter.WriteStartArray();
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Operand1");
            jsonWriter.WriteValue(num1);
            jsonWriter.WritePropertyName("Operand2");
            jsonWriter.WriteValue(num2);
            jsonWriter.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    jsonWriter.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    jsonWriter.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    jsonWriter.WriteValue("Multiply");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        jsonWriter.WriteValue("Divide");
                    }
                    break;
                default:
                    break;
            }
            
            jsonWriter.WritePropertyName("Result");
            jsonWriter.WriteValue(result);
            jsonWriter.WriteEndObject();

            return result;
        }
        public void Finish(int calcUsed)
        {
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("Calculator Used:");
            jsonWriter.WriteValue(calcUsed);
            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndArray();
            jsonWriter.WriteEndObject();
            jsonWriter.Close();
        }
    }
}
