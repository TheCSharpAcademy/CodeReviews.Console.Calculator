using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator log");
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

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            if (Regex.IsMatch(op, "^(sq1|ten1|sq2|ten2|acos1|cos1|asin1|sin1|atan1|tan1|acos2|cos2|asin2|sin2|atan2|tan2)$")) writer.WriteValue(0);
            else writer.WriteValue(num2);
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
                    writer.WriteValue("Substract");
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
                        writer.WriteValue("Divide");
                    }
                    break;
                // Return text for an incorrect option entry.
                case "sq1":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Sqrt Num1");
                    break;
                case "sq2":
                    result = Math.Sqrt(num2);
                    writer.WriteValue("Sqrt Num1");
                    break;
                case "p1":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Num1^Num2");
                    break;
                case "p2":
                    result = Math.Pow(num2, num1);
                    writer.WriteValue("Num2^Num1");
                    break;
                case "ten1":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x Num1");
                    break;
                case "ten2":
                    result = Math.Pow(10, num2);
                    writer.WriteValue("10x Num1");
                    break;
                case "acos1":
                    result = Math.Acos(num1);
                    writer.WriteValue("Acos Num1");
                    break;
                case "cos1":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos Num1");
                    break;
                case "asin1":
                    result = Math.Asin(num1);
                    writer.WriteValue("Asin Num1");
                    break;
                case "sin1":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin Num1");
                    break;
                case "atan1":
                    result = Math.Atan(num1);
                    writer.WriteValue("Atan Num1");
                    break;
                case "tan1":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan Num1");
                    break;
                case "acos2":
                    result = Math.Acos(num2);
                    writer.WriteValue("Acos Num2");
                    break;
                case "cos2":
                    result = Math.Cos(num2);
                    writer.WriteValue("Cos Num2");
                    break;
                case "asin2":
                    result = Math.Asin(num2);
                    writer.WriteValue("Asin Num2");
                    break;
                case "sin2":
                    result = Math.Sin(num2);
                    writer.WriteValue("Sin Num2");
                    break;
                case "atan2":
                    result = Math.Atan(num2);
                    writer.WriteValue("Atan Num2");
                    break;
                case "tan2":
                    result = Math.Tan(num2);
                    writer.WriteValue("Tan Num2");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
    }
}
