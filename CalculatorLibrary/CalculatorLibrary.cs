namespace CalculatorLibrary;
using Newtonsoft.Json;

public class Calculator
{
    private readonly JsonWriter _writer;
    
    public Calculator()
    {
        var logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }
    
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.

        switch (op)
        {
            case "a":
                result = num1 + num2;
                _writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                _writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                _writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                _writer.WriteValue("Divide");
                break;
            case "r":
                if (num1 >= 0)
                {
                    result = Math.Sqrt(num1);
                }
                _writer.WriteValue("Square Root");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                _writer.WriteValue("Taking the Power");
                break;
            case "e":
                result = Math.Sin(num1);
                _writer.WriteValue("Sine");
                break;
            case "c":
                result = Math.Cos(num1);
                _writer.WriteValue("Cosine");
                break;
            case "t":
                result = Math.Tan(num1);
                _writer.WriteValue("Tangent");
                break;
        }

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();
        return result;
    }
    
    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
}
