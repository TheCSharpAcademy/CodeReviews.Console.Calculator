namespace CalculatorLibrary;

using Newtonsoft.Json;

public class Database
{
    private readonly string filename;
    private readonly CalculatorData data;

    public Database(string filename)
    {
        this.filename = filename;
        data = LoadFromFile();
    }

    public void Close()
    {
        SaveToFile();
    }

    public int GetUsageCount()
    {
        return data.UsageCount;
    }

    public void AddCalculation(double num1, double num2, string op, double result)
    {
        data.AddCalculation(new Calculation(num1, num2, op, result));
    }

    public List<Calculation> GetCalculations()
    {
        return data.Calculations ?? new List<Calculation>();
    }

    public void DeleteCalculations()
    {
        data.DeleteCalculations();
    }

    public void AddUsage()
    {
        data.UsageCount++;
    }

    private CalculatorData LoadFromFile()
    {
        if (!File.Exists(filename))
        {
            return new CalculatorData();
        }
        using StreamReader sr = new(filename);
        var json = sr.ReadToEnd();
        var deserializedJson = JsonConvert.DeserializeObject<CalculatorData>(json);
        if (deserializedJson == null)
        {
            return new CalculatorData();
        }
        return deserializedJson;
    }

    private void SaveToFile()
    {
        using StreamWriter file = File.CreateText(filename);
        var serializer = new JsonSerializer();
        serializer.Serialize(file, data);
    }
}