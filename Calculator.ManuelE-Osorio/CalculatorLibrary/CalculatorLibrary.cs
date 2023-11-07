using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{

    JsonWriter writer;
    int CalculatorUses;
    public Calculator()
    {
        CalculatorUses = 0;
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
        CalculatorUses++;
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
            case "sqrt":
                if (num2 >= 0)
                {
                    result = num1*Math.Sqrt(num2);
                }
                writer.WriteValue("Square Root");
                break;
            case "pow":
                result = Math.Pow(num1,num2);
                writer.WriteValue("Power");
                break;
            case "10x":
                result = num1*Math.Pow(10,num2);
                writer.WriteValue("10x");
                break;
            case "cos":
                result = num1*Math.Cos(num2);
                writer.WriteValue("Cosine");
                break;
            case "sin":
                result = num1*Math.Sin(num2);
                writer.WriteValue("Sine");
                break;
            default:
                writer.WriteValue("Undefined");
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
