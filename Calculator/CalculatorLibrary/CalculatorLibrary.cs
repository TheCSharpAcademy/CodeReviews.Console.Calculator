// CalculatorLibrary.cs
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CalculatorLibrary
{
    public class Calculator

    {
        JsonWriter writer;
        string logFilePath;

        public Calculator()
        {
            logFilePath = "C:\\Users\\GuiSteamDeck\\Desktop\\C#\\C# Academy\\Calculator\\Calculator\\calculatorlog.json";
            StreamWriter logFile = File.CreateText(logFilePath);
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
                        writer.WriteValue("Divide");
                    }
                    break;
                case "srt":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("SquareRoot");
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "trig sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Trigonometry - Sin");
                    break;
                case "trig cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Trigonometry - Cos");
                    break;
                case "trig tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Trigonometry - Tan");
                    break;

                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public string History(double num1, double num2, string op)
        {
            string opSignal = ""; // Variable to store the operator signal.

            switch (op)
            {
                case "a":
                    opSignal = "+";
                    break;
                case "s":
                    opSignal = "-";
                    break;
                case "m":
                    opSignal = "*";
                    break;
                case "d":
                    opSignal = "/";
                    break;
                case "srt":
                    opSignal = "√";
                    break;
                case "pow":
                    opSignal = "^";
                    break;
                case "trig sin":
                    opSignal = "sin\u03B8";
                    break;
                case "trig cos":
                    opSignal = "cos\u03B8";
                    break;
                case "trig tan":
                    opSignal = "tan\u03B8";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            string result = $"{num1} {opSignal} {num2} = ";

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