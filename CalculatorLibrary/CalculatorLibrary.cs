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

        public double DoOperation(double num1, double num2, string op, int count)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            
            writer.WriteStartObject();
            writer.WritePropertyName("#");
            writer.WriteValue(count);
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "1":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "2":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "3":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "4":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    writer.WriteValue("Error");
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
    }
}