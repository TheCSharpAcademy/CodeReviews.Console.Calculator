using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

internal class CalculatorMenu
{
    Calculator calculator = new();
    List<string> history = new List<string>();
    List<double> results = new List<double>();
    CalculatorData calculatorData = new();
    internal double[] InputValues(bool singleNumber)
    {
        double[] numbers = new double[2];

        string? firstNumber = "";
        string? secondNumber = "";
        //double result = 0;

        Console.WriteLine("Type a number, and then press Enter.");
        firstNumber = Console.ReadLine();
        if (singleNumber == false)
        {
            double cleanFirstNumber = ParseInputValue(firstNumber);
            Console.WriteLine("Type a number, and then press Enter.");
            secondNumber = Console.ReadLine();
            double cleanSecondNumber = ParseInputValue(secondNumber);
            numbers[0] = cleanFirstNumber;
            numbers[1] = cleanSecondNumber;
        }
        else
        {
            double cleanFirstNumber = ParseInputValue(firstNumber);
            numbers[0] = cleanFirstNumber;
        }
        return numbers;
    }
    internal double ParseInputValue(string number)
    {
        double cleanNumber = 0;
        while (!double.TryParse(number, out cleanNumber))
        {
            Console.WriteLine("This is not a vaild input. Please enter an integer value: ");
            number = Console.ReadLine();
        }
        return cleanNumber;
    }
    internal string CalculatorOptions()
    {
        string regexPattern = "^((a|s|m|d|l|u|r|q|p|x|sin|cos|tan),)*(a|s|m|d|l|u|r|q|p|x|sin|cos|tan)$";
        string? operation;

        MenuOptions(true);

        while (true)
        {
            operation = Console.ReadLine();
            if (operation == null || !Regex.IsMatch(operation, regexPattern))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else if (Regex.IsMatch(operation, "r"))
            {
                Results(operation);
                MenuOptions(true);
            }
            else break;
        }
        return operation;
    }
    internal bool CalculatorOperation(double firstNumber, double secondNumber, string operation)
    {
        double result = 0;
        string regexPattern = "^((q|x|sin|cos|tan),)*(q|x|sin|cos|tan)$";
        bool returnToMenu = true;
        try
        {
            if (Regex.IsMatch(operation, regexPattern))
            {
                result = calculator.AdvanceDoOperation(firstNumber, operation);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.WriteLine("Your reslut: {0:0.##}\n", result);
                    returnToMenu = false;
                }
            }
            else if (Regex.IsMatch(operation, "^u$"))
            {
                Statistics();

            }
            else if (Regex.IsMatch(operation, "^l$"))
            {
                PreviousCalculations();
                returnToMenu = true;
            }
            else
            {
                result = calculator.StandardDoOperation(firstNumber, secondNumber, operation);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.WriteLine("Your reslut: {0:0.##}\n", result);
                    returnToMenu = false;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }

        Console.WriteLine("------------------------\n");
        if (!returnToMenu)
        {
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                calculator.Finish();
                return true;
            }
        }
        Console.WriteLine("\n");
        return false;
    }

    internal void MenuOptions(bool displayOptions)
    {
        Console.Clear();
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tq - Square Root");
        Console.WriteLine("\tp - Power of X");
        Console.WriteLine("\tx - Power of 10");
        Console.WriteLine("\tsin - Sin");
        Console.WriteLine("\tcos - Cos");
        Console.WriteLine("\ttan - Tan");
        if (displayOptions == true)
        {
            Console.WriteLine("-----Calculator Options-----");
            Console.WriteLine("\tu - Statistics");
            Console.WriteLine("\tl - Previous Calculations");
            Console.WriteLine("\tr - Previous Results");
        }

        Console.Write("Your option? ");
    }

    internal void PreviousCalculations()
    {
        calculator.Finish();
        Console.Clear();
        history.AddRange(calculatorData.CalculatorHistory());
        results.AddRange(calculatorData.ResultHistory());
        Console.WriteLine("Here are all previous calculations performed by this calculator: \n");
        foreach (string calculationHistory in history)
        {
            Console.WriteLine(calculationHistory);
        }
        Console.WriteLine("Would you like to clear this list? Type Yes to clear list or any other key to continue.");
        string? userInput = Console.ReadLine().ToLower().Trim();
        while (!String.IsNullOrEmpty(userInput))
        {
            if (userInput == "yes")
            {
                history.Clear();
                Console.WriteLine("Calculation history has been cleared. Press any key to return to the Main Menu.");
                break;
            }
            else
            {
                break;
            }
        }
        calculator.Start();
    }

    internal void Statistics()
    {
        calculator.Finish();
        history.AddRange(calculatorData.CalculatorHistory());
        results.AddRange(calculatorData.ResultHistory());
        calculatorData.CalculatorStatistics();
        calculator.Start();
    }

    internal void Results(string operation)
    {
        calculator.Finish();
        int entry = 1;

        Console.Clear();
        results.AddRange(calculatorData.ResultHistory());
        Console.WriteLine("Here are all the previous results performed:\n");
        if (results.Count > 0)
        {

            foreach (double result in results)
            {
                Console.WriteLine($"{entry}) {result}");
                entry++;
            }
            Console.WriteLine("\nDo you want to use previous results?");
            Console.WriteLine("Type yes to use results or any other key to return to the menu.");
            Console.WriteLine("!!!Current implementation will always ask you to select two numbers.!!!");

            ResultsSelection(operation);
        }
        else
        {
            calculator.Start();
            Console.WriteLine("No results found, press any key to return to the menu.");
            Console.ReadLine();
            MenuOptions(true);
        }
    }

    internal void ResultsSelection(string operation)
    {
        double firstNumber = 0;
        double secondNumber = 0;
        string regexPattern = "^((a|s|m|d|q|p|x|sin|cos|tan),)*(a|s|m|d|q|p|x|sin|cos|tan)$";

        string? userInput = Console.ReadLine();
        if (Regex.IsMatch(userInput, "^((yes),)*(yes)$"))
        {
            Console.WriteLine("Type the entry index number to select first number");
            int value;
            userInput = Console.ReadLine();
            while (!String.IsNullOrEmpty(userInput) && int.TryParse(userInput, out value))
            {
                if (value < results.Count() + 1)
                {
                    firstNumber = results[value - 1];
                    break;
                }
                Console.WriteLine("Error: Unrecognized input.");
            }

            Console.WriteLine("Type the entry index number to select second number");
            userInput = Console.ReadLine();
            while (!String.IsNullOrEmpty(userInput) && int.TryParse(userInput, out value))
            {
                if (value < results.Count() + 1)
                {
                    secondNumber = results[value - 1];
                    break;
                }
                Console.WriteLine("Error: Unrecognized input.");
            }

            calculator.Start();
            MenuOptions(false);
            while (true)
            {
                operation = Console.ReadLine();
                if (operation == null || !Regex.IsMatch(operation, regexPattern))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    CalculatorOperation(firstNumber, secondNumber, operation);
                    break;
                }
            }
        }
        else
        {
            calculator.Start();
            MenuOptions(true);
        }
    }
}
