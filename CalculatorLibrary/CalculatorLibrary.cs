namespace CalculatorLibrary;
using System.Text.Json;

public class CalcLib
{
    private const string _jsonFileName = "Calculations.json";
    private List<Calculation> _calculations = new();

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;
        double angle = Math.PI * num1 / 180.0;
        switch (op.Trim().ToLower())
        {
            case "a":
                result = num1 + num2;
                AddToList(num1, num2, result, "+");
                break;
            case "s":
                result = num1 - num2;
                AddToList(num1, num2, result, "-");
                break;
            case "m":
                result = num1 * num2;
                AddToList(num1, num2, result, "*");
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    AddToList(num1, num2, result, "/");
                }
                break;
            case "sq":
                result = Math.Sqrt(num1);
                AddToList(num1, 0.00f, result, "Square Root");
            break;
            case "p":
                result = Math.Pow(num1, num2);
                AddToList(num1, num2, result, "Power");
                break;
            case "sin":               
                result = Math.Sin(angle);
                AddToList(num1, num2, result, "Sine");
                break;
            case "cos":
                result = Math.Cos(angle);
                AddToList(num1, num2, result, "Cosine");
                break;
            case "tan":
                result = Math.Tan(angle);
                AddToList(num1, num2, result, "Tangent");
                break;
            default:
                break;
        }        
        return result;
    }

    private void AddToList(double num1, double num2, double result, string operation)
    {
        _calculations.Add(new Calculation(num1, operation, num2, result));
    }

    public async Task OpenFile()
    {
        if (File.Exists(_jsonFileName) && new FileInfo(_jsonFileName).Length != 0)
        {
            await using FileStream openStream = File.OpenRead(_jsonFileName);
            _calculations = await JsonSerializer.DeserializeAsync<List<Calculation>>(openStream);
            
        }        
    }

    public async Task SaveFile()
    {
        await using FileStream createStream  =File.Create(_jsonFileName);
        await JsonSerializer.SerializeAsync(createStream, _calculations);
    }
    
    public void ClearHistory()
    {
        _calculations.Clear();
        if (File.Exists(_jsonFileName)) File.Delete(_jsonFileName);
    }

    public IEnumerable<Calculation> PreviousCalculations => _calculations;

    public record Calculation(double Operand1, string Operation, double Operand2, double Result);
}