using Newtonsoft.Json;

namespace Calculator.Tomi.CalculatorCore;

public class CalculatorEngine
{

    JsonWriter writer;
    public double PrevResult { get; private set; }
    public bool CanReuseResult { get; private set; } = false;

    public CalculatorEngine()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();

    }

    public double DoOperation(List<double> operands, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        string operation = string.Empty;

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = operands[0] + operands[1];
                operation = "add";
                break;
            case "s":
                result = operands[0] - operands[1];
                operation = "subtract";
                break;
            case "m":
                result = operands[0] * operands[1];
                operation = "multiply";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (operands[1] != 0)
                {
                    result = operands[0] / operands[1];
                    operation = "divide";
                }

                break;
            case "sq":
                result = operands[0] * operands[0];
                operation = "square";
                break;
            case "sqr":
                result = Math.Sqrt(operands[0]);
                operation = "squareroot";
                break;
            case "pow":
                result = Math.Pow(operands[0], operands[1]);
                operation = "power";
                break;
            default:
                break;
        }




        if (!string.IsNullOrEmpty(operation))
        {
            writer.WriteStartObject();

            for (int i = 0; i < operands.Count; i++)
            {
                writer.WritePropertyName($"Operand{i + 1}");
                writer.WriteValue(operands[i]);
            }
            writer.WritePropertyName("Operation");
            writer.WriteValue(operation);
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            if (!CanReuseResult) CanReuseResult = true;
            PrevResult = result;
        }

        return result;
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }


}
