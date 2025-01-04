using Spectre.Console;
using static CalculatorLibrary.Enums;

namespace CalculatorLibrary;

public class UserInterface
{
    private CalculatorEngine calculator = new CalculatorEngine();

    public void MainMenu()
    {
        bool endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            Console.Clear();

            double result;
            var menuSelection = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                .Title("[green]Choose an operation from the following list:[/]")
                .AddChoices(Enum.GetValues<MenuOptions>()));

            switch (menuSelection)
            {
                case MenuOptions.Add:
                case MenuOptions.Subtract:
                case MenuOptions.Multiply:
                case MenuOptions.Divide:
                case MenuOptions.ToPower:
                    Console.WriteLine("First Number: ");
                    double firstNumber = GetNumberFromUser();

                    Console.WriteLine("Second Number: ");
                    double secondNumber = GetNumberFromUser();

                    try
                    {
                        result = calculator.DoMathOperation(firstNumber, secondNumber, menuSelection);

                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    break;
                case MenuOptions.SquareRoot:
                    Console.WriteLine("Number to take square root of: ");
                    double sqrtNumber = GetNumberFromUser();

                    try
                    {
                        result = Math.Sqrt(sqrtNumber);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    break;
                case MenuOptions.TenTimes:
                    Console.WriteLine("Number to multiply by 10: ");
                    double tenTimesNumber = GetNumberFromUser();

                    try
                    {
                        result = tenTimesNumber * 10;
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    break;
                case MenuOptions.Trigonometry:
                    Console.WriteLine("Number to do trigonometry on: ");
                    double trigNumber = GetNumberFromUser();

                    try
                    {
                        result = calculator.DoTrigonometricOperation(trigNumber);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    break;
                case MenuOptions.ShowHistory:
                    calculator.ShowHistory();
                    break;
            }
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
        }

        calculator.Finish();
    }

    public double GetNumberFromUser()
    {
        Console.WriteLine("Enter a number OR type \"h\" to select from history");

        string? userInput1 = Console.ReadLine();
        double cleanNum1;

        if (userInput1.ToLower() == "h")
        {
            return calculator.SelectFromHistory();
        }
        
        while (!double.TryParse(userInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            userInput1 = Console.ReadLine();
        }

        return cleanNum1;
    }
}