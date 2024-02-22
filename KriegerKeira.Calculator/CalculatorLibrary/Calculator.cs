using System.Transactions;
using Newtonsoft.Json;
namespace CalculatorLibrary;

public class Calculator
{
    readonly JsonWriter _writer;

    public Calculator()
    {
        var logFile = File.CreateText(@"c:\temp\calculatorlog.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile)
        {
            Formatting = Formatting.Indented
        };
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operation");
        _writer.WriteStartArray();
    }

    public void JsonWriter(double firstNumber, double? secondNumber, double result, string operationType)
    {
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(firstNumber);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(secondNumber);
        _writer.WritePropertyName("Operation");
        _writer.WriteValue(operationType);
        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();
    }

    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Close();
    }
    public void DoOperation(string? op)
    {
        switch (op)
        {
            case "a":
                Addition();
                break;
            case "s":
                Subtraction();
                break;
            case "m":
                Multiplication();
                break;
            case "d":
                Division();
                break;
        }
    }
    public static void CalculatorHeader()
    {
        Console.Clear();
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
    }

    public static double GetFirstNumber()
    {
        var calculations = CalculationDataStore.Results;
        var lastCalculation = calculations.LastOrDefault();
        var cleanFirstNumber = double.NaN;

        if (!double.IsNaN(lastCalculation) && calculations.Count !=0)
        {
            CalculatorHeader();
            Console.Write($"Your last calculation was {lastCalculation}, would you like to use this number in your next calculation? y/n: ");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    cleanFirstNumber = lastCalculation;
                    break;
                case ConsoleKey.N:
                {
                    CalculatorHeader();
                    Console.Write("Enter a number: ");
                    var firstNumber = Console.ReadLine()?.Trim().ToLower();
                    cleanFirstNumber = ValidateNumbers(firstNumber);
                } break;
            }
        }
        else
        {
            CalculatorHeader();
            Console.Write("Enter a number: ");
            var firstNumber = Console.ReadLine()?.Trim().ToLower();
            cleanFirstNumber = ValidateNumbers(firstNumber);
        }

        return cleanFirstNumber;
    }

    public static double ValidateNumbers(string? number)
    {
        double cleanNumber;
        while (!double.TryParse(number, out cleanNumber))
        {
            CalculatorHeader();
            Console.Write("Invalid entry, please enter a number: ");
            number = Console.ReadLine()?.Trim().ToLower();
        }
        return cleanNumber;
    }
    private void DisplayResults(double result, double cleanFirstNumber, double? cleanSecondNumber, string operationType)
    {
        try
        {
            if (double.IsNaN(result))
            {
                CalculatorHeader();
                Console.WriteLine("This operation will result in a mathematical error. \n");
            }
            else
            {
                CalculatorHeader();
                Console.WriteLine($"Your result: {result}");
                CalculationDataStore.AddLastCalculation(result);
                JsonWriter(cleanFirstNumber, cleanSecondNumber, result, operationType);
            }
        }
        catch (Exception e)
        {
            CalculatorHeader();
            Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + e.Message);
        }
    }

    public double Addition()
    {
        var operationType = "Add";
        var cleanFirstNumber = GetFirstNumber();
        CalculatorHeader();
        Console.Write("Enter your second number: ");
        var secondNumber = Console.ReadLine()?.Trim().ToLower();
        var cleanSecondNumber = ValidateNumbers(secondNumber);
        var result = cleanFirstNumber + cleanSecondNumber;
        DisplayResults(result,cleanFirstNumber,cleanSecondNumber,operationType);
        return result;

    }
    public double Subtraction()
    {
        var operationType = "Subtract";
        var cleanFirstNumber = GetFirstNumber();
        CalculatorHeader();
        Console.Write("Enter your second number: ");
        var secondNumber = Console.ReadLine()?.Trim().ToLower();
        var cleanSecondNumber = ValidateNumbers(secondNumber);
        var result = cleanFirstNumber - cleanSecondNumber;
        DisplayResults(result, cleanFirstNumber, cleanSecondNumber, operationType);
        return result;

    }
    public double Multiplication()
    {
        var operationType = "Multiply";
        var cleanFirstNumber = GetFirstNumber();
        CalculatorHeader();
        Console.Write("Enter your second number: ");
        var secondNumber = Console.ReadLine()?.Trim().ToLower();
        var cleanSecondNumber = ValidateNumbers(secondNumber);
        var result = cleanFirstNumber * cleanSecondNumber;
        DisplayResults(result, cleanFirstNumber, cleanSecondNumber, operationType);
        return result;

    }
    public double Division()
    {
        var operationType = "Divide";
        var cleanFirstNumber = GetFirstNumber();
        CalculatorHeader();
        Console.Write("Enter your second number: ");
        var secondNumber = Console.ReadLine()?.Trim().ToLower();
        var cleanSecondNumber = ValidateNumbers(secondNumber);
        while (cleanSecondNumber == 0)
        {
            CalculatorHeader();
            Console.Write("Cannot divide by 0, please enter a number other than 0: ");
            secondNumber = Console.ReadLine()?.Trim().ToLower();
            cleanSecondNumber = ValidateNumbers(secondNumber);
        }
        var result = cleanFirstNumber / cleanSecondNumber;
        DisplayResults(result, cleanFirstNumber, cleanSecondNumber, operationType);
        return result;

    }
}