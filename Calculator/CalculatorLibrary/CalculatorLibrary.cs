using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        int timesUsed;
        List<string> operationsResults = new List<string>() { "Calculation History:"};

        JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("operations");
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
                    operationsResults.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operationsResults.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operationsResults.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        operationsResults.Add($"{num1} / {num2} = {result}");
                    }
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    operationsResults.Add($"V{num1} = {result}");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking Power");
                    operationsResults.Add($"{num1}^{num2} = {result}");
                    break;
                case "x":
                    result =  Math.Pow(10, num1);
                    writer.WriteValue("10^x");
                    operationsResults.Add($"10^{num1}= {result}");
                    break;
                case "c":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosinus");
                    operationsResults.Add($"Cos {num1} = {result}");
                    break;
                case "t":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sinus");
                    operationsResults.Add($"Sin {num1} = {result}");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            timesUsed++;
            
            return result;
        }

        public int GetUsageTimes()
        {
        return timesUsed;   
        }

        public void ShowCalculationsList()
        {
            if (operationsResults.Count > 1)
            operationsResults.ForEach(Console.WriteLine);
            else Console.WriteLine("No operations are recorded yet.\n");
        }

        public void ClearCalculationList()
        {
            operationsResults.Clear();
            operationsResults.Add("Calculation History:");
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

    }
}
