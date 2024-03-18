using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<string> calculations = new List<string>();

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

            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculations.Add($"{num1} + {num2} = {result:0.##}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculations.Add($"{num1} - {num2} = {result:0.##}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculations.Add($"{num1} * {num2} = {result:0.##}");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        calculations.Add($"{num1} / {num2} = {result:0.##}");
                    }
                    writer.WriteValue("Divide");
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square root");
                    calculations.Add($"√{num1} = {result}");
                    break;
                case "e":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Exponentiation");
                    calculations.Add($"{num1}^{num2} = {result}");
                    break;
                case "e10":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("Exponential of 10");
                    calculations.Add($"10^{num1} = {result}");
                    break;
                case "sin":
                    result = Math.Sin(num1 * Math.PI / 180);
                    writer.WriteValue("Sine");
                    calculations.Add($"sin({num1}°) = {result}");
                    break;
                case "cos":
                    result = Math.Cos(num1 * Math.PI / 180);
                    writer.WriteValue("Cosine");
                    calculations.Add($"cos({num1}°) = {result}");
                    break;
                case "tan":
                    result = Math.Tan(num1 * Math.PI / 180);
                    writer.WriteValue("Tangent");
                    calculations.Add($"tan({num1}°) = {result}");
                    break;
                default:
                    Console.WriteLine("Incorrect operation!");
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