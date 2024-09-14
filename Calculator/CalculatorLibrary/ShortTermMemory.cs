namespace CalculatorLibrary;

public static class ShortTermMemory
{
    public static List<string> ListWithResults { get; private set; } = new();

    public static void AddResult (string equation)
    {
        ListWithResults.Add(equation);
    }
    public static void ClearListWithResults()
    {
        ListWithResults.Clear();
    }
    public static void PrintResults()
    {
        for(int i = 0; i < ListWithResults.Count; i++)
        {
            Console.WriteLine("[" + (i + 1) + "] " + ListWithResults[i]);
        }
    }
    public static string GetResult(int index)
    {
        return ListWithResults[--index];
    }
}
