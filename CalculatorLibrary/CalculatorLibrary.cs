using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    List<(double num1, double num2, double result, string symbol)> latestCalculations = new List<(double, double, double, string)>();

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
        string symbol = "";
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
                symbol = "+";
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                symbol = "-";
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                symbol = "*";
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                symbol = "/";
                break;
            case "sq":
                result = Math.Sqrt(num1);
                writer.WriteValue("Square Root");
                symbol = "√";
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Taking the Power");
                symbol = "^";
                break;
            case "t":
                result = Math.Pow(10, num1);
                writer.WriteValue("Raising 10 to the Power of x");
                symbol = "10^";
                break;
            case "sin":
                result = Math.Sin(ToRadian(num1));
                writer.WriteValue("Sine");
                symbol = "sin";
                break;
            case "cos":
                result = Math.Cos(ToRadian(num1));
                writer.WriteValue("Cosine");
                symbol = "cos";
                break;
            case "tan":
                result = Math.Tan(ToRadian(num1));
                writer.WriteValue("Tangent");
                symbol = "tan";
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        latestCalculations.Add((num1, num2, result, symbol));
        return result;
    }
    public List<(double num1, double num2, double result, string symbol)> GetCalculations()
    {
        return latestCalculations;
    }
    public void Finish(int usageCount)
    {
        writer.WriteEndArray();
        writer.WritePropertyName("Amount of Times Calculator Used (in operations)");
        writer.WriteValue(usageCount);
        writer.WriteEndObject();
        writer.Close();
    }
    public double ToRadian(double value)
    {
        return value * (Math.PI / 180.0);
    }
}