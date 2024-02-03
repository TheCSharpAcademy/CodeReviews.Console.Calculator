using CalculatorLibrary;

namespace CalculatorProgram;

public static class Program
{
    private static Calculator calculator = new Calculator();
    private static int timesUsed = 0;

    private static double GetNumber()
    {
        string numInput = "";
        numInput = Console.ReadLine();

        double cleanNum = 0;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }

    private static double SelectInputType()
    {
        Console.WriteLine("Choose input type:");
        Console.WriteLine("1 - Type a number");
        Console.WriteLine("2 - Get value from results");
        string? input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.Write("Type a number, and then press Enter: ");
                return GetNumber();
            case "2":
                Console.Write("Type a index, and then press Enter: ");
                return Results.GetResultFromList();
            default:
                Console.WriteLine("Wrong input");
                break;
        }
        
        return 0;
    }

    private static void UseCalculator()
    {
        Console.WriteLine($"Usage count: {timesUsed}");
        timesUsed++;
        
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tp - Power");
        Console.WriteLine("\tsr - Square Root");
        Console.WriteLine("\tt - 10x");
        Console.WriteLine("\tsin - sine");
        Console.WriteLine("\tcos - cosine");
        Console.WriteLine("\ttan - tangent");
        Console.WriteLine("\tctan - cotangent");
        Console.Write("Your option? ");

        string op = Console.ReadLine();
        try
        {
            double result = 0;
            switch (op)
            {
                case "a":
                case "s":
                case "m":
                case "d":
                case "p":
                    double cleanNum1 = SelectInputType();
                    double cleanNum2 = SelectInputType();
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    break;
                case "sr":
                case "t":
                case "sin":
                case "cos":
                case "tan":
                case "ctan":
                    double cleanNum = SelectInputType();
                    result = calculator.DoOperation(cleanNum, op);
                    break;
            }
            
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Results.Add(result);
                Console.WriteLine("Your result: {0:0.##}\n", result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
    }
    
    static void Main(string[] args)
    {
        bool endApp = false;
        
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------");
        
        while (!endApp)
        {
            Console.WriteLine("Choose option:");
            Console.WriteLine("1 - Calculate");
            Console.WriteLine("2 - View results");
            Console.WriteLine("3 - Clear results");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    UseCalculator();
                    break;
                case "2":
                    Results.PrintResults();
                    break;
                case "3":
                    Results.Clear();
                    Console.WriteLine("Results cleared");
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
            Console.WriteLine("------------------------");
            
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("");
        }
        calculator.Finish();
    }
}