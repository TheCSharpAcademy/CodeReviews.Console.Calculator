using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

internal class CalculatorMenu
{
    Calculator calculator = new();
    List<string> history = new List<string>();
    internal double[] InputValues()
    {
        double[] numbers = new double[2];

        string? firstNumber = "";
        string? secondNumber = "";
        //double result = 0;

        Console.WriteLine("Type a number, and then press Enter.");
        firstNumber = Console.ReadLine();

        double cleanFirstNumber = 0;
        while (!double.TryParse(firstNumber, out cleanFirstNumber))
        {
            Console.WriteLine("This is not a vaild input. Please enter an integer value: ");
            firstNumber = Console.ReadLine();
        }

        Console.WriteLine("Type a number, and then press Enter.");
        secondNumber = Console.ReadLine();

        double cleanSecondNumber = 0;
        while (!double.TryParse(secondNumber, out cleanSecondNumber))
        {
            Console.WriteLine("This is not a vaild input. Please enter an integer value: ");
            secondNumber = Console.ReadLine();
        }
        numbers[0] = cleanFirstNumber;
        numbers[1] = cleanSecondNumber;
        return numbers;
    }
    internal string CalculatorOptions()
    {
        CalculatorData calculatorData = new();
        string? operation;

        Console.Clear();
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("-----Calculator Options-----");
        Console.WriteLine("\tu - Statistics");
        Console.WriteLine("\tl - Previous Results");
        Console.Write("Your option? ");   

        while(true)
        {
            operation = Console.ReadLine();
            if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d|l|u]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else if (Regex.IsMatch(operation, "l"))
            {
                calculator.Finish();

                Console.Clear();
                history.AddRange(calculatorData.CalculatorHistory());
                Console.WriteLine("Here are all previous calculations performed by this calculator: \n");
                foreach (string calculationHistory in history)
                {
                    Console.WriteLine(calculationHistory);
                }
                Console.WriteLine("Would you like to clear this list? Type Yes to clear list or any other key to continue.");
                string? userInput = Console.ReadLine().ToLower().Trim();
                while(!String.IsNullOrEmpty(userInput))
                {
                    if (userInput == "yes")
                    {
                        history.Clear();
                        Console.WriteLine("Calculation history has been cleared. Press any key to return to the Main Menu.");
                    }
                    else
                    {
                        break;
                    }
                    Console.WriteLine("Please try again.");
                }

                calculator.Start();
                break;
            }
            else if (Regex.IsMatch(operation, "u"))
            {
                calculator.Finish();
                history.AddRange(calculatorData.CalculatorHistory());
                calculatorData.CalculatorStatistics();
                calculator.Start();
                break;
            }
            else break;

        }

        return operation;
    }
    internal bool CalculatorOperation(double firstNumber, double secondNumber, string operation)
    {
        double result = 0;

        try
        {
            result = calculator.DoOperation(firstNumber, secondNumber, operation);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your reslut: {0:0.##}\n", result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }

        Console.WriteLine("------------------------\n");

        Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
        if (Console.ReadLine() == "n")
        {
            calculator.Finish();
            return true;
        }
        Console.WriteLine("\n");
        return false;
    }

    internal void CalculatorOptionValidation()
    {
        //TODO
    }
}
