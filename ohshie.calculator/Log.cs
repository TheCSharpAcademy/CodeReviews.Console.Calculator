namespace ohshie.calculator;

public static class Log
{
    private static List<PreviousEquations> _logList = new List<PreviousEquations>();

    public static void WriteToLog(Calculator calculatorData)
    {
        PreviousEquations logEntry = new PreviousEquations()
        {
            Result = calculatorData.Result,
            NumberA = calculatorData.NumberA,
            NumberB = calculatorData.NumberB,
            MathOperator = calculatorData.MathOperator,
            OperationIndex = calculatorData.OperationIndex
        };
        
        _logList.Add(logEntry);
    }

    public static void ReadLog()
    {
        foreach (var entry in _logList)
        {
            Console.WriteLine($"{entry.OperationIndex}. {entry.NumberA}{entry.MathOperator}{entry.NumberB}=" +
                $"{entry.Result}");
        }
    }

    public static decimal ExtractResult(uint index)
    {
        foreach (var entry in _logList)
        {
            if (entry.OperationIndex == index)
            {
                return entry.Result;
            }
        }

        return 0;
    }

    public static void FlushLog()
    {
        string userChoice;
        Console.WriteLine("Do you really want to delete all entries from a previous equations list?\n" +
                          "Type \"yes\" and press enter to confirm\n" +
                          "Everything else will bring you back to main menu.");
        userChoice = Console.ReadLine().ToUpperInvariant();
        if (userChoice == "YES")
        {
            _logList = new List<PreviousEquations>();
        }
    }
    
    private class PreviousEquations
    {
        public int OperationIndex { get; set; }
        public string MathOperator { get; set; }
        public decimal NumberA { get; set; }
        public decimal NumberB { get; set; }
        public decimal Result { get; set; }
    }
}