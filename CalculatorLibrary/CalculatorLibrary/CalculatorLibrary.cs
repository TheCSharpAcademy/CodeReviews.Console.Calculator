// CalculatorLibrary.cs

// CalculatorLibrary.cs
using Newtonsoft.Json;

namespace CalculatorLibrary
{

    public class Calculator
    {
        // CalculatorLibrary.cs
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
        // CalculatorLibrary.cs
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


        // CalculatorLibrary.cs
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
                // Return text for an incorrect option entry.
                case "r":
                    {
                        result = Math.Sqrt(num1);
                        writer.WriteValue("SquareRoot");
                        break;

                    }
                case "t":
                    {
                        result = num1 * 10.00;
                        writer.WriteValue("10x");
                        break;
                    }
                case "p":
                    {
                        result = Math.Pow(num1, num2);
                        writer.WriteValue("power of");
                        break;
                    }
                case "c":
                    {
                        result = Math.Cos(num1);
                        writer.WriteValue("cosine");
                        break;
                    }
                case "sn":
                    {
                        result = Math.Sin(num1);
                        writer.WriteValue("sine");
                        break;
                    }
                case "ta":
                    {
                        result = Math.Tan(num1);
                        writer.WriteValue("tangent");
                        break;
                    }
                case "ct":
                    {
                        result = Math.Cos(num1) / Math.Sin(num1);
                        writer.WriteValue("cotangent");
                        break;
                    }
                case "se":
                    {
                        result = 1 / Math.Cos(num1);
                        writer.WriteValue("secant");
                        break;
                    }
                case "cose":
                    {
                        result = 1 / Math.Sin(num1);
                        writer.WriteValue("cosecant");
                        break;
                    }

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


