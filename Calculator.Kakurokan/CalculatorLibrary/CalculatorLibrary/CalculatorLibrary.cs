using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class Calculator
    {
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

        public double Calculate(double num1, double num2, string op)
        {

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            double result = op switch
            {
                "a" => num1 + num2,
                "s" => num1 - num2,
                "m" => num1 * num2,
                "d" => num1 / PreventDivisionError(num2),
                _ => double.NaN
            };

            string operationName = op switch
            {
                "a" => "Add",
                "s" => "Subtract",
                "m" => "Multiply",
                "d" => "Divide"
            };

            writer.WriteValue(operationName);

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

        public static double PreventDivisionError(double num2)
        {
            while (num2 == 0)
            {
                Console.WriteLine("You entered a non-zero divisor");
                Console.WriteLine("Enter the seconde number again: ");
                num2 = Convert.ToInt32(Console.ReadLine());
            }
            return num2;
        }
    }
}
