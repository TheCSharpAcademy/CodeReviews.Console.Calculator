namespace CalculatorLibrary;

public class Menu
{
    public static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------");
    }
    public static void DisplayOptionMenu()
    {
        Console.WriteLine("\nChoose an option from the following list:");
        Console.WriteLine("\t1 - Add");
        Console.WriteLine("\t2 - Subtract");
        Console.WriteLine("\t3 - Multiply");
        Console.WriteLine("\t4 - Divide");
        Console.WriteLine("\t5 - Exponential");
        Console.WriteLine("\t6 - Square Root");

        Brain.Calculate();
    }
    internal static string DisplayCalculationOutcome(decimal x, decimal y, string op, decimal outcome)
    {
        return $"Result: {x} {op} {y} = {outcome}";
    }
    public static bool DisplayAgainOption()
    {
        Console.Write("Please 'Esc' to exit, 'Del' to exit and clear OR any other key to continue: ");
        return Utility.CalculatorLoop();
    }
    public static void DisplayUseCount()
    {
        Console.WriteLine($"Calculator current use count: {Brain.count}");
        Console.WriteLine($"Calculator total use count: {Brain.total}\n");
    }
}
