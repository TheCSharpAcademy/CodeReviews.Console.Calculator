//  Create a functionality that will count the amount of times the calculator was used.
//  Store a list with the latest calculations. And give the users the ability to delete that list.
//  Allow the users to use the results in the list above to perform new calculations.
//  Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.
using Newtonsoft.Json;

namespace CalculatorLibrary;


public class Calculator
{
    JsonWriter writer;
    private static int usageCount;
    private static List<string> history = new List<string>();

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
                history.Add($"{history.Count + 1}) {num1} + {num2} = {result}");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                history.Add($"{history.Count + 1}) {num1} - {num2} = {result}");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                history.Add($"{history.Count + 1}) {num1} * {num2} = {result}");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                history.Add($"{history.Count + 1} - {num1} / {num2} = {result}");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
                history.Add($"{history.Count + 1}) {num1}^{num2} = {result}");
                break;

            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        usageCount++;

        return result;
    }

    public double DoOperation(double num1, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "r":
                result = Math.Sqrt(num1);
                writer.WriteValue("SquareRoot");
                history.Add($"{history.Count + 1}) √{num1} = {result}");
                break;
            case "p":
                result = Math.Pow(10, num1);
                writer.WriteValue("TenPower");
                history.Add($"{history.Count + 1}) 10^{num1} = {result}");
                break;
            case "s":
                result = Math.Sin(num1);
                writer.WriteValue("Sin");
                history.Add($"{history.Count + 1}) Sin({num1}) = {result}");
                break;
            case "c":
                result = Math.Cos(num1);
                writer.WriteValue("Cos");
                history.Add($"{history.Count + 1}) Cos({num1}) = {result}");
                break;
            case "t":
                result = Math.Tan(num1);
                writer.WriteValue("Tan");
                history.Add($"{history.Count + 1}) Tan({num1}) = {result}");
                break;

            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        usageCount++;

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public int GetUsageCount()
    {
        return usageCount;
    }

    public List<string> GetHistory()
    {
        return history;
    }

    public void ClearHistory()
    {
        history.Clear();
    }
}

