using System.Diagnostics;
using Newtonsoft.Json;
using CalculatorLibrary.Models;

namespace CalculatorLibrary;

public class Calculator
{
    internal static List<Operation> operations = new();
    JsonWriter writer;
    public int calculationsAmount = 0;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        Trace.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        // TODO: Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                AddToHistory(num1, num2, result, OperationType.Addition);
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                AddToHistory(num1, num2, result, OperationType.Subtraction);
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                AddToHistory(num1, num2, result, OperationType.Multiplication);
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                AddToHistory(num1, num2, result, OperationType.Division);
                break;
            case "sqr":
                if (num1 >= 0)
                    result = Math.Sqrt(num1);
                writer.WriteValue("SquareRoot");
                AddToHistory(num1, num2, result, OperationType.SquareRoot);
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("TakingThePower");
                AddToHistory(num1, num2, result, OperationType.TakingThePower);
                break;
            case "x":
                result = num1 * Math.Pow(10, num2);
                writer.WriteValue("10x");
                AddToHistory(num1, num2, result, OperationType.x10);
                break;
            case "sin":
                result = num1 * Math.Sin(num2);
                writer.WriteValue("Sin");
                AddToHistory(num1, num2, result, OperationType.Sin);
                break;
            case "cos":
                result = num1 * Math.Cos(num2);
                writer.WriteValue("Cos");
                AddToHistory(num1, num2, result, OperationType.Cos);
                break;
            case "tan":
                result = num1 * Math.Tan(num2);
                writer.WriteValue("Tan");
                AddToHistory(num1, num2, result, OperationType.Tan);
                break;
            default:
                break;
        }
        AddOperation(calculationsAmount);
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public int AddOperation(int numberOfCalculations)
    {
        return numberOfCalculations++;
    }
    public static void AddToHistory(double num1, double num2, double result, OperationType type)
    {
        operations.Add(new Operation
        {
            Date = DateTime.Now,
            Number1 = num1,
            Number2 = num2,
            Result = result,
            Type = type
        });
    }
    //TODO: Allow the users to use the results in the list above to perform new calculations.
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WritePropertyName("CalculationsAmount");
        writer.WriteValue(calculationsAmount);
        writer.WriteEndObject();
        writer.Close();
    }
    public void ShowHistory()
    {
        Console.Clear();
        Console.WriteLine("Games History: ");
        foreach (var operation in operations)
        {
            Console.WriteLine($@"{operation.Date}
----------------
Type: {operation.Type} 
number1 = {operation.Number1}
number2 = {operation.Number2}
result = {operation.Result}
");
        }
    }
    public void ClearHistory()
    {
        operations.Clear();
    }

}