using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;



public class Calculator
{
    JsonWriter writers;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writers = new JsonTextWriter(logFile);
        writers.Formatting = Formatting.Indented;
        writers.WriteStartObject();
        writers.WritePropertyName("Operations");
        writers.WriteStartArray();
    }

    public double DoOperation(double num, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        double radians = (num) * Math.PI / 180;

        switch (op)
        {
            case "r":
                result = Math.Sqrt(num);
                writers.WriteValue("square root");
                break;
            case "t":
                result = Math.Pow(10, num);
                writers.WriteValue("powers of 10");
                break;
            case "i":
                result = Math.Sin(radians);
                writers.WriteValue("sine");
                break;
            case "c":
                result = Math.Cos(radians);
                writers.WriteValue("cosine");
                break;
            case "n":
                result = Math.Tan(radians);
                writers.WriteValue("tangent");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }

        return result;
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        // When this method is called do the following

        // Write the begining of the JSON object
        writers.WriteStartObject();
        // Write the property name of a name/value in the JSON object (Operand1)
        writers.WritePropertyName("Operand1");
        // Write a double value in the JSON object(num1)
        writers.WriteValue(num1);
        writers.WritePropertyName("Operand2");
        writers.WriteValue(num2);
        // Write the property name of a name/value in the JSON object (Operations)
        writers.WritePropertyName("Operation");

        // Use a switch statement to do the math.
        // Add log output to each calculation (Trace.WriteLine(String format(X (+-*/) Y = Z))
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writers.WriteValue("add");
                break;
            case "s":
                result = num1 - num2;
                writers.WriteValue("subtract");
                break;
            case "m":
                result = num1 * num2;
                writers.WriteValue("multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    writers.WriteValue("divide");
                }
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writers.WritePropertyName("result");
        writers.WriteValue(result);
        writers.WriteEndObject();


        return result;
    }

    public void Finish()
    {
        writers.WriteEndArray();
        writers.WriteEndObject();
        writers.Close();
    }
}

