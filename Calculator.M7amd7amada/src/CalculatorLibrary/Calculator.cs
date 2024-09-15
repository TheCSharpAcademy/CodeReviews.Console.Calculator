using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private readonly JsonWriter _writer;
    private static int s_usedTimes;
    private static readonly List<OperationModel> Operations = [];

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile)
        {
            Formatting = Formatting.Indented
        };
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        s_usedTimes++;
        double result = double.NaN;
        OperationModel operation = new()
        {
            Operand1 = num1,
            Operand2 = num2,
            Operation = op
        };
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");
        switch (op)
        {
            case "a":
                result = num1 + num2;
                operation.Result = result;
                Operations.Add(operation);
                _writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                operation.Result = result;
                Operations.Add(operation);
                _writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                operation.Result = result;
                Operations.Add(operation);
                _writer.WriteValue("Multiply");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                operation.Result = result;
                Operations.Add(operation);
                _writer.WriteValue("Divide");
                break;
            default:
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

    public static int GetUsedTimes()
    {
        return s_usedTimes;
    }

    public static List<OperationModel> GetOperations()
    {
        return Operations;
    }

    public static void ClearOperations()
    {
        Operations.Clear();
    }


    public double UseStoredOperation(int operationIndex, double newOperand)
    {
        if (operationIndex < 0 || operationIndex >= Operations.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(operationIndex), "Invalid operation index.");
        }

        var storedOperation = Operations[operationIndex];
        double result;
        switch (storedOperation.Operation)
        {
            case "Add":
                result = storedOperation.Result + newOperand;
                break;
            case "Subtract":
                result = storedOperation.Result - newOperand;
                break;
            case "Multiply":
                result = storedOperation.Result * newOperand;
                break;
            case "Divide":
                if (newOperand != 0)
                {
                    result = storedOperation.Result / newOperand;
                }
                else
                {
                    return double.NaN;
                }
                break;
            default:
                return double.NaN;
        }

        var newOperation = new OperationModel
        {
            Operand1 = storedOperation.Result,
            Operand2 = newOperand,
            Operation = storedOperation.Operation,
            Result = result
        };

        Operations.Add(newOperation);

        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(storedOperation.Result);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(newOperand);
        _writer.WritePropertyName("Operation");
        _writer.WriteValue(storedOperation.Operation);
        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        return result;
    }

}