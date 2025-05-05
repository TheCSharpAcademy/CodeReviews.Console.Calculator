using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;    

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.log");
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
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;

                case "pow":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;

                case "10x":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10 to the power of x");
                    break;

                case "trig":
                    result = TrigonoFunctions(num1);
                    writer.WriteValue("Trigonometric Functions");
                    break;

                default:
                    break;

            }

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double TrigonoFunctions(double num1)
        {
            Console.Clear();
            Console.WriteLine(@"What trigonometric function you want to perform?
    sin - sine
    cos - cosine
    tan - tangent

    Your option?");
            string? trigOp = Console.ReadLine();

            while (string.IsNullOrEmpty(trigOp) || !Regex.IsMatch(trigOp, "[sin|cos|tan]"))
            {
                Console.WriteLine("Error: Unrecognized input. Please enter the available function: ");
                trigOp = Console.ReadLine();
            }
            double result = double.NaN; 

            switch (trigOp)
            {
                case "sin":
                    result = Math.Sin(num1);
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    break;
                default:
                    break;
            }

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
