using System.Diagnostics;
using System.Globalization;

namespace CalculatorLibrary;

public class CalculatorLib
{
    public CalculatorLib()
    {
        var logFile = File.CreateText("calculator.log");
        Trace.Listeners.Add(new TextWriterTraceListener(logFile));
        Trace.AutoFlush = true;
        Trace.WriteLine("Starting Calculator Log");
        Trace.WriteLine($"Started {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
    }

    public double DoOperation(double num1, double num2, string op)
    {
        var result = double.NaN;

        switch (op)
        {
            case "a":
                result = num1 + num2;
                Trace.WriteLine($"{num1} + {num2} = {result}");
                break;
            case "s":
                result = num1 - num2;
                Trace.WriteLine($"{num1} - {num2} = {result}");
                break;
            case "m":
                result = num1 * num2;
                Trace.WriteLine($"{num1} * {num2} = {result}");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    Trace.WriteLine($"{num1} / {num2} = {result}");
                }

                break;
        }

        return result;
    }
}