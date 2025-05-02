// CalculatorLibrary.cs
using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{
    private int timesCalcWasUsed;

    JsonWriter writer;

    List<Operation> latestCalculations = new();

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
        Operator typeOp = Operator.Addition;
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
                typeOp = Operator.Addition;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                typeOp = Operator.Subtraction;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                typeOp = Operator.Multiplication;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    typeOp = Operator.Division;
                }
                writer.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        timesCalcWasUsed++;
        AddLatestCalculation(num1, num2, typeOp, result);

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public int GetTimesCalcWasUsed()
    {
        return timesCalcWasUsed;
    }

    public void AddLatestCalculation(double n1, double n2, Operator op, double res)
    {
        latestCalculations.Add(new Operation
        {
            Num1 = n1,
            Num2 = n2,
            Operator = op,
            Result = res
        });
    }

    public List<Operation> GetLatestCalculations()
    {
        return latestCalculations;
    }

    public void DeleteList()
    {
        latestCalculations.Clear();
    }
}