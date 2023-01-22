using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{
    public int TotalOperations { get; private set; } = 0;
    private List<Tuple<double, double, string?, double>> CachedOperations { get; set; } =
        new List<Tuple<double, double, string?, double>>();

    private JsonWriter Writer { get; set; }

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        Trace.AutoFlush = true;
        Writer = new JsonTextWriter(logFile);
        Writer.Formatting = Formatting.Indented;
        Writer.WriteStartObject();
        Writer.WritePropertyName("Operations");
        Writer.WriteStartArray();
    }

    public void Finish()
    {
        Writer.WriteEndArray();
        Writer.WriteEndObject();
        Writer.Close();
    }

    public double DoOperation(double num1, double num2, string? op)
    {
        IncrementOperations();

        double result;

        Writer.WriteStartObject();
        Writer.WritePropertyName("Operand1");
        Writer.WriteValue(num1);
        Writer.WritePropertyName("Operand2");
        Writer.WriteValue(num2);
        Writer.WritePropertyName("Operator");

        switch (op)
        {
            case "+":
                result = num1 + num2;
                Writer.WriteValue("Add");
                break;
            case "-":
                result = num1 - num2;
                Writer.WriteValue("Subtract");
                break;
            case "*":
                result = num1 * num2;
                Writer.WriteValue("Multiply");
                break;
            case "/":
                if (num2 == 0)
                {
                    throw new DivideByZeroException("Second Argument can not be zero when dividing.");
                }
                result = num1 / num2;
                Writer.WriteValue("Divide");
                break;
            default:
                result = double.NaN;
                Writer.WriteValue("Invalid Operator");
                break;
        }
        Writer.WritePropertyName("Result");
        Writer.WriteValue(result);
        Writer.WriteEndObject();

        CacheOperation(num1, num2, op, result);
        return result;

    }

    private void CacheOperation(double num1, double num2, string? op, double result)
    {
        CachedOperations.Add(Tuple.Create(num1, num2, op, result));
        if (CachedOperations.Count > 10)
        {
            CachedOperations.RemoveAt(0);
        }
    }

    private void IncrementOperations()
    {
        TotalOperations++;
    }

    public string ViewCache()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < CachedOperations.Count; i++)
        {
            var op = CachedOperations[i];
            sb.AppendLine($"{op.Item1} {op.Item3} {op.Item2} = {op.Item4} ({i + 1})");
        }
        return sb.ToString();

    }

    public void ClearCache()
    {
        CachedOperations.Clear();
    }

    public double GetPreviousResults(int idx)
    {
        return CachedOperations[idx].Item4;
    }
}