using Newtonsoft.Json;

public class CalculationData
{
    public List<Calculation> calculations = new();
    public int numCalculatorLaunches;
    public string filePath = "calculationdata.json";

    public void Init()
    {
        this.ReadFromFile();
        this.numCalculatorLaunches++;
        this.WriteToFile(this);
    }

    public void DeleteCalculationHistory()
    {
        this.calculations.Clear();
        this.WriteToFile(this);
    }

    public void AddCalculation(Calculation calculation)
    {
        this.calculations.Add(calculation);
        this.WriteToFile(this);
    }

    public void WriteToFile(CalculationData data)
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }
        catch
        {
            Console.WriteLine("Failed to save calculator data");
        }
    }

    public void ReadFromFile()
    {
        try
        {
            if (!File.Exists(this.filePath))
            {
                return;
            }

            string jsonString = File.ReadAllText(filePath);

            if (jsonString == null)
            {
                throw new Exception("Could not read JSON file");
            }

            CalculationData? calculationData = JsonConvert.DeserializeObject<CalculationData>(jsonString);

            if (calculationData == null)
            {
                throw new Exception("Could not read JSON file");
            }

            calculations = calculationData.calculations;
            numCalculatorLaunches = calculationData.numCalculatorLaunches;
        }
        catch
        {
            Console.WriteLine("Loading save file failed");
        }
    }
}