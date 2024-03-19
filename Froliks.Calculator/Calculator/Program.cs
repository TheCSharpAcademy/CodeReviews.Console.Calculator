using System.Text.RegularExpressions;

namespace CalculatorProgram;

public class Calculator
{
    private static CalculatorLibrary.Calculator _calculator = new CalculatorLibrary.Calculator();
    private static bool usePreviousResult;

    public static void Main(string[] args)
    {
        ShowMenu();
    }

    private static void ShowMenu()
    {
        var endApp = false;
        while (!endApp)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("\tc - Go to calculator");

            if (_calculator.GetOperationsCount() != 0)
            {
                Console.WriteLine("\tp - Use calculator with previous result");
            }
            
            Console.WriteLine("\td - Delete previous calculations");
            Console.WriteLine("\te - Exit app");
            Console.Write("Your option? ");
            
            switch (Console.ReadLine())
            {
                case "c":
                    Console.Clear();
                    usePreviousResult = false;
                    ShowCalculator();
                    break;
                case "p" when _calculator.GetOperationsCount() > 0:
                    Console.Clear();
                    usePreviousResult = true;
                    ShowCalculator();
                    break;
                case "d":
                    _calculator.DeleteData();
                    break;
                case "e":
                    endApp = true;
                    break;
                default:
                    break;
            }
        }
    }

    private static void ShowCalculator()
    {
        var endApp = false;

        while (!endApp)
        {
            var result = 0.0;
            var usageCount = _calculator.GetUsageCount();
            var cleanNum1 = 0.0;
            
            if (usePreviousResult)
            {
                cleanNum1 = _calculator.GetNumberFromData();
            }
            else
            {
                Console.Write("Type a number, and then press Enter: ");
                cleanNum1 = GetNumberFromConsole();
            }
            
            Console.Write("Type another number, and then press Enter: ");
            var cleanNum2 = GetNumberFromConsole();

            Console.WriteLine($"\nYou used calculator {usageCount} times");
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            var operation = Console.ReadLine();
            
            if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            { 
                try
                {
                    result = _calculator.DoOperation(cleanNum1, cleanNum2, operation);
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
            }
            
            Console.Write("Press 'n' and Enter to go to the menu, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
            
            Console.Clear();
        }
        _calculator.Finish();
    }

    private static double GetNumberFromConsole()
    {
        var numInput = Console.ReadLine();

        var cleanNum = 0.0;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }
}