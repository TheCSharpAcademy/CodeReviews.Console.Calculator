namespace CalculatorProgram;

public static class Printer
{
    public static void PrintWelcomeMessage()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
    }

    public static void PrintCalculatorCount(int calculatorCount)
    {
        if (calculatorCount == 0)
        {
            Console.WriteLine("The calculator not been used yet.");
        }
        else
        {
            Console.WriteLine($"The calculator has been used {calculatorCount} times.");
        }
    }

    public static void PrintResult(double result)
    {
        Console.WriteLine("Your result: {0:0.##}\n", result);
    }
}
