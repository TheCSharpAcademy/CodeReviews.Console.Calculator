namespace ConsoleUI;

public static class ConsoleMassages
{
    public static void AppWelcomeMessage()
    {
        for (int i = 3; i > 0; i--)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Welcome To Calculator App.");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("This App Built By Chernobyl.");
            Console.WriteLine("*******************************************");
            Console.WriteLine($"The Calculator is starting in {i} sec");
            Console.WriteLine("*******************************************");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
    public static void DisplayMainMenu()
    {
        Console.WriteLine("******************************************************");
        Console.WriteLine("---------------------- Main Menu ---------------------");
        Console.WriteLine("******************************************************");
        Console.WriteLine("+ -> x + y");
        Console.WriteLine("- -> x - y");
        Console.WriteLine("* -> x * y");
        Console.WriteLine("/ -> x / y");
        Console.WriteLine("% -> x % y");
        Console.WriteLine("^ -> x^y");
        Console.WriteLine("# -> Sqrt(x)");
        Console.WriteLine("! -> 10^(x)");
        Console.WriteLine("~ -> Sin(x)");
        Console.WriteLine("& -> Cos(x)");
        Console.WriteLine("$ -> Tan(x)");
        Console.WriteLine("H -> Display History");
        Console.WriteLine("R -> Pick History Result");
        Console.WriteLine("C -> Clear History");
        Console.WriteLine("-----------------------------------------------------");

    }
    public static void DisplayList(List<(double firstOperand, string op, double? secondOperand, double result)> items) {
        int counter = 1;
        foreach (var item in items)
        {
            if (item.secondOperand != null)
            {
                Console.WriteLine($"[{counter++}]: {item.firstOperand} {item.op} {item.secondOperand} = {item.result}");
            }
            else
            {
                Console.WriteLine( $"[{counter++}]: {item.op} = {item.result}");
            }
        }
    }

    
}
