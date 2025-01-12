namespace KamilKolanowski.Calculator;

public class CalculatorApp
{
    public void Calc()
    {
        // Calculator Menu 
        Console.WriteLine("Welcome to the calculator!");
        Console.WriteLine("Please specify which operation you want to perform");
        Console.WriteLine("\t a -> Add");
        Console.WriteLine("\t s -> Subtract");
        Console.WriteLine("\t m -> Multiply");
        Console.WriteLine("\t d -> Divide");
        //////////////////////////////////////
        Console.Write($"Your choice: ");
        string operation = Console.ReadLine();

        var res = new GetResult();
        res.GetResultFromOperation(operation);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}