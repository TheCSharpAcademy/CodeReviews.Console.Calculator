using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class CalculatorLog
{
    ListFunctions listFunctions = new ListFunctions();
    JsonWriter writer;
    string operation = "";
    int totalComputations = 0;
    public CalculatorLog() 
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
  
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                operation = "Add";
                break;
            case "s":
                result = num1 - num2;
                operation = "Subtract";
                break;
            case "m":
                result = num1 * num2;
                operation = "Multiply";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                operation = "Divide";
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        WriteToLog(num1, num2, operation, result);
       
        // Add to tally
        totalComputations++;

        // Write to list for user access during runtime
        listFunctions.WriteList(num1, num2, operation, result, totalComputations);
        
        return result;
    }

    public void WriteToLog(double num1, double num2, string operation, double result)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        writer.WriteValue(operation);
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
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

