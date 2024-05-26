using System.Text;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        public int NumberOfOperations = 0;
        

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
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking the Power");
                    break;
                case "x":
                    result = num1 * 10;
                    writer.WriteValue("10x");
                    break;
                case "t":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sine");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            NumberOfOperations++;
            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WritePropertyName("Number of times the calculator was used");
            writer.WriteValue(NumberOfOperations);
            writer.WriteEndObject();
            writer.Close();
        }
    }

    public class Operations
    {
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }
        public string Operation { get; set; } = default!;
        public double Result { get; set; }
    }

}