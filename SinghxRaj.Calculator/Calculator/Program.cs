using CalculatorLibrary;

internal class Program
{
    private static void Main(string[] args)
    {
        DisplayTitle();
        RunCalculator();
        DisplayExit();
    }

    private static void RunCalculator()
    {
        var calculator = new Calculator();
        bool continueCalculator = true;
        while (continueCalculator)
        {
            continueCalculator = SingleCalculation(calculator);
            Console.WriteLine();
        }
        calculator.Finish();
        DisplayTotalCalculations(calculator);
    }

    private static void DisplayExit()
    {
        Console.WriteLine("Press any key to close the Calculator console app...");
        Console.ReadKey();
    }

    private static void DisplayTitle()
    {
        Console.WriteLine("Console Calculator in c#\r");
        Console.WriteLine("-------------------------\n");
    }

    private static bool SingleCalculation(Calculator calculator)
    {
        bool isCalculatorRunning;
        if (calculator.TotalOperations != 0)
        {
            AskToViewPreviousCalculations(calculator);
            AskToClearCache(calculator);
        }
        PerformCalculation(calculator);
        isCalculatorRunning = ContinueCalculator();

        return isCalculatorRunning;
    }

    private static void DisplayTotalCalculations(Calculator calculator)
    {
        Console.WriteLine($"Total number of operations: {calculator.TotalOperations}");
    }

    private static bool ContinueCalculator()
    {
        bool isCalculatorRunning;
        Console.WriteLine("Would you like to go again (y or n).");

        string? response = Console.ReadLine();
        if (string.IsNullOrEmpty(response)) return false;
        char firstLetterOfResponse = response.ToLower()[0];
        isCalculatorRunning = firstLetterOfResponse == 'y';

        return isCalculatorRunning;
    }

    private static void PerformCalculation(Calculator calculator)
    {
        double num1, num2;
        GetOperands(out num1, out num2);

        Console.WriteLine("Choose an option from the following list:");

        Console.WriteLine("\t+ - Add");
        Console.WriteLine("\t- - Subtract");
        Console.WriteLine("\t* - Multiple");
        Console.WriteLine("\t/ - Divide");
        Console.WriteLine("Your option?");

        try
        {
            string? op = Console.ReadLine();
            double result = calculator.DoOperation(num1, num2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("Invalid Operator.");
            }
            else
            {
                Console.WriteLine($"{num1} {op} {num2} = {result}");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private static void GetOperands(out double num1, out double num2)
    {
        Console.WriteLine("Type a number, and then press Enter");
        while (!double.TryParse(Console.ReadLine(), out num1))
        {
            Console.WriteLine("Invalid input. Must enter a number:");
        }

        Console.WriteLine("Type another number, and then press Enter");
        while (!double.TryParse(Console.ReadLine(), out num2))
        {
            Console.WriteLine("Invalid input. Must enter a number:");
        }
    }

    private static void AskToClearCache(Calculator calculator)
    {
        Console.WriteLine("Would you like to clear previous calculations (y or n)?");
        string? response = Console.ReadLine();
        if (!string.IsNullOrEmpty(response) && response[0] == 'y')
        {
            calculator.ClearCache();
            Console.WriteLine("Calculator has been cleared.");
        }
    }

    private static void AskToViewPreviousCalculations(Calculator calculator)
    {
        Console.WriteLine("Would you like to see previous calculations (y or n)?");
        string? response = Console.ReadLine();
        if (!string.IsNullOrEmpty(response) && response[0] == 'y')
        {
            string cache = calculator.ViewCache();
            Console.WriteLine("List of previous operations:");
            if (string.IsNullOrEmpty(cache))
            {
                cache = "No previous calculations";
            }
            Console.WriteLine(cache);
        }
    }
}