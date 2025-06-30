using CalculatorLibrary.Models;
using Newtonsoft.Json;
using Spectre.Console;

namespace CalculatorLibrary;

public class CalculatorEngine
{
    internal static List<double> Results = new();
    private static readonly JsonWriter writer;

    static CalculatorEngine()
    {
        var logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public static double BasicOperations(OperationType operationType)
    {
        var operation = new Operation(operationType);

        var twoNumbers = Helpers.GetTwoNumbers();
        operation.Operand1 = twoNumbers[0];
        operation.Operand2 = twoNumbers[1];
        var num1 = operation.Operand1;
        var num2 = operation.Operand2;

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        if (operationType == OperationType.Addition)
        {
            operation.Result = (double)(num1 + num2);
            Helpers.PrintTwoNumberCalculation(num1, (double)num2, "+", operation.Result);
            Helpers.AddToCalculationList($"{num1} + {num2} = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Addition");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Subtraction)
        {
            operation.Result = (double)(num1 - num2);
            Helpers.PrintTwoNumberCalculation(num1, (double)num2, "-", operation.Result);
            Helpers.AddToCalculationList($"{num1} - {num2} = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Subtraction");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Multiplication)
        {
            operation.Result = (double)(num1 * num2);
            Helpers.PrintTwoNumberCalculation(num1, (double)num2, "*", operation.Result);
            Helpers.AddToCalculationList($"{num1} * {num2} = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Multiplication");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Division)
        {
            operation.Result = (double)(num1 / num2);
            Helpers.PrintTwoNumberCalculation(num1, (double)num2, "/", operation.Result);
            Helpers.AddToCalculationList($"{num1} / {num2} = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Division");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Exponentiation)
        {
            operation.Result = Math.Pow(num1, (double)num2);
            Helpers.PrintTwoNumberCalculation(num1, (double)num2, "^", operation.Result);
            Helpers.AddToCalculationList($"{num1} ^ {num2} = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Exponentiation");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }

        return operation.Result;
    }

    internal static double AdvancedOperations(OperationType operationType)
    {
        var operation = new Operation(operationType);

        operation.Operand1 = Helpers.GetSingleNumber();
        var num1 = operation.Operand1;

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operation");

        if (operationType == OperationType.SquareRoot)
        {
            if (num1 < 0)
            {
                AnsiConsole.WriteLine(
                    "Cannot calculate the square root of a negative number. Please try again."
                );
                num1 = Helpers.GetSingleNumber();
            }

            operation.Result = Math.Sqrt(num1);
            Helpers.PrintSingleNumberCalculation(num1, "√", operation.Result);
            Helpers.AddToCalculationList($"√{num1} = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("SquareRoot");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Sine)
        {
            operation.Result = Math.Sin(num1);
            Helpers.PrintSingleNumberCalculation(num1, "sin", operation.Result);
            Helpers.AddToCalculationList($"sin({num1}) = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Sine");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Cosine)
        {
            operation.Result = Math.Cos(num1);
            Helpers.PrintSingleNumberCalculation(num1, "cos", operation.Result);
            Helpers.AddToCalculationList($"cos({num1}) = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Cosine");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }
        else if (operationType == OperationType.Tangent)
        {
            operation.Result = Math.Tan(num1);
            Helpers.PrintSingleNumberCalculation(num1, "tan", operation.Result);
            Helpers.AddToCalculationList($"tan({num1}) = {operation.Result}");
            Results.Add(operation.Result);
            writer.WriteValue("Tangent");
            writer.WritePropertyName("Result");
            writer.WriteValue(operation.Result);
            writer.WriteEndObject();
        }

        return operation.Result;
    }

    // Closes the JSON object and array and outputs the log file to bin/debug.
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
