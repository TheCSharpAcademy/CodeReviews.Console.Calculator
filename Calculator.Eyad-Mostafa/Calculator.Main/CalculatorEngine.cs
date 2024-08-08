using System.Text.RegularExpressions;
using CalculatorLibrary;
using CalculatorProgrram.Models;

namespace CalculatorProgram;

internal class CalculatorEngine
{
    static List<Calculation> CalculationsHistory = new();
    
    internal static void TwoOperandsCalculations(Calculator calculator, ref int numberOfCalculation)
    {
        string numInput1;
        string numInput2;
        double result = 0;

        Console.Write("Type a number, and then press Enter: ");
        numInput1 = ReadNumericnput();

        Console.Write("Type another number, and then press Enter: ");
        numInput2 = ReadNumericnput();

        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tp - Taking the Power");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
            numberOfCalculation--;
        }
        else
        {
            try
            {
                result = calculator.DoTwoOperandsOperation(double.Parse(numInput1), double.Parse(numInput2), op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                    numberOfCalculation--;
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    if (op == "a")
                        op = "+";
                    else if (op == "s")
                        op = "-";
                    else if (op == "m")
                        op = "*";
                    else if (op == "d")
                        op = "/";
                    else
                        op = "^";
                    AddToHistory(double.Parse(numInput1), double.Parse(numInput2), result, numberOfCalculation, op, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }
        
        Console.WriteLine("------------------------\n");

    }

    internal static void MoreOperations(Calculator calculator, ref int numberOfCalculation)
    {
        string numInput1;
        double result = 0;

        Console.Write("Type a number, and then press Enter: ");
        numInput1 = ReadNumericnput();

        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\tsqrt - Square root");
        Console.WriteLine("\t10x");
        Console.Write("Your option? ");

        string? op = Console.ReadLine()?.Trim().ToLower();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[sqrt|pow|10x]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            try
            {
                result = calculator.DoOneOperandOperation(double.Parse(numInput1), op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                    numberOfCalculation--;
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    AddToHistory(double.Parse(numInput1), 0, result, numberOfCalculation, op, false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                numberOfCalculation--;
            }
        }
        Console.WriteLine("------------------------\n");
    }

    internal static void TrigonometryFunctions(Calculator calculator, ref int numberOfCalculation)
    {
        string numInput1;
        double result = 0;

        Console.Write("Type a number, and then press Enter: ");
        numInput1 = ReadNumericnput();

        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\tSin");
        Console.WriteLine("\tCos");
        Console.WriteLine("\tTan");
        Console.Write("Your option? ");

        string? op = Console.ReadLine()?.Trim().ToLower();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[sin|cos|tan]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            try
            {
                result = calculator.DoTrigonometryOperation(double.Parse(numInput1), op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                    numberOfCalculation--;
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    AddToHistory(double.Parse(numInput1), 0, result, numberOfCalculation, op, false);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                numberOfCalculation--;
            }
        }
        Console.WriteLine("------------------------\n");
    }

    static string ReadNumericnput()
    {
        string? input = Console.ReadLine();
        while (!double.TryParse(input, out _))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            input = Console.ReadLine();
        }
        return input;
    }

    static void AddToHistory(double operand1, double operand2, double result, int NumberOfCalculation, string operation, bool twoOperandOperation)
    {

        if (twoOperandOperation)
            CalculationsHistory.Add(new Calculation
            {
                CaluclatuonNumber = NumberOfCalculation,
                Operand1 = operand1,
                Operand2 = operand2,
                Result = result,
                Operation = operation,
            });
        else
            CalculationsHistory.Add(new Calculation
            {
                CaluclatuonNumber = NumberOfCalculation,
                Operand1 = operand1,
                Result = result,
                Operation = operation,
            });
    }
    
    internal static void ShowHistory()
    {
        Console.Clear();
        Console.WriteLine("Calculations History:");
        foreach (var history in CalculationsHistory)
            if(history.Operation == "sin" || history.Operation == "cos" || history.Operation == "tan" || history.Operation == "p" || history.Operation == "10x" || history.Operation == "sqrt")
                Console.WriteLine($"{history.CaluclatuonNumber}:  {history.Operation} {history.Operand1} = {history.Result}");
            else
                Console.WriteLine($"{history.CaluclatuonNumber}:  {history.Operand1} {history.Operation} {history.Operand2} = {history.Result}");

        Console.Write("\nType del to delete history : ");
        if (String.Equals(Console.ReadLine()?.Trim(), "del", StringComparison.OrdinalIgnoreCase))
            DeleteHistory();
    }

    static void DeleteHistory()
    {
        CalculationsHistory.Clear();
    }
}
