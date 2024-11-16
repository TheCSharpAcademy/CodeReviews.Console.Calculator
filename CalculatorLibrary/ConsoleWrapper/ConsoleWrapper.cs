namespace CalculatorLibrary.ConsoleWrapper;

public class ConsoleWrapper : IConsoleWrapper
{
    public ConsoleKeyInfo ReadKey(bool intercept)
    {
        return Console.ReadKey(intercept);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}