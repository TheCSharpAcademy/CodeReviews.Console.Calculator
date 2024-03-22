using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

internal class CalculatorMenu
{
    Calculator calculator = new();
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

        string operation = Console.ReadLine();

        return operation;
    }
    internal bool CalculatorOperation(double firstNumber, double secondNumber, string operation)
    {
        CalculatorData calculatorData = new();

        double result = 0;
        if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d|l|u]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else if (Regex.IsMatch(operation, "l"))
        {
            calculator.Finish();
            calculatorData.CalculatorHistory();
            calculator.Start();
        }
        else if (Regex.IsMatch(operation, "u"))
        {
            calculator.Finish();
            calculatorData.CalculatorStatistics();
            calculator.Start();
        }
        else
        {
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
}
