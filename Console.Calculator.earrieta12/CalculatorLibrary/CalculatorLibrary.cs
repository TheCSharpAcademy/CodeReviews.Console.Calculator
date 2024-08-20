using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        int count = 0;


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
                    count++;
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    count++;
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    count++;
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    count++;
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking the power");
                    count++;
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Count");
            writer.WriteValue(count);

            Helper.AddToHistory(num1, num2, op, result);
            writer.WriteEndObject();

            return result;
        }

        public double DoOperation(double num1, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

            writer.WriteStartObject();
            writer.WritePropertyName("Number");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {

                case "sr":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    count++;
                    break;

                case "x":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x");
                    count++;
                    break;

                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sine");
                    count++;
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosine");
                    count++;
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangent");
                    count++;
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Count");
            writer.WriteValue(count);
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