using System.Diagnostics;
using Newtonsoft.Json;

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

        public void Calculate(float n1, float n2, string op)
        {

            float result = 0;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(n1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(n2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    result = n1 + n2;
                    Console.WriteLine($"{n1} + {n2} = " + result);
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = n1 - n2;
                    Console.WriteLine($"{n1} - {n2} = " + result);
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = n1 * n2;
                    Console.WriteLine($"{n1} * {n2} = " + result);
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    if (n2 == 0)
                    {
                        Console.WriteLine("Can't divide by zero");
                        Console.WriteLine("Type another number:");
                        n2 = float.Parse(Console.ReadLine());
                    }
                    result = n1 / n2;
                    Console.WriteLine($"{n1} / {n2} = " + result);
                    writer.WriteValue("Divide");
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();


        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
    
}