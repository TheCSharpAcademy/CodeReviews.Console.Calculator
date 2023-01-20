using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    public int CalculationsCompleted { get; private set; }
    public List<Calculation> CalculationMemory { get; private set; }
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
        CalculationMemory = new List<Calculation>();
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

        switch (op)
        {
            case "a":
                result = num1 + num2;
                StoreCalculation(num1, num2, op, result);
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                StoreCalculation(num1, num2, op, result);
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                StoreCalculation(num1, num2, op, result);
                writer.WriteValue("Multiply");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                StoreCalculation(num1, num2, op, result);
                writer.WriteValue("Divide");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                StoreCalculation(num1, num2, op, result);
                writer.WriteValue("Power");
                break;
            case "sin":
                result = Math.Sin(num1);
                StoreTrigCalculation(num1, num2, op, result);
                writer.WriteValue("Sine");
                break;
            case "cos":
                result = Math.Cos(num1);
                StoreTrigCalculation(num1, num2, op, result);
                writer.WriteValue("Cosine");
                break;
            case "tan":
                result = Math.Tan(num1);
                StoreTrigCalculation(num1, num2, op, result);
                writer.WriteValue("Tangent");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        CalculationsCompleted++;

        return result;
    }

    private void StoreCalculation(double num1, double num2, string op, double result)
    {
        if (!double.IsNaN(result))
        {
            CalculationMemory.Add(new Calculation(num1, num2, result, op));
        }
    }

    private void StoreTrigCalculation(double num1, double num2, string op, double result)
    {
        if (!double.IsNaN(result))
        {
            CalculationMemory.Add(new TrigCalculation(num1, num2, result, op));
        }
    }

    public void ClearHistory()
    {
        CalculationMemory.Clear();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}