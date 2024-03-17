using System.Diagnostics;
using Newtonsoft.Json;
namespace CalculatorLibrary;
public class Calculator
{
    JsonWriter writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }
    public double DoOperation(double firstNumber, double secondNumber, string operation)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(firstNumber);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(secondNumber);
        writer.WritePropertyName("Operation");

        switch (operation)
        {
            case "a":
                result = firstNumber + secondNumber;
                writer.WriteValue("Add");
                break;
            case "s":
                result = firstNumber - secondNumber;
                writer.WriteValue("Subract");
                break;
            case "m":
                result = firstNumber * secondNumber;
                writer.WriteValue("Multiply");
                break;
            case "d":
                if (secondNumber != 0)
                {
                    result = firstNumber / secondNumber;
                    writer.WriteValue("Divide");
                }
                break;
            default:
                break;
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

