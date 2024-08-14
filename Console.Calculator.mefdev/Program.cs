using System.Text.RegularExpressions;
using CalculatorLibrary;
class Application
{
    private static double s_num1;
    private static double s_num2;

    private static void Main()
    {
        bool quit = false;
        Calculator calculator = new Calculator();
        Console.WriteLine("\n-------------------------- Console Calculator ------------------------");
        while (!quit)
        {
            try
            {
                CalculatorApp(calculator);
                calculator.CountUsage();
                DisplayOtherOptions(calculator);
                quit = PressToQuit("\"Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            }
            catch (Exception ex)
            {
                DisplayError(ex.Message);
                quit = PressToQuit("\"Press 'n' and Enter to close the app: ");
            }
        }
        calculator.CloseRecord();
        return;

    }

    private static void DisplayMenu()
    {
        Console.WriteLine("-------------------------- Operation Menu ---------------------------");
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("-------------------------- Available operations----------------------");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");
    }

    private static void DisplayUsageTime(Calculator calculator)
    {
        Console.WriteLine("\n------------------------Calculator Usage---------------------------");
        string usageTime = "time";
        usageTime += calculator.UsageCounter > 1 ? "s" : "";
        Console.WriteLine($"The calculator has been used {calculator.UsageCounter} {usageTime}");
        Console.WriteLine("-------------------------------------------------------------------\n");

    }
    private static void DeleteJsonFile(Calculator calculator)
    {

        Console.WriteLine("\n------------------------- Record list ---------------------------");
        try
        {
            calculator.DeleteJsonFile();
            calculator.CloseRecord();
        }
        catch
        {
            throw new Exception("Cannot delete the file");
        }


    }
    private static void DisplayOtherOptions(Calculator calculator)
    {
        Console.WriteLine("-------------------------Available Options-------------------------");
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

                DeleteJsonFile(calculator);
                break;
            case "q":
                calculator.CloseRecord();
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
        s_num1 = calculator.AskUserForNumber("Type a number, and then press Enter: ");
        s_num2 = calculator.AskUserForNumber("Type another number, and then press Enter: ");

        while (!Regex.IsMatch(operation, "[a|s|m|d]"))
        {
            DisplayMenu();
            operation = Console.ReadLine()?.ToLower();
        };
        calculator.Calculate(s_num1, s_num2, operation);
    }
}
