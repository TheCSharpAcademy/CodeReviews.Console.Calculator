using System.Net;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        // if i delete this it gives an error on its absence
        // ##########################
        static void Main() { }
        // ##########################
        JsonWriter writer;
        List<string> calculations = new List<string>();
        List<double> results = new List<double>();
        int count = 0;
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
                    History(num1, num2, result, op);
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    History(num1, num2, result, op);
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    History(num1, num2, result, op);
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    History(num1, num2, result, op);
                    break;
                // Return text for an incorrect option entry.
                default:
                    Console.WriteLine("Thats not an operation.");
                    writer.WriteValue("Invalid operation");
                    Console.ReadLine();
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

        public void History(double num1 = 0, double num2 = 0, double result = 0, string op = "")
        {
            op = Operator(op);
            calculations.Add($"{count + 1}. {num1} {op} {num2} = {result}");
            results.Add(result);
            count++;
        }

        public double ShowHistory()
        {
            Console.Clear();
            double num1 = 0;
            string? result = "";
            foreach (string calculation in calculations)
            {
                Console.WriteLine(calculation);
            }
            while (string.IsNullOrEmpty(result))
            {

                Console.WriteLine("u. Use previous result");
                Console.WriteLine("d. Delete history");
                Console.WriteLine("Enter any other key to do another calculation.");
                result = Console.ReadLine();
            }
            if (result == "u")
            {
                Console.WriteLine("Type the number of the result you would like to use: ");
                result = Console.ReadLine();
                if (int.TryParse(result, out _) && Convert.ToDouble(result) > 0)
                    num1 = Convert.ToDouble(results[(Convert.ToInt32(result) - 1)]);
                else
                {
                    Console.WriteLine("Invalid number");
                    Console.ReadLine();
                }
            }
            else if (result == "d")
            {
                calculations.Clear();
                results.Clear();
                count = 0;
            }
            return num1;
        }

        public string Operator(string op)
        {
            switch (op)
            {
                case "a":
                    op = "+";
                    break;
                case "s":
                    op = "-";
                    break;
                case "m":
                    op = "*";
                    break;
                case "d":
                    op = "/";
                    break;
            }
            return op;
        }
    }
}
