using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Utility
{
    public static List<decimal> inputNumList = new List<decimal>();
    public static List<decimal> userOptionList = new List<decimal>();

    public static List<decimal> ReadUserNumberInput()
    {
        string[] numOptions = { "First", "Second" };

        for (int i = 0; i < numOptions.Length; i++)
        {
            Console.Write($"Enter {numOptions[i]} number: ");
            NumericInputOnly(inputNumList);
        }
        return inputNumList;
    }
    internal static decimal ReadUserOptionInput()
    {
        Console.Write("Your option? ");
        NumericInputOnly(userOptionList);
        Console.WriteLine();

        return userOptionList[0];
    }
    internal static void NumericInputOnly(List<decimal> list)
    {
        string msg = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                if (char.IsDigit(key.KeyChar) || key.KeyChar == '.')
                {
                    if (key.KeyChar == '.' && msg.Contains("."))
                    {
                        continue;
                    }
                    else
                    {
                        msg += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
            }
            else if (key.Key == ConsoleKey.Backspace && msg.Length > 0)
            {
                msg = msg.Substring(0, (msg.Length - 1));
                Console.Write("\b \b");
            }
        }
        while (key.Key != ConsoleKey.Enter);

        if (!string.IsNullOrEmpty(msg))
        {
            if (decimal.TryParse(msg, out decimal val))
            {
                list.Add(val);
            }
        }
        Console.WriteLine();
    }
    internal static bool CalculatorLoop()
    {
        ConsoleKeyInfo key;
        key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
        {
            Console.WriteLine();
            Console.WriteLine();
            return true;
        }
        else if (key.Key == ConsoleKey.Delete)
        {
            ClearCalculator();
            return true;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine();
            Clear();
            return false;
        }
    }
    internal static void Clear()
    {
        inputNumList.Clear();
        userOptionList.Clear();
    }
    internal static void ClearCalculator()
    {
        Console.WriteLine();
        Console.WriteLine();
        Brain.total = 0;
        
        string clearJson = "[]";
        string filePath = "calculatorLog.json";
        File.WriteAllText(filePath, clearJson);
    }
}
internal class CalculationResult
{
    public decimal Operand1 { get; set; }
    public decimal Operand2 { get; set; }
    public string? Operation { get; set; }
    public decimal Result { get; set; }
    public DateTime Time { get; set; }

    internal static void SaveCalculationResult(CalculationResult calculationResult, out int num)
    {
        List<CalculationResult>? results;
        string filePath = "calculatorLog.json";

        if (File.Exists(filePath))
        {
            string current = File.ReadAllText(filePath);
            results = JsonConvert.DeserializeObject<List<CalculationResult>>(current);
        }
        else
        {
            results = new List<CalculationResult>();
        }

        results.Add(calculationResult);
        string json = JsonConvert.SerializeObject(results, Formatting.Indented);
        File.WriteAllText(filePath, json);

        num = results.Count;
    }
}


