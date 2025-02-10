using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public List<double> Results = new List<double>();

        public int Counter = 0;
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
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "sqrt":
                    if (num1 >= 0)
                    {
                        result = Math.Sqrt(num1);
                        writer.WriteValue("SquareRoot");
                    }
                    else
                    {
                        Console.WriteLine("Cannot calculate the square root of a negative number.");
                    }
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    break;
                default:
                    Console.WriteLine("Invalid operation.");
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            Counter++;
            Results.Add(result);
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
