using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    public int Iterations = 1;

    List<History> history = new List<History>();

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
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        AddHistory(num1, num2, op, result);

        Iterations++;

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void AddHistory(double num1, double num2, string op, double result)
    {
        history.Add(new History
        {
            FirstOperand = num1,
            SecondOperand = num2,
            Operator = op,
            Sum = result
        });
    }

    public void DisplayHistory()
    {
        Console.Clear();
        Console.WriteLine("------ History ------");
        foreach (History calculation in history)
        {
            Console.WriteLine($"{calculation.FirstOperand} {OperatorSymbol(calculation.Operator)} {calculation.SecondOperand} = {calculation.Sum}");
        }
        Console.ReadLine();
    }

    private string OperatorSymbol(string op)
    {
        switch (op)
        {
            case "a":
                return "+";
            case "s":
                return "-";
            case "m":
                return "*";
            case "d":
                return "/";
            default:
                return op;
        }
    }
}