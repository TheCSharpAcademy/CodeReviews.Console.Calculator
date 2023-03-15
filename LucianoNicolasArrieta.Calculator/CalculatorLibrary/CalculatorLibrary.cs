using Newtonsoft.Json;
using System.Diagnostics;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        List<string> previousCalculations = new List<string>();

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
                    previousCalculations.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    previousCalculations.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    previousCalculations.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        previousCalculations.Add($"{num1} / {num2} = {result}");
                    }
                    writer.WriteValue("Divide");
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

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public void LastestCalculations()
        {
            int i = 0;
            Console.WriteLine("------------------------");
            if (!previousCalculations.Any())
            {
                Console.WriteLine("There is no previous calculations done.");
            }
            else
            {
                foreach (string calculation in previousCalculations)
                {
                    i++;
                    Console.WriteLine($"{i}. {calculation}");
                }
            }
            Console.WriteLine("------------------------");
        }
        /*
        public int UseAPreviousResult()
        {
            this.LastestCalculations();
            Console.Write("Select the result you want to use in the calculator: ");

        }
        */
    }
}