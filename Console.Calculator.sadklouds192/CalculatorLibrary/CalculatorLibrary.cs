using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace CalculatorLibrary;
// CalculatorLibrary.cs
using System.Diagnostics;
public class Calculator
{
    // CalculatorLibrary.cs
    JsonWriter writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Newtonsoft.Json.Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
        
    }
    
    public static int NumberOfCalculations{ get; set; }
    public static List<double> Calculations { get; set; } = new List<double>();
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
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.s
                if (num2 != 0)
                {
                    result = num1 / num2;
                    writer.WriteValue("Divide");
                    Calculations.Add(result);
                    NumberOfCalculations++;
                }
                break;
            case "q":
                result = Math.Sqrt(num1);
                writer.WriteValue("SquareRoot");
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "t":
                result = Math.Pow(num1, 10);
                writer.WriteValue("Power 10");
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        return result;
    }
    
    // CalculatorLibrary.cs
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public string ShowCalculations()
    {
        string result = "";
        if (Calculations.Count == 0)
        {
            result = "There are no calculations to show";
        }
        foreach (var calculation in Calculations)
        {
            result += $"{calculation}, ";
        }
        return result;
    }
    
    public void ClearCalculations()
    {
        Calculations.Clear();
    }
}