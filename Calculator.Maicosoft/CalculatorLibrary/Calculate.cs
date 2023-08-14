using Newtonsoft.Json;
using System.Diagnostics;

namespace CalculatorLibrary;

public class Calculate
{
    JsonWriter writer;
    public Calculate()
    {
        StreamWriter logFile = File.CreateText("Calculate.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }
    public string DoMath(double num1, double num2, string operation)
    {
        double result = 0;
        string output = string.Empty;
        string op = operation;

        while (!new[] {"a", "s", "m", "d"}.Contains(op))
        {
            Console.WriteLine("Not a valid input, Please enter an operation: ");
            op = Console.ReadLine();
        }
        switch (op)
        {
            case "a":
                result = num1 + num2;
                output = $"{num1} + {num2} = {result}";
                Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                break;
            case "s":
                result = num2 - num1;
                output = $"{num1} - {num2} = {result}";
                Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                break;
            case "m":
                result = num1 * num2;
                result = Math.Round(result, 2);
                output = $"{num1} * {num2} = {result}";
                Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                break;
            case "d":
                result = num1 / num2;
                result = Math.Round(result, 2);
                output = $"{num1} / {num2} = {result}";
                Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                break;
        }
        return output;
    }
}
