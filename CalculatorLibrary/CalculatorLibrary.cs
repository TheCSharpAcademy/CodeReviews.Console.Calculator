using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private readonly List<CalculatorHistory> history;
    private readonly JsonWriter writer;

    public Calculator()
    {
        history = new List<CalculatorHistory>();
        var logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public void DisplayHistory()
    {
        Console.WriteLine($"You have entered {history.Count} valid operation(s).");

        for (var i = 0; i < history.Count; i++)
            history[i].DisplayResult(i);

        Console.WriteLine();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        var
            result = double
                .NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.

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
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0) result = num1 / num2;

                writer.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        if (!double.IsNaN(result))
            history.Add(new CalculatorHistory(num1, num2, op, result));

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}

public class CalculatorHistory
{
    private readonly double num1;
    private readonly double num2;
    private readonly string op;
    private readonly string opString;
    private readonly double result;

    public CalculatorHistory(double num1, double num2, string op, double result)
    {
        this.num1 = num1;
        this.num2 = num2;
        this.op = op;
        this.result = result;

        opString = op switch
        {
            "a" => "+",
            "d" => "/",
            "m" => "*",
            "s" => "-",
            _ => ""
        };
    }

    public void DisplayResult(int accessor)
    {
        Console.WriteLine($"[{accessor}] {num1} {opString} {num2} = {result}");
    }
}