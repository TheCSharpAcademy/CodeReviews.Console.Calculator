// CalculatorLibrary.cs
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class CalculatorLibrary
    {
        public class Calculator
        {
            JsonWriter writer;

            public List<(double, double?, double, string)> history = new (3);
            private string oper;

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
                        oper = "+";
                        break;
                    case "s":
                        result = num1 - num2;
                        writer.WriteValue("Subtract");
                        oper = "-";
                        break;
                    case "m":
                        result = num1 * num2;
                        writer.WriteValue("Multiply");
                        oper = "*";
                        break;
                    case "d":
                        // Ask the user to enter a non-zero divisor.
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        writer.WriteValue("Divide");
                        oper = "/";
                        break;
                    case "p":
                        result = Math.Pow(num1, num2);
                        writer.WriteValue("Power");
                        oper = "^";
                        break;
                    case "mod":
                        result = num1 % num2;
                        writer.WriteValue("Modulo");
                        oper = "%";
                        break;
                    case "root":
                        if (num2 != 0)
                            result = Math.Pow(num1, 1/ num2);
                        writer.WriteValue("Root of Nth");
                        oper = "**";
                        break;
                    case "10x":
                        result = num1 * 10;
                        writer.WriteValue("10x");
                        oper = "* 10";
                        break;
                    case "sin":
                        result = Math.Sin(num1);
                        writer.WriteValue("sin");
                        oper = "sin";
                        break;
                    case "cos":
                        result = Math.Cos(num1);
                        writer.WriteValue("cos");
                        oper = "cos";
                        break;
                    // Return text for an incorrect option entry.
                    default:
                        break;
                }

                if(history.Count < 3)
                    history.Add((num1, num2, result, oper));
                else
                {
                    for (var index = 0; index < history.Count; index++)
                    {
                        if (index < 2)
                        {
                            history[index] = history[index + 1];
                        }
                        else history[index] = (num1, num2, result, oper);
                    }
                }
                writer.WritePropertyName("Result");
                writer.WriteValue(result);
                writer.WriteEndObject();

                return result;
            }
            // Redefine method for second menu operations
            public double DoOperation(double num1, string op)
            {
                double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
                writer.WriteStartObject();
                writer.WritePropertyName("Operand1");
                writer.WriteValue(num1);
                writer.WritePropertyName("Operation");
                // Use a switch statement to do the math.
                switch (op)
                {
                    case "10x":
                        result = num1 * 10;
                        writer.WriteValue("10x");
                        oper = "* 10";
                        break;
                    case "sin":
                        result = Math.Sin(num1);
                        writer.WriteValue("sin");
                        oper = "sin";
                        break;
                    case "cos":
                        result = Math.Cos(num1);
                        writer.WriteValue("cos");
                        oper = "cos";
                        break;
                    // Return text for an incorrect option entry.
                    default:
                        break;
                }

                if (history.Count < 3)
                    history.Add((num1, null, result, oper));
                else
                {
                    for (var index = 0; index < history.Count; index++)
                    {
                        if (index < 2)
                        {
                            var x = history[index];
                            history[index] = history[index + 1];
                        }
                        else history[index] = (num1, null, result, oper);
                    }
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
}
