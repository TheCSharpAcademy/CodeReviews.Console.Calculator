using Newtonsoft.Json;
namespace CalculatorLibrary;

public class JsonParse
{
    public class JSONArray
    {
        public double Operand1;
        public double Operand2;
        public string OperationType;
        public double Result;
    }
    public class JSONRoot()
    {
        public List<JSONArray> Operations;
        public int Usage;
    }
    public class Calculations()
    {
        public double firstNumber { get; set; }
        public double secondNumber { get; set; }
        public string OperationType { get; set; }
        public double result { get; set; }
    }
    public int GetCalculatorUsageStats()
    {
        int result = 0; ;
        string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
        string json = File.ReadAllText(path);
        JSONRoot root = JsonConvert.DeserializeObject<JSONRoot>(json);
        result = root.Usage;
        return result;
    }
    public List<Calculations> GetCalculationHistory()
    {
        List<Calculations> previousCalculations = new();

        string path = Path.Combine(Environment.CurrentDirectory, "calculator.json");
        string json = File.ReadAllText(path);
        JSONRoot root = JsonConvert.DeserializeObject<JSONRoot>(json);

        foreach (var operations in root.Operations)
        {
            previousCalculations.Add(new Calculations { firstNumber = operations.Operand1, secondNumber = operations.Operand2, OperationType = operations.OperationType, result = operations.Result });
        }
        return previousCalculations;
    }
}
