using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    public List<double> history = new List<double>();
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
                    writer.WriteValue("Divide");
                }
                break;
            default:
                break;
        }
        if (result != double.NaN)
        {
            history.Add(result);
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        return result;
    }

    public void PrintResultsHistory(List<double> history)
    {
        Console.WriteLine("Results history");
        Console.WriteLine("--------------------");
        foreach (var item in history)
        {
            Console.WriteLine($"{history.IndexOf(item) + 1}. result -> {item}");
        }
        Console.WriteLine("--------------------");
    }

    public void DeleteResultsHistory(List<double> history)
    {
        history.Clear();
        Console.WriteLine("Results history was cleared! Press any key to continue...");
        Console.ReadKey();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}

