using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CalculatorLibrary;

public class CalculatorData
{
    public int? Count { get; set; }
    public List<OperationItem>? Operations { get; set; }
}

public class OperationItem
{
    public double Operand1 { get; set; }
    public double Operand2 { get; set; }
    public string Operation { get; set; }
    public double Result { get; set; }
}

public class Calculator
{
    private CalculatorData? _data;
    private List<OperationItem> _operations = new List<OperationItem>(); 
    private int _usedCount = 0;
    
    public Calculator()
    {
        if (File.Exists("logger.json"))
        {
            var readFile = File.ReadAllText("logger.json");
            _data = JsonConvert.DeserializeObject<CalculatorData>(readFile);
            SetOperations();
        }
        
        var logFile = File.CreateText("logger.json");
        logFile.AutoFlush = true;
    }
    
    public double DoOperation(double num1, double num2, string op)
    {
        var result = double.NaN;
        var operationItem = new OperationItem { Operand1 = num1, Operand2 = num2 };
        
        switch (op)
        {
            case "a":
                result = num1 + num2;
                operationItem.Operation = "Add";
                break;
            case "s":
                result = num1 - num2;
                operationItem.Operation = "Subtract";
                break;
            case "m":
                result = num1 * num2;
                operationItem.Operation = "Multiply";
                break;
            case "d":
                if (num2 != 0)
                {
                    result = num1 / num2;
                    operationItem.Operation = "Divide";
                }
                break;
            default:
                break;
        }

        operationItem.Result = result;
        _operations.Add(operationItem);

        _usedCount++;
        
        return result;
    }

    public double GetNumberFromData()
    {
        ShowPreviousResults();
        Console.Write("Choose what result you want to reuse: ");

        int? index = null;
        while (index == null)
        {
            var tmp = 0;
            var isSuccesfulParse = int.TryParse(Console.ReadLine(), out tmp);

            if (isSuccesfulParse && tmp < _operations.Count && tmp >= 0)
            {
                index = tmp;
            }
            else
            {
                Console.Write("Write an index which is presented above");
            }
        }

        return _operations.ElementAt(index.Value).Result;
    }

    public void Finish()
    {
        var summaryCount = _usedCount + (_data?.Count ?? 0);
        var outputObject = new
        {
            Count = summaryCount,
            Operations = _operations
        };

        var jsonString = JsonSerializer.Serialize(outputObject);
        
        File.WriteAllText("logger.json", jsonString);
    }

    public int GetUsageCount()
    {
        return _usedCount + (_data?.Count ?? 0);
    }
    
    public int GetOperationsCount()
    {
        return _operations.Count;
    }

    public void DeleteData()
    {
        _operations.Clear();
        Finish();
        Console.Clear();
        Console.WriteLine("Calculations deleted");
    }

    public void ShowPreviousResults()
    {
        var index = 0;
        
        Console.WriteLine("Previous Results:");
        _operations.ForEach(operation =>
        {
            Console.WriteLine($"{index} - {operation.Result}");
            index++;
        });
    }

    private void SetOperations()
    {
        if (_data != null && _data.Operations != null)
        {
            _operations.AddRange(_data.Operations);
        }
    }
}