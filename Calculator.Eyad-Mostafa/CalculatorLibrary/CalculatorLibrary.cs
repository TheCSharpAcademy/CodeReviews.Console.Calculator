using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;

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

    public double DoTwoOperandsOperation(double num1, double num2, string op)
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
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "p":
                result = Math.Pow(num1,num2);
                writer.WriteValue("pow");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
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

    public double DoTrigonometryOperation(double num1, string op)
    {
        double result;
        writer.WriteStartObject();
        writer.WritePropertyName("Operation");
        switch (op)
        {
            case "sin":
                result = Math.Sin(num1);
                writer.WriteValue("Sin");
                break;
            case "cos":
                result = Math.Cos(num1);
                writer.WriteValue("Cos");
                break;
            case "tan":
                result = Math.Tan(num1);
                writer.WriteValue("Tan");
                break;
            // Return text for an incorrect option entry.
            default:
                result = 0;
                break;
        }
        writer.WritePropertyName("Operand");
        writer.WriteValue(num1);
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public double DoOneOperandOperation(double num1, string op)
    {
        double result;
        writer.WriteStartObject();
        writer.WritePropertyName("Operation");
        switch (op)
        {
            case "sqrt":
                result = Math.Sqrt(num1);
                writer.WriteValue("sqrt");
                break;
            case "10x":
                result = num1 * 10;
                writer.WriteValue("10x");
                break;
            // Return text for an incorrect option entry.
            default:
                result = 0;
                break;
        }
        writer.WritePropertyName("Operand");
        writer.WriteValue(num1);
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