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
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case "+":
                result = (double)(num1 + num2);
                writer.WriteValue("Add");
                break;
            case "-":
                result = (double)(num1 - num2);
                writer.WriteValue("Subtract");
                break;
            case "*":
                result = (double)(num1 * num2);
                writer.WriteValue("Multiply");
                break;
            case "/":
                if (num2 != 0)
                {
                    result = (double)(num1 / num2);
                }
                writer.WriteValue("Divide");
                break;
            case "^":
                result = Pow(num1, (double)num2);
                writer.WriteValue("Pow");
                break;
            case "sqrt":
                result = Sqrt(num1);
                writer.WriteValue("Sqrt");
                break;
            case "10^":
                result = Pow(10, num1);
                writer.WriteValue("10^");
                break;
            case "sin":
                result = Sin(num1);
                writer.WriteValue("Sinus");
                break;
            case "cos":
                result = Cos(num1);
                writer.WriteValue("CosSinus");
                break;
            case "tan":
                result = Tan(num1);
                writer.WriteValue("Tangent");
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

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
}
