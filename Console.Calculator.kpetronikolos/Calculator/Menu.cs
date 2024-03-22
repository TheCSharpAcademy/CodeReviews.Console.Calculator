using CalculatorLibrary;

namespace CalculatorProgram;

public static class Menu
{
    public static void MainFunctionality(Calculator calculator)
    {
        bool endApp = false;

        Printer.AskToContinueToMenu();

        while (!endApp)
        {
            DisplayMainMenu();

            string selection = Console.ReadLine();

            Console.Clear();

            switch (selection.Trim().ToLower())
            {
                case "p":
                    CalculatorEngine.InitCalculator(calculator);
                    break;
                case "a":
                    CalculatorEngine.InitCalculator(calculator, useSecondNumber: false);
                    break;
                case "c":
                    Printer.PrintCalculatorCount(calculator.GetCalculatorCount());
                    break;
                case "v":
                    CalculatorEngine.PrintCalculations();
                    Printer.AskToContinueToMenu();
                    break;
                case "d":
                    CalculatorEngine.DeleteCalculations();
                    break;
                case "h":
                    CalculatorEngine.InitHistoryCalculator(calculator);
                    break;
                case "q":
                    Console.WriteLine("Thanks for using the calculator. \nExiting ...");
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.\n");
                    break;
            }
        }
    }
    private static void DisplayMainMenu()
    {
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\tp - Perform Calculation");
        Console.WriteLine("\ta - Perform Advanced Calculation");
        Console.WriteLine("\tc - Count the amount of times the calculator was used");
        Console.WriteLine("\tv - View a list of the calculations");
        Console.WriteLine("\td - Delete the list of the calculations");
        Console.WriteLine("\th - Use the results from the calculation list to perform new calculation");
        Console.WriteLine("\tq - Quit the App");
        Console.Write("Enter your option: ");
    }

    public static void DisplayCalculationMenu()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\t+ - Add");
        Console.WriteLine("\t- - Subtract");
        Console.WriteLine("\t* - Multiply");
        Console.WriteLine("\t/ - Divide");
        Console.WriteLine("\t^ - Pow");
        Console.Write("Your option? ");
    }

    public static void DisplayAdvancedCalculationMenu()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\tsqrt - Square Root");
        Console.WriteLine("\t10^ - 10^");
        Console.WriteLine("\tsin - Sinus");
        Console.WriteLine("\tcos - CosSinus");
        Console.WriteLine("\ttan - Tangient");
        //Console.WriteLine("\ttan - Tangient"); TODO:sftan()
        Console.Write("Your option? ");
    }

    public static void DisplayCalculationOptions()
    {
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\tp - Perform Calculation");
        Console.WriteLine("\ta - Perform Advanced Calculation");
        Console.WriteLine("\tAny other key to go back");
        Console.Write("Enter your option: ");
    }
}
