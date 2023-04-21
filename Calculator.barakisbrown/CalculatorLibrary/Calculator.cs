namespace CalculatorLibrary;

using Newtonsoft.Json;

public class Calculator
{
    private readonly JsonWriter writer;
    private int Counter;

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
                writer.WriteValue("Subtraction");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                    
                }
                writer.WriteValue("Divide");
                break;
            case "p":
                writer.WriteValue("Power");
                result = Math.Pow(num1, num2);
                break;
            case "q":
                writer.WriteValue("Squart Root");
                result = Math.Sqrt(num1);
                break;
            case "x":
                writer.WriteValue("10X");
                result = Math.Pow(10,2);
                break;
            case "i":
                writer.WriteValue("SIN");
                result = Math.Sin(num1);
                break;
            case "c":
                writer.WriteValue("COS");
                result = Math.Cos(num1);
                break;
            case "t":
                writer.WriteValue("TAN");
                result = Math.Tan(num1);
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
        Counter++;
        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public int GetCount() => Counter;
}