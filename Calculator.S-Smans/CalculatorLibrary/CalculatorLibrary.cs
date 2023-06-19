using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    List<History> history = new();
    Symbols symbol = new();
    public int Iteration { get; set; }

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

        Iteration++;

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
            Operator = symbol.OperatorToSymbol(op),
            Sum = result
        });
    }

    public void History()
    {
        DisplayHistory();

        Console.WriteLine("\nd - Delete history");
        string input = Console.ReadLine();

        switch (input)
        {
            case "d":
                DeleteHistory();
                break;
        }
    }

    private void DisplayHistory()
    {
        Console.Clear();
        Console.WriteLine("------ History ------");

        int i = 1;
        foreach (History calculation in history)
        {
            Console.WriteLine($"{i}) {calculation.FirstOperand} {calculation.Operator} {calculation.SecondOperand} = {calculation.Sum}");
            i++;
        }
    }

    private void DeleteHistory()
    {
        history.Clear();
        Console.Clear();
        Console.WriteLine("History deleted!");
        Console.ReadLine();
    }

    public double PreviousResult()
    {
        DisplayHistory();
        Console.WriteLine("\nPick one of the numbers in the ordered list.\nThe sum will be used as an operand.");

        double sum = 0;
        string input = Console.ReadLine();

        int cleanNum = 0;
        while (int.TryParse(input, out cleanNum) && cleanNum > history.Count)
        {
            Console.WriteLine("Pick a number from the ordered list!");
            input = Console.ReadLine();
        }

        // Returns users requested sum from the history
        return history[cleanNum - 1].Sum;
    }

    public int GetHistoryCount()
    {
        return history.Count;
    }
}