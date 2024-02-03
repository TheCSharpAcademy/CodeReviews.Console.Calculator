using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
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
    
    public double DoOperation(double num, string op)
    {
        double result = double.NaN;
        writer.WriteStartObject();
        writer.WritePropertyName("Operand");
        writer.WriteValue(num);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case "sr":
                result = Math.Sqrt(num);
                writer.WriteValue("Square root");
                break;
            case "t":
                result = num * 10;
                writer.WriteValue("10x");
                break;
            case "sin":
                result = Math.Sin(num);
                writer.WriteValue("Sin");
                break;
            case "cos":
                result = Math.Cos(num);
                writer.WriteValue("Cos");
                break;
            case "tan":
                result = Math.Tan(num);
                writer.WriteValue("Tan");
                break;
            case "ctan":
                result = 1 / Math.Tan(num);
                writer.WriteValue("Ctan");
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
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
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
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
        writer.WriteEndObject();
        writer.Close();
    }
}