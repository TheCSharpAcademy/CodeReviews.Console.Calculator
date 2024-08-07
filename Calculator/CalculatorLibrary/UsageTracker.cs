namespace CalculatorLibrary;

public class UsageTracker
{

    private const string FILE_NUMBER_USES = "numberUses.txt";
    private int _numberUsesSession;

    public UsageTracker()
    {
        _numberUsesSession = 0;
    }

    public int GetNumberUses()
    {
        if (!File.Exists(FILE_NUMBER_USES))
        {
            using (var fs = File.Create(FILE_NUMBER_USES))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine("0");
                }
            }
        }

        try
        {
            string[] numberUsesLines = File.ReadAllLines(FILE_NUMBER_USES);
            if (numberUsesLines.Length > 0 && int.TryParse(numberUsesLines[0].Trim(), out int linesFile))
            {
                return linesFile + _numberUsesSession;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading from file: {ex.Message}");
        }


        return _numberUsesSession;
    }

    public void SetNumberUses()
    {
        int numberUses = GetNumberUses();
        try
        {
            File.WriteAllText(FILE_NUMBER_USES, numberUses.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to a file: {ex.Message}");
        }
    }

    public void IncrementNumberUses()
    {
        _numberUsesSession++;
    }
}
