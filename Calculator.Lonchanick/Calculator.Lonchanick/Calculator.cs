using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Calculator.Lonchanick
{
    public class Calculator
    {
        JsonWriter writer;
        public Calculator() {
            string path = "D:/.NET FOLDER/C# ACADEMY/Lonchanick9427.Calculator" +
                "/Calculator.Lonchanick/Calculator.Lonchanick/bin/Debug/net6.0/calculatorlog.json";

            if(File.Exists(path)) 
            {
                StreamWriter logFile = File.AppendText(path);
                logFile.AutoFlush = true;
                writer = new JsonTextWriter(logFile);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartArray();
            }

            
            /*
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();*/
        }
        public void Finish()
        {
            writer.WriteEndArray();
            //writer.WriteEndObject();
            writer.Close();
        }
        public void DoOperation(double num1, double num2, string op)
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
                    while (num2 == 0)
                    {
                        Console.WriteLine("Enter a non-zero divisor: ");
                        num2 = Convert.ToInt32(Console.ReadLine());
                    }
                    result = num1 / num2;
                    Console.WriteLine($"Your result: {num1} / {num2} = " + result);
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
        }
    }

}
