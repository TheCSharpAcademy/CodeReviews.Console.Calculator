using Newtonsoft.Json;
namespace CalculatorLibrary;

public class JsonParse
{
    public class JsonArray
    {
        public double Operand1;
        public double Operand2;
        public string OperationType;
        public double Result;
    }
    public class JsonRoot()
    {
        public List<JsonArray> Operations;
        public int Usage;
    }
    public class Calculations()
    {
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public string OperationType { get; set; }
        public double Result { get; set; }
    }
    public int GetCalculatorUsageStats()
    {
        int result = 0; ;
        string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
        string json = File.ReadAllText(path);
        JsonRoot root = JsonConvert.DeserializeObject<JsonRoot>(json);
        result = root.Usage;
        return result;
    }
    public List<Calculations> GetCalculationHistory()
    {
        List<Calculations> previousCalculations = new();

        string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
        string json = File.ReadAllText(path);
        JsonRoot root = JsonConvert.DeserializeObject<JsonRoot>(json);

        foreach (var operations in root.Operations)
        {
            previousCalculations.Add(new Calculations { FirstNumber = operations.Operand1, SecondNumber = operations.Operand2, OperationType = operations.OperationType, Result = operations.Result });
        }
        return previousCalculations;
    }
}
