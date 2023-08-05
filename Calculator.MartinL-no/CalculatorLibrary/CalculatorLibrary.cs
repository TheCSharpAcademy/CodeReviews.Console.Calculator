namespace CalculatorLibrary;

public class Calculator
{
    private OperationRepository _operationsRepository;
    private readonly string _alphabet;

    public Calculator()
    {
        _operationsRepository = new OperationRepository();
        _alphabet = "abcdefghijklmnopqrstuvwxyz";
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        string operation = "";

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                operation = "Add";
                break;
            case "s":
                result = num1 - num2;
                operation = "Subtract";

                break;
            case "m":
                result = num1 * num2;
                operation = "Multiply";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                operation = "Divide";
                break;
            case "e":
                result = Math.Pow(num1, num2);
                operation = "Exponent";
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }

        var operationRecord = new OperationRecord(num1, num2, result, operation);
        _operationsRepository.Create(operationRecord);
        return result;
    }

    public double DoOperation(double num, string op)
    {
        double result = double.NaN;
        string operation = "";

        switch (op)
        {
            case "r":
                result = Math.Sqrt(num);
                operation = "Square Root";
                break;
            case "si":
                result = Math.Sin(num);
                operation = "Sine";
                break;
            case "co":
                result = Math.Cos(num);
                operation = "Cosine";
                break;
            case "ta":
                result = Math.Tan(num);
                operation = "Tangent";
                break;
        }

        var operationRecord = new OperationRecord(num, result, operation);
        _operationsRepository.Create(operationRecord);
        return result;
    }

    public bool IsPreviousOperations()
    {
        if (_operationsRepository.All().Count > 0) return true;

        return false;
    }

    public bool GetValidInput(string input, out double validOutput)
    {
        if (double.TryParse(input, out validOutput)) return true;
        else if (input.Length == 0 || input.Length > 1 || !_alphabet.Contains(input)) return false;

        var operations = _operationsRepository.All();
        var index = _alphabet.IndexOf(input);

        if (index > operations.Count - 1)
        {
            return false;
        }
        else
        {
            validOutput = operations[index].Result;
            return true;
        };
    }

    public List<string> GetPreviousOperations()
    {
        var operations = _operationsRepository.All();
        var output = new List<string>();

        for (int i = 0; i < operations.Count; i++)
        {
            output.Add($"{_alphabet[i]}: {operations[i]}");
        }

        return output;
    }

    public void ClearOperations()
    {
        _operationsRepository.DeleteAll();
    }
}
