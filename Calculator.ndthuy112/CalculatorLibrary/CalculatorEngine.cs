using Newtonsoft.Json;
using static CalculatorLibrary.CalculationModel;

namespace CalculatorLibrary
{
    public class CalculatorEngine
    {
        JsonWriter writer;
        public CalculatorEngine()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public CalculationModel DoOperation(double num1, double num2, string op)
        {
            double answer = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            CalculationType type;
            switch (op)
            {
                case "a":
                    answer = num1 + num2;
                    writer.WriteValue("Add");
                    type = CalculationType.Add;
                    break;
                case "s":
                    answer = num1 - num2;
                    writer.WriteValue("Subtract");
                    type = CalculationType.Subtract;
                    break;
                case "m":
                    answer = num1 * num2;
                    writer.WriteValue("Multiply");
                    type = CalculationType.Multiply;
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        answer = num1 / num2;
                        writer.WriteValue("Divide");
                        type = CalculationType.Divide;
                    }
                    else
                    {
                        writer.WriteValue("Fail Divide");
                        type = CalculationType.FailDivide;
                    }
                    break;
                // Return text for an incorrect option entry.
                default:
                    type = CalculationType.FailDivide;
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(answer);
            writer.WriteEndObject();

            return new CalculationModel(num1, num2, type, answer);
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
