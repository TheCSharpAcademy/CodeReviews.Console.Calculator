using Newtonsoft.Json;
using System.Diagnostics;

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

        public Operation DoOperation(double num1, double num2, string op)
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
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
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
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                case "p":
                    result = 1;
                    for (int i = 0; i < num2; i++) { result *= num1; }
                    writer.WriteValue("Power of");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            Operation return_op = new(num1, num2, op, result);
            return return_op;
        }
        public Operation DoOperation(double num1, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Sqrt");
                    break;
                case "s":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;
                case "c":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;
                case "p":
                    result = 1;
                    for (int i = 0; i < num1; i++) { result *= 10; }
                    result = Math.Cos(num1);
                    writer.WriteValue("10^");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            Operation return_op = new(num1, double.NaN, op, result);
            return return_op;
    }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }

    public class Operation
    {
        double num1;
        double num2;
        string op;
        double result;

        public Operation(double num1, double num2, string op, double result)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.op = op;
            this.result = result;
        }

        public double GetResult()
        {
            return result;
        }

        public string GetOperationString()
        {
            if (num2 == double.NaN)
            {
                switch (op)
                {
                    case "r":   return $"sqrt({num1})= {result}";
                    case "s":   return $"sin({num1})= {result}";
                    case "c":   return $"cos({num1})= {result}";
                    case "p":   return $"10^{num1} = {result}";
                    default:    return result.ToString();
                }
            }
            else 
            {
                switch (op)
                {
                    case "a":   return $"{num1} + {num2} = {result}";
                    case "s":   return $"{num1} - {num2} = {result}";
                    case "m":   return $"{num1} x {num2} = {result}";
                    case "d":   return $"{num1} / {num2} = {result}";
                    case "p":   return $"{num1} ^ {num2} = {result}";
                    default:    return result.ToString();
                }
            } 
        }
    }
}