using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private readonly JsonWriter writer;
    public List<string> CalculationHistory { get; } = new();
    public int UsageCount { get; private set; }
    
    public Calculator()
    {
        var logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        var result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        string operationName;

        switch (op)
        {
            case "a":
                result = num1 + num2;
                operationName = "Add";
                break;
            case "s":
                result = num1 - num2;
                operationName = "Subtract";
                break;
            case "m":
                result = num1 * num2;
                operationName = "Multiply";
                break;
            case "d":
                if (num2 != 0) result = num1 / num2;
                operationName = "Divide";
                break;
            case "sqrt":
                result = Math.Sqrt(num1);
                operationName = "SquareRoot";
                break;
            case "pow":
                result = Math.Pow(num1, num2);
                operationName = "Power";
                break;
            case "tenx":
                result = Math.Pow(10, num1);
                operationName = "TenX";
                break;
            case "sin":
                result = Math.Sin(num1);
                operationName = "Sine";
                break;
            case "cos":
                result = Math.Cos(num1);
                operationName = "Cosine";
                break;
            case "tan":
                result = Math.Tan(num1);
                operationName = "Tangent";
                break;
            default:
                operationName = "Invalid";
                break;
        }

        writer.WriteValue(operationName);

        if (!double.IsNaN(result))
        {
            UsageCount++;
            CalculationHistory.Add($"{num1} {operationName} {num2} = {result}");
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}