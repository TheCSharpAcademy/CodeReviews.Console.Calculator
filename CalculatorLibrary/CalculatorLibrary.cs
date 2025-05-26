using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        private static readonly List<string> calculationList = new List<string>();

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

        public double Calculate(double num1, double num2, string operation)
        {
            double result = double.NaN;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (operation)
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
                default:
                    Console.WriteLine("Invalid operation. Please try again.");
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        // Closes the JSON object and array and outputs the log file to bin/debug.
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public static string GetOperator(string choice)
        {
            return choice switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "e" => "^",
                "r" => "√",
                "f" => "!",
                "l" => "log",
                "n" => "sin",
                "c" => "cos",
                "t" => "tan",
                "cot" => "cot",
                "sec" => "sec",
                "csc" => "csc",
            };
        }

        public static void PrintCalculation(double num1, double num2, string operationType, double result)
        {

            Console.WriteLine($"\t The result of {num1} {operationType} {num2} is: {result}\n");
        }

        public static void AddToCalculationList(string calculation)
        {
            calculationList.Add(calculation);
        }

        public static void PrintCalculationList()
        {
            if (calculationList.Count == 0)
            {
                Console.WriteLine("No calculations have been performed yet.");
                return;
            }
            else
            {
                Console.WriteLine("----------------------------------------------------\n");
                Console.WriteLine("\t Calculation History:\n");
                foreach (var calculation in calculationList)
                {
                    Console.WriteLine($"\t {calculation}");
                }
            }
        }
    }
}
