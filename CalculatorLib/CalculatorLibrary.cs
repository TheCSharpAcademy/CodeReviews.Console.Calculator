using Newtonsoft.Json;

namespace CalculatorLibrary;

public class CalculatorLib
{
    private JsonWriter _jsonWriter;
    public CalculatorLib()
    {
        var logFile = File.CreateText("calclog.json");
        logFile.AutoFlush = true;

        _jsonWriter = new JsonTextWriter(logFile);
        _jsonWriter.Formatting = Formatting.Indented;
        
        _jsonWriter.WriteStartObject();
        _jsonWriter.WritePropertyName("Operations");
        _jsonWriter.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        var result = double.NaN;
        
        _jsonWriter.WriteStartObject();
        _jsonWriter.WritePropertyName("Operand1");
        _jsonWriter.WriteValue(num1);
        _jsonWriter.WritePropertyName("Operand2");
        _jsonWriter.WriteValue(num2);
        _jsonWriter.WritePropertyName("Operation");
        switch (op)
        {
            case "a":
                result = num1 + num2;
                _jsonWriter.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                _jsonWriter.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                _jsonWriter.WriteValue("Multiply");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                _jsonWriter.WriteValue("Divide");


                break;
        }

        _jsonWriter.WritePropertyName("Result");
        _jsonWriter.WriteValue(result);
        _jsonWriter.WriteEndObject();
        
        return result;
    }

    public void Finish()
    {
        _jsonWriter.WriteEndArray();
        _jsonWriter.WriteEndObject();
        _jsonWriter.Close();
    }
}