namespace CalculatorLibrary;

public class Calculator
{
    public List<string> results = new List<string> { };

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;      
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;    
       
        switch (op)
        {
            case "a":
                result = num1 + num2;              
                break;
            case "s":
                result = num1 - num2;                
                break;
            case "m":
                result = num1 * num2;               
                break;
            case "d":               
                if (num2 != 0)
                {
                    result = num1 / num2;
                }               
                break;          
            default:
                break;
        }      
        AddToHistory(result.ToString());
        return result;
    }

    void AddToHistory(string result)
    {
        results.Add(result);
    }

    public void PrintResults()
    {
        Console.Clear();
        Console.WriteLine("Results");
        Console.WriteLine("-----------------------");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {results[i]}");
        }
        Console.WriteLine("-----------------------\n");
    }

    public void ClearResults()
    {
        results.Clear();
        Console.WriteLine("Results cleared.");
    }
    
}