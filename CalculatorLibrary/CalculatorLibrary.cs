using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        List<string> calculationList = new List<string>();
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
            writer.WritePropertyName("Operand 1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand 2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    calculationList.Add($"{num1} + {num2} = {result}");
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculationList.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculationList.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        calculationList.Add($"{num1} / {num2} = {result}");
                        writer.WriteValue("Divide");
                    }
                    break;
                // Return text for an incorrect option entry.
                case "sqrt":
                    result = Math.Sqrt(num1);
                    calculationList.Add($"{num1}^(1/2) = {result}");
                    writer.WriteValue("Square root");
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    calculationList.Add($"{num1}^{num2} = {result}");
                    writer.WriteValue("Taking the Power");
                    break;
                case "exp":
                    result = Math.Pow(10, num1);
                    calculationList.Add($"10^{num1} = {result}");
                    writer.WriteValue("10x");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    calculationList.Add($"sin({num1}) = {result}");
                    writer.WriteValue("Sine");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    calculationList.Add($"cos({num1}) = {result}");
                    writer.WriteValue("Cosine");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    calculationList.Add($"tan({num1}) = {result}");
                    writer.WriteValue("Tangent");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void ViewCalculation()
        {
            int i = 1;
            for (int item = calculationList.Count - 1; item >= 0; item--)
            {
                Console.WriteLine($"{i}. {calculationList[item]}");
                i++;
            }
            Console.WriteLine("\n------------------------\n");

            Console.WriteLine();
        }

        public int GetListLength()
        {
            return calculationList.Count;
        }

        public double GetResult(int i)
        {
            calculationList.Reverse();
            string s = calculationList[i - 1];
            s = s.Substring(s.IndexOf("= ") + 1);
            calculationList.Reverse();
            return Double.Parse(s);
        }

        public void EmptyList()
        {
            if (calculationList.Count == 0)
                Console.WriteLine("List is empty");
            else
            {
                calculationList.Clear();
                Console.WriteLine("List cleared");
            }
        }


        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}