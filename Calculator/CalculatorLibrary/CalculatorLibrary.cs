using System;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        int counter;

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
            counter++;
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
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
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
            writer.Close();
        }

        //History is saved in json log and this function deletes file and creates new instead
        public void ClearHistory()
        {
            writer.Close();
            File.Delete(@"C:\Users\KM\Exercism\csharp\Calculator\Calculator\bin\Debug\net8.0\calculatorlog.json");
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.Close();
            Console.WriteLine("Calculation History cleared");
        }


    }
}
