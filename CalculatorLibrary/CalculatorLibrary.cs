using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace CalculatorLibrary
    {
    public class Calculator
        {
        JsonWriter writer;
        public Calculator()
            {
            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
            }

        public void Finish()
            {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
            }
        private int count = 0;
        public void Counter()
            {
            count += 1;
            Console.WriteLine("This calculator was used {0} times.", count);
            }



        public string Operation(string operand)
            {
            string value = "";
            switch ( operand )
                {
            case "a":
                value = "Addition";
                break;
            case "m":
                value = "Multiplication";
                break;
            case "s":
                value = "Subtraction";
                break;
            case "d":
                value = "Division";
                break;
            case "r":
                value = "Square Root";
                break;
            case "p":
                value = "Power";
                break;
            case "t":
                value = "Trigonometry";
                break;
                }

            return value;
            }
        public string Sign(string operand)
            {
            string sign = "";
            switch ( operand )
                {
            case "a":
                sign = "+";
                break;
            case "m":
                sign = "*";
                break;
            case "s":
                sign = "-";
                break;
            case "d":
                sign = "/";
                break;
            case "r":
                sign = "r";
                break;
            case "p":
                sign = "^";
                break;
            case "t":
                sign = "Sin";
                break;
                }

            return sign;
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

            //Create a list to store history 

            switch ( op )
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
                if ( num2 != 0 )
                    {
                    result = num1 / num2;
                    writer.WriteValue("Divide");
                    }
                break;
            // Return text for an incorrect option entry.
            case "r":
                //Math.Sqrt(x);
                //num2 = 0;
                result = Math.Sqrt(num1);
                writer.WriteValue("Square Root");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
                break;
            case "t":
                //Math.Sin(double x)
                result = Math.Sin(num1);
                writer.WriteValue("Trigonometry");
                break;
            default:
                break;

                }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
            }
        }
    }
