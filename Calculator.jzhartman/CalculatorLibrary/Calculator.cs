using System.Diagnostics;
using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<CalculationModel> History { get; set; }
        public int TimesUsed { get; set; }

        public Calculator()
        {
            this.History = new List<CalculationModel>();
            this.TimesUsed = 0;

            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(double num1, double num2, string operation)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (operation.ToLower())
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
                case "q":
                    if (num1 > 0)
                    {
                        result = Math.Sqrt(num1);
                    }
                    writer.WriteValue("Square Root");
                    break;
                case "r":
                    result = Math.Pow(num1, 1.0 / num2);
                    writer.WriteValue("y Root");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                default:
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

        public static string GetOperationSymbol(string operation)
        {
            string operationSymbol = string.Empty;

            switch (operation)
            {
                case "a":
                    operationSymbol = "+";
                    break;
                case "s":
                    operationSymbol = "-";
                    break;
                case "m":
                    operationSymbol = "*";
                    break;
                case "d":
                    operationSymbol = "/";
                    break;
                case "q":
                    operationSymbol = "sqrt";
                    break;
                case "r":
                    operationSymbol = "root";
                    break;
                case "e":
                    operationSymbol = "^";
                    break;
                default:
                    break;
            }

            return operationSymbol;
        }
    }
}
