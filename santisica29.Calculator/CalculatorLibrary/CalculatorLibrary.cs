using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{
    int timesCalcWasUsed = 0;
    static List<string> latestCalculations = new();
    static List<double> latestResults = new List<double>();

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
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        string operand = string.Empty;
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
                operand = "+";
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Substract");
                operand = "-";
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                operand = "*";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                while (num2 == 0)
                {
                    Console.WriteLine("You can't divide by 0. Pick another number");
                    num2 = int.Parse(Console.ReadLine());
                }
                result = num1 / num2;
                writer.WriteValue("Divide");
                operand = "/";
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        timesCalcWasUsed++;
        latestCalculations.Add($"{num1} {operand} {num2} = {result}");
        latestResults.Add(result);

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public void Finish()
    {
        CountTimesTheCalcWasUsed(timesCalcWasUsed);
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void CountTimesTheCalcWasUsed(int timesItWasUsed)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Times the calc was used");
        writer.WriteValue(timesItWasUsed);
        writer.WriteEndObject();
    }

    public static List<string> GetLatestsCalculations()
    {
        if (latestCalculations.Count == 0) return null;

        return latestCalculations;
    }

    public static void ShowLatestCalculations()
    {
        Console.Clear();

        var lC = GetLatestsCalculations();

        if (lC == null)
        {
            Console.WriteLine("List is empty\n");
            Console.WriteLine("'n' to start a new calculation");
        }
        else
        {
            Console.WriteLine("Latest Calculations:\n");
            foreach (var c in lC)
            {
                Console.WriteLine(c);
            }

            Console.WriteLine("\nPress 'd' to delete the list");
            Console.WriteLine("'n' to start a new calculation");
            Console.WriteLine("'u' to use one of the latest results to make a new operation");
        }
    }
    public static List<double> GetLatestsResults()
    {
        if (latestResults.Count == 0) return null;

        return latestResults;
    }

    public static double ShowAndUseLatestResults()
    {
        Console.Clear();
        var lR = GetLatestsResults();
        double num = 0;

        if (lR == null) return num;

        Console.WriteLine("Latest results: \n");
        foreach (var item in lR)
        {
            Console.Write($"{item}. y/n ");
            var c = Console.ReadLine().ToLower();

            if (c == "y")
            {
                num = item;
                break;
            }
        }

        return num;
    }

    public static void DeleteLists()
    {
        latestCalculations.Clear();
        latestResults.Clear();
    }
}
