using CalculatorLibrary.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace CalculatorLibrary;

public class Calculator
{
    private static int _CalculatorUseCount = 0;
    public static List<Calculation> _CalculationsList = new List<Calculation>();


    JsonWriter writer;
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
    public double DoOperation(double num1, Enums.OperationType op, double? num2 = null)
    {
        double? result = double.NaN;

        if (num2 == null) {
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case Enums.OperationType.SquareRoot:
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case Enums.OperationType.TenX:
                    result = num1 * 10;
                    writer.WriteValue("10(x) Multiplier");
                    break;
                case Enums.OperationType.Cosine:
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosine()");
                    break;
                case Enums.OperationType.Sine:
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin()");
                    break;
                default:
                    break;
            }
        }
        else
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case Enums.OperationType.Add:
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case Enums.OperationType.Subtract:
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case Enums.OperationType.Multiply:
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case Enums.OperationType.Divide:
                    while (num2 == 0)
                    {
                        Console.WriteLine("The operation yields a mathematical error - cannot divide by zero!\n");
                        num2 = Calculation.GetCalculateNumber(2);
                    }
                    result = num1 / num2;
                    writer.WriteValue("Divide");
                    break;
                case Enums.OperationType.Power:
                    result = Math.Pow(num1, num2??0);
                    writer.WriteValue("Power");
                    break;
                default:
                    break;
            }
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        _CalculatorUseCount++;

        return Math.Round(result ?? 0, 2);
    }

    public double DoOperation(double num1, Enums.OperationType op)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case Enums.OperationType.SquareRoot:
                result = Math.Sqrt(num1);
                writer.WriteValue("Square Root");
                break;
            case Enums.OperationType.TenX:
                result = num1 * 10;
                writer.WriteValue("10(x) Multiplier");
                break;
            case Enums.OperationType.Cosine:
                result = Math.Cos(num1);
                writer.WriteValue("Cosine()");
                break;
            case Enums.OperationType.Sine:
                result = Math.Sin(num1);
                writer.WriteValue("Sin()");
                break;
            default:
                break;
        }
        result = Math.Round(result, 2);

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        _CalculatorUseCount++;

        return result;
    }
    public void PreviewCalculations()
    {
        foreach (Calculation calc in _CalculationsList)
        {
            calc.GetDetails();
        }
    }

    public void Finish()
    {
        // Closes previously opened object (Operations) and its array of operations that were written
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
