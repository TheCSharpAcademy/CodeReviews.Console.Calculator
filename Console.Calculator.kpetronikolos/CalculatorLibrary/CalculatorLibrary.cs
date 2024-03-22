using Newtonsoft.Json;
using static System.Math;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;

    private static int calculatorCount;

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

    public double DoOperation(double num1, double? num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        string operation;

        switch (op)
        {
            case "+":
                result = (double)(num1 + num2);
                operation = "Add";
                break;
            case "-":
                result = (double)(num1 - num2);
                operation = "Subtract";
                break;
            case "*":
                result = (double)(num1 * num2);
                operation = "Multiply";
                break;
            case "/":
                if (num2 != 0)
                {
                    result = (double)(num1 / num2);
                }
                operation = "Divide";
                break;
            case "^":
                result = Pow(num1, (double)num2);
                operation = "Pow";
                break;
            case "sqrt":
                result = Sqrt(num1);
                operation = "Sqrt";
                break;
            case "10^":
                result = Pow(10, num1);
                operation = "10^";
                break;
            case "sin":
                result = Sin(num1);
                operation = "Sine";
                break;
            case "cos":
                result = Cos(num1);
                operation = "Cosine";
                break;
            case "tan":
                result = Tan(num1);
                operation = "Tangent";
                break;
            default:
                return result;
        }

        WriteToFile(num1, num2, result, operation);

        calculatorCount++;

        return result;
    }    

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public int GetCalculatorCount()
    {
        return calculatorCount;
    }

    private void WriteToFile(double num1, double? num2, double result, string operation)
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
}
