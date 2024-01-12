using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    int totalComputations = 0;
    public Calculator() 
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
        //Trace.WriteLine("Starting Calculator Log");
        //Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
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
        //writer.WriteStartArray();
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                //Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                //Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                //Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    //Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                }
                writer.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("product");
        writer.WriteValue(result);
        writer.WriteEndObject();
     
        totalComputations++;
        return result;
    }
    public void Finish()
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Computations ran");
        writer.WriteValue(totalComputations);
        writer.WriteEndObject();
        writer.WriteEndArray();
        //writer.WriteEndObject();
        writer.Close();
    }

}

