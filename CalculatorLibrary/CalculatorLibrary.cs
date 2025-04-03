using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{
    JsonWriter writer;
    int numUsages;
    List<double> resultsHistory;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();

        // Initialize variables
        numUsages = 0;
        resultsHistory = new List<double>();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        numUsages++;

        double result = Double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
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
                resultsHistory.Add(result);
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                resultsHistory.Add(result);
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                resultsHistory.Add(result);
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    resultsHistory.Add(result);
                }
                writer.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
            case "p":
                result = Math.Pow(num1, num2);
                resultsHistory.Add(result);
                writer.WriteValue("Power");
                break;
            default:
                break;
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public double DoSingleInputOperation(double num, string op)
    {
        numUsages++;

        double result = Double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand");
        writer.WriteValue(num);
        writer.WritePropertyName("Operation");

        // Use a switch statement to do the math.
        switch (op)
        {
            case "q":
                result = Math.Sqrt(num);
                resultsHistory.Add(result);
                writer.WriteValue("Square Root");
                break;
            case "t":
                result = num * 10;
                resultsHistory.Add(result);
                writer.WriteValue("10x");
                break;
            case "i":
                result = Math.Sin(num);
                resultsHistory.Add(result);
                writer.WriteValue("Sin");
                break;
            case "o":
                result = Math.Cos(num);
                resultsHistory.Add(result);
                writer.WriteValue("Cos");
                break;
            case "n":
                result = Math.Tan(num);
                resultsHistory.Add(result);
                writer.WriteValue("Tan");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }

        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();

        writer.WritePropertyName("Number of usages");
        writer.WriteValue(numUsages);

        writer.WritePropertyName("History");
        writer.WriteStartArray();
        foreach (double res in resultsHistory)
        {
            writer.WriteValue(res);
        }
        writer.WriteEndArray();


        writer.WriteEndObject();
        writer.Close();
    }

    public void ClearHistory()
    {
        resultsHistory = new List<double>();
    }

    public List<double> GetResultHistory()
    {
        return resultsHistory;
    }
}
