using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<string> sums;

        public Calculator()
        {
            sums = new List<string>();
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
            double result = double.NaN;
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
                    sums.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    sums.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    sums.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":

                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        sums.Add($"{num1} / {num2} = {result}");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Multiply");
                    sums.Add($"{num1} to the power of {num2} = {result}");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double SingleDigitOp(double num1, string op2)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");

            switch (op2)
            {
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square root");
                    sums.Add($"Square root of {num1} = {result}");
                    break;
                case "10x":
                    result = num1 * 10;
                    writer.WriteValue("10x");
                    sums.Add($"10x {num1} = {result}");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    sums.Add($"Sin of {num1} = {result}");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    sums.Add($"Tan of {num1} = {result}");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    sums.Add($"Cos of {num1} = {result}");
                    break;
                default:
                    Console.WriteLine("Invalid option");
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
