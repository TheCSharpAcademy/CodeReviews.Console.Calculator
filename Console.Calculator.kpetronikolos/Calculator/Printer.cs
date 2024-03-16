namespace CalculatorProgram;

public static class Printer
{
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
}
