namespace CalculatorLibrary;

public static class Helpers
{
    public static double GetNumber()
    {
        Console.Write("Enter number, or type `last` to use last result: ");
        var num = Console.ReadLine();
        
        while (!double.TryParse(num, out _) || num == "last")
        {
            var lastResult = Calculator.GetLastResult();

            if (!double.IsNaN(lastResult))
            {
                return lastResult;
            }
            else
            {
                Console.WriteLine("There is no last result.");
            }

            Console.Write("This is not valid input. Please enter an integer value: ");
            num = Console.ReadLine();
        }

        return double.Parse(num);
    }
    
    public static string ParseOperation(string op)
    {
        foreach (var key in Calculator.GetOperationsKeys().Where(key => key.Contains(op.ToLower())))
        {
            return key;
        }

        return op;
    }
}