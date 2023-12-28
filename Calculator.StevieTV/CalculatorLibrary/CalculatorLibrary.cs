using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    public int timesUsed = 0;
    internal static List<Operation> operations = new List<Operation> { };
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
        var result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        var operation = "";
        writer.WriteStartObject();
        writer.WritePropertyName("Sequence");
        writer.WriteValue(timesUsed + 1);
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
                timesUsed++;
                operation = "+";
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                timesUsed++;
                operation = "-";
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                timesUsed++;
                operation = "*";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                timesUsed++;
                operation = "/";
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power Of");
                timesUsed++;
                operation = "^";
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        RecordOperation(timesUsed, num1, num2, result, operation);
        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void ShowHistory()
    {
        foreach (var operation in operations)
        {
            Console.WriteLine($"{operation.Sequence}: {operation.Operand1}{operation.OperationType}{operation.Operand2} = {operation.Result}");
        }
    }

    internal static void RecordOperation(int sequence, double operand1, double operand2, double result, string operation)
    {
        operations.Add(new Operation
        {
            Sequence = sequence,
            Operand1 = operand1,
            Operand2 = operand2,
            Result = result,
            OperationType = operation,
        });
    }
}