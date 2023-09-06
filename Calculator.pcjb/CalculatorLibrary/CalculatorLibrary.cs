namespace CalculatorLibrary;

using Newtonsoft.Json;

public class Calculator
{
    private readonly JsonWriter writer;
    public List<Operation> Operations { get; }
    private Operation? activeOperation;

    public Calculator()
    {
        Operations = BuildOperationsList();
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public bool SetActiveOperation(string? shortcut)
    {
        if (shortcut == null || Operations == null)
        {
            return false;
        }

        foreach (var op in Operations)
        {
            if (op.Shortcut.Equals(shortcut))
            {
                activeOperation = op;
                return true;
            }
        }
        return false;
    }

    public string GetActiveOperationShortcut()
    {
        if (activeOperation != null)
        {
            return activeOperation.Shortcut;
        }
        return "";
    }

    public bool OperationRequiresTwoNumbers()
    {
        return activeOperation != null && activeOperation.ParamCount == 2;
    }

    public double DoOperation(double num1, double num2)
    {
        string operationName = "";
        double result = double.NaN;
        if (activeOperation != null)
        {
            operationName = activeOperation.Name;
            result = activeOperation.GetResult(new double[] { num1, num2 });
        }

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        writer.WriteValue(operationName);
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

    public string Format(Calculation calculation)
    {
        foreach (var op in Operations)
        {
            if (op.Shortcut.Equals(calculation.Op))
            {
                return String.Format("{0} = {1}", op.Format(new double[] { calculation.Num1, calculation.Num2 }), calculation.Result);
            }
        }
        throw new InvalidOperationException($"Unknown operation shortcut {calculation.Op}");
    }

    private static List<Operation> BuildOperationsList()
    {
        return new()
        {
            new OperationAdd(),
            new OperationSubtract(),
            new OperationMultiply(),
            new OperationDivide(),
            new OperationSquareRoot(),
            new OperationPower(),
            new Operation10x(),
            new OperationSine(),
            new OperationCosine(),
            new OperationTangent()
        };
    }
}