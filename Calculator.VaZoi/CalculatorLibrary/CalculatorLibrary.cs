using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private List<string> allAnswers = new List<string>();
    int calculatorUsedCount = 0;
    private JsonWriter writer;

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
        string answer = "";
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
                answer = $"{num1} + {num2} = {result}";
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                answer = $"{num1} - {num2} = {result}";
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                answer = $"{num1} * {num2} = {result}";
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                answer = $"{num1} / {num2} = {result}";
                break;
            default:
                break;
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        allAnswers.Add(answer);
        calculatorUsedCount++;

        return result;
    }

    public int GetCalculatorUsedCount()
    {
        return calculatorUsedCount;
    }

    public string GetHistoryCalculations()
    {
        if (allAnswers.Count > 0)
        {
            return string.Join("\n", allAnswers);
        }
        else
        {
            return "No calculations yet.";
        }
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
