using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

internal class CalculatorMenu
{
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

    internal string GameMenu()
    {
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("-----Calculator Options-----");
        Console.WriteLine("\tl - Previous Results");
        Console.Write("Your option? ");


        string operation = Console.ReadLine();

        return operation;
    }

    internal void GameOperation(double firstNumber, double secondNumber, string operation)
    {
        CalculatorData calculatorData = new();
        Calculator calculator = new();
        double result = 0;
        if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d|l]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else if (Regex.IsMatch(operation, "l"))
        {
            calculatorData.CalculatorHistory();
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
    }

    internal void RecordResultsJSON()
    {
        Calculator calculator = new();
        calculator.Finish();
    }

}
