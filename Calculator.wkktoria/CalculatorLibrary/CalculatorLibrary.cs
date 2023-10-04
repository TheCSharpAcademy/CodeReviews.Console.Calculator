using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private readonly JsonWriter _writer;
    private static double _lastResult = double.NaN;
    private static int _timesUsed = 0;
    private static List<string> _latestCalculations = new List<string>();
    private static readonly Dictionary<string, string> Operations = new Dictionary<string, string>
    {
        
        {"add", "Add the first number to the second number"},
        {"subtract", "Subtract second number from the first number"},
        {"multiply", "Multiply numbers"},
        {"divide", "Divide first number by second number"},
        {"sqr", "Square root of the number"},
        {"pow", "Raise the first number to the power of second number"},
        {"10x", "Number times 10"},
        {"sin", "Sine of the number"},
        {"cos", "Cosine of the number"},
        {"times", "Show the amount of times the calculator was used"},
        {"print", "Print the latest calculations"},
        {"delete", "Delete the latest calculations"}
    };

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
        var result = double.NaN;

        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");

        switch (op)
        {
            case "add":
                result = num1 + num2;
                _latestCalculations.Add($"{num1} + {num2} = {result}");
                _writer.WriteValue("Add");
                break;
            case "subtract":
                result = num1 - num2;
                _latestCalculations.Add($"{num1} - {num2} = {result}");
                _writer.WriteValue("Subtract");
                break;
            case "multiply":
                result = num1 * num2;
                _latestCalculations.Add($"{num1} * {num2} = {result}");
                _writer.WriteValue("Multiply");
                break;
            case "divide":
                if (num2 != 0) result = num1 / num2;
                _latestCalculations.Add($"{num1} - {num2} = {result}");
                _writer.WriteValue("Divide");
                break;
            case "sqr":
                result = Math.Sqrt(num1);
                _latestCalculations.Add($"\u221a{num1} = {result}");
                _writer.WriteValue("Square root");
                break;
            case "pow":
                result = Math.Pow(num1, num2);
                _latestCalculations.Add($"{num1}^{num2} = {result}");
                _writer.WriteValue("Power");
                break;
            case "10x":
                result = num1 * 10;
                _latestCalculations.Add($"{num1} x 10 = {result}");
                _writer.WriteValue("10x");
                break;
            case "sin":
                result = Math.Sin(num1);
                _latestCalculations.Add($"sin({num1}) = {result}");
                _writer.WriteValue("Sine");
                break;
            case "cos":
                result = Math.Cos(num1);
                _latestCalculations.Add($"cos({num1}) = {result}");
                _writer.WriteValue("Cosine");
                break;
        }

        _timesUsed++;
        _lastResult = result;

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        return result;
    }

    public void PrintAvailableOperations()
    {
        foreach (var operation in Operations)
        {
            Console.WriteLine($"\t- {operation.Key.Substring(0,3)} - {operation.Value}");
        }
    }

    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }

    public bool IsValidOperation(string operation)
    {
        var keys = new List<string>(Operations.Keys);

        return keys.Any(key => key.Contains(operation.ToLower()));
    }
    
    public static int GetTimesUsed()
    {
        return _timesUsed;
    }

    public static double GetLastResult()
    {
        return _lastResult;
    }

    public static Dictionary<string, string>.KeyCollection GetOperationsKeys()
    {
        return Operations.Keys;
    }

    public static void PrintLatestCalculations()
    {
        if (_latestCalculations.Any())
        {
            foreach (var calculation in _latestCalculations)
            {
                Console.WriteLine($"- {calculation}");
            }
        }
        else
        {
         Console.WriteLine("There aren't any latest calculations...");   
        }
    }

    public static void DeleteLatestCalculations()
    {
        if (_latestCalculations.Any())
        {
            _lastResult = double.NaN;
            _latestCalculations = new List<string>();
            Console.WriteLine("Latest calculations has been deleted.");
        }
        else
        {
         Console.WriteLine("There is nothing to delete.");   
        }
    }
}