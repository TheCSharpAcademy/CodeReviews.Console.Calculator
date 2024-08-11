using CalculatorLibrary;
class Application
{
    private static double num1;
    private static double num2;
   
    private static void Main()
    {
        bool quit = false;
        Calculator calculator = new Calculator();

        Console.WriteLine("\nConsole Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        while (!quit)
        {
            try
            {
                CalculatorApp(calculator);
                calculator.CountUsage();
                quit = PressToQuit("\"Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                DisplayOtherOptions(calculator);
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
                quit = PressToQuit("\"Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            }
        }
        calculator.Finish();
        return;

    }
    
    private static void DisplayMenu()
    {
        Console.WriteLine("Choose an option from the following list:");

        Console.WriteLine("-------Available operations-------");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");
    }

    private static void DisplayUsageTime(Calculator calculator)
    {
        {
            Console.WriteLine("\n---------Calculator Usage---------");
            string usageTime = "time";
            usageTime += calculator.GetUsageTime() > 1 ? "s" : "";
            Console.WriteLine($"The calculator has been used {calculator.GetUsageTime()} {usageTime}");
            Console.WriteLine("----------------------------------\n");
        }
    }
    private static void DeleteJsonFile(Calculator calculator)
    {
        Console.WriteLine("\n---------Record list---------");
        calculator.DeleteJsonFile();
        Console.WriteLine("----------------------------------\n");

    }
    private static void DisplayOtherOptions(Calculator calculator)
    {
        Console.WriteLine("-------Available Options-------");
        Console.WriteLine("\tu - Display Usage Time: ");
        Console.WriteLine("\tr - Remove record list file");
        Console.WriteLine("\tq - Quit");
        Console.Write("Your option? ");
        string? option = Console.ReadLine();
        switch (option)
        {
            case "u":
                DisplayUsageTime(calculator);
                break;
            case "r":
                calculator.Finish();
                DeleteJsonFile(calculator);
                break;
            case "q":
                calculator.Finish();
                Environment.Exit(0);
                break;
            default:
                break;

        }
    }

    private static bool PressToQuit(string message)
    {
        Console.Write(message);
        return Console.ReadLine() == "n";
    }
  
    public static void DisplayError(string errorMessage)
    {
        Console.WriteLine(errorMessage);
    }
    
    public static void CalculatorApp(Calculator calculator)
    {
        string? operation = "";
        num1 = calculator.AskUserForNumber("Type a number, and then press Enter: ");
        num2 = calculator.AskUserForNumber("Type another number, and then press Enter: ");
        Console.WriteLine("------------------------\n");

        DisplayMenu();
  
        operation = Console.ReadLine();
        calculator.Calculate(num1, num2, operation);
    }
}