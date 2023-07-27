using static Calculator_lib.CalculatorEngine;

namespace Calculator_lib;

public static class Menu
{
    private static string ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine(@$"Choose an option:
        Add - Addition
        Sub - Subtraction
        Multi - Multiplication
        Div - Divison
        10x - 10x
        Pow - Taking to power
        Sqrt - Square root
        Sin - Sinus
        Cos - Cosinus
        Tan - Tangens
        Ctan - Cotanges
        H - History of calculations
        Cls - Clear the history of calculations
        Q - Quit the program");

        Console.Write("Your option: ");
        return Helpers.ValidateOption(Console.ReadLine());
    }

    private static bool MainMenuChoice(string option)
    {
        switch(option.ToLower().Trim()) 
        {
            case "add":
                Console.Clear();
                Addition();
                Console.ReadKey();
                break;

            case "sub":
                Console.Clear();
                Subtraction();
                Console.ReadKey();
                break;

            case "multi":
                Console.Clear();
                Multiplication();
                Console.ReadKey();
                break;

            case "div":
                Console.Clear();
                Divison();
                Console.ReadKey();
                break;

            case "10x":
                Console.Clear();
                TimesTen();
                Console.ReadKey();
                break;

            case "pow":
                Console.Clear();
                TakeToPower();
                Console.ReadKey();
                break;

            case "sqrt":
                Console.Clear();
                SquareRoot();
                Console.ReadKey();
                break;

            case "sin":
                Console.Clear();
                Sinus();
                Console.ReadKey();
                break;
            
            case "cos":
                Console.Clear();
                Cosinus();
                Console.ReadKey();
                break;

            case "tan":
                Console.Clear();
                Tangens();
                Console.ReadKey();
                break;

            case "ctan":
                Console.Clear();
                Cotangens();
                Console.ReadKey();
                break;

            case "h":
                Console.Clear();
                Helpers.PrintHistory();
                Console.ReadKey();
                break;

            case "cls":
                Helpers.ClearHistory();
                Console.WriteLine("\nCalculator history is deleted!\nPress any key to continue...");
                Console.ReadKey();
                
                break;

            case "q":
                Console.Clear();
                Console.WriteLine("Thank you for using our calculator!");
                return false;

            default:
                Console.WriteLine("Choose some of the options from the menu");
                Console.ReadKey();
                break;
        }
        return true;
    }

    public static void MainMenu()
    {
        bool anotherCalculation;
        string option;
        do
        {
            option = ShowMainMenu();
            anotherCalculation = MainMenuChoice(option);
        }
        while (anotherCalculation);
    }
}
