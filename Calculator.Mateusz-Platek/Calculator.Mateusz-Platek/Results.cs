namespace CalculatorProgram;

public static class Results
{
    public static List<double> results
    {
         get;
    } = new List<double>();

    public static void Add(double value)
    {
        results.Add(value);
    }

    public static void Clear()
    {
        results.Clear();
    }
    
    public static double GetResultFromList()
    {
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            if (index < 0 || index >= results.Count)
            {
                return 0;
            }
            return results.ElementAt(index);
        }

        Console.WriteLine("Wrong input");
        return 0;
    }
    
    public static void PrintResults()
    {
        Console.WriteLine("Results:");
        for (int i = 0; i < results.Count; i++)
        {
            Console.WriteLine($"index: {i} result: {results.ElementAt(i)}");
        }
    }
}