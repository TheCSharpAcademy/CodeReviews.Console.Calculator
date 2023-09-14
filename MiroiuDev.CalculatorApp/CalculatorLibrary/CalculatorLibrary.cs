using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    public int NumberOfCalculations { get; set; };
    private readonly List<Calculation> _history = new();

    private readonly JsonWriter _writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.log");
        logFile.AutoFlush = true;

        _writer = new JsonTextWriter(logFile)
        {
            Formatting = Formatting.Indented
        };
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }

    public bool HasHistory()
    {
        return _history.Count > 0;
    }

    public void PrintHistory()
    {
        foreach (var calculation in _history.OrderByDescending(x => x.CalculatedAt))
        {
            Console.WriteLine(calculation);
        }
    }

    public void ClearHistory()
    {
        _history.Clear();
    }

    public double BinaryOperation(double operand1, double operand2, string? operation)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(operand1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(operand2);
        _writer.WritePropertyName("Operation");

        switch (operation)
        {
            case "a":
                result = operand1 + operand2;
                _writer.WriteValue("Add");

                break;
            case "s":
                result = operand1 - operand2;
                Trace.WriteLine($"{operand1} - {operand2} = {result}");
                _writer.WriteValue("Subtract");

                break;
            case "m":
                _writer.WriteValue("Multiply");
                result = operand1 * operand2;
                break;
            case "d":
                while (operand2 == 0)
                {
                    Console.Write("Number must not be equal to 0. Please try again: ");
                    operand2 = Helpers.GetNumber();
                }

                result = operand1 / operand2;
                _writer.WriteValue("Divide");

                break;
            case "p":
                result = Math.Pow(operand1, operand2);
                _writer.WriteValue("Power");

                break;
            default:
                throw new Exception("Invalid operation.");
        }

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        ++NumberOfCalculations;

        _history.Add(new Calculation
        {
            CalculatedAt = DateTime.Now,
            Operand1 = operand1,
            Operand2 = operand2,
            Operation = operation,
            Type = OperationType.Binary,
            Result = result
        });

        return result;
    }

    public double UnaryOperation(double num, string? operation)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        _writer.WriteStartObject();
        _writer.WritePropertyName("Value");
        _writer.WriteValue(num);
        _writer.WritePropertyName("Operation");

        switch (operation)
        {
            case "l":
                result = Math.Sin(num);
                _writer.WriteValue("Sinus");

                break;
            case "k":
                result = Math.Cos(num);
                _writer.WriteValue("Cosinus");

                break;
            case "r":
                result = Math.Sqrt(num);
                _writer.WriteValue("Square Root");

                break;
            default:
                throw new Exception("Invalid operation.");
        }

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        ++NumberOfCalculations;

        _history.Add(new Calculation
        {
            CalculatedAt = DateTime.Now,
            Operation = operation,
            Value = num,
            Type = OperationType.Unary,
            Result = result
        });


        return result;
    }
    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
}
