using CalculatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorProgram;

internal class Helpers
{
    internal static string GetChoice()
    {
        Console.WriteLine("Choose 'n' to make a new calculation or 'v' to view your latest calculations\n");
        var choice = ValidateInput(Console.ReadLine().ToLower().Trim(), "n", "v");

        return choice;
    }

    internal static double StartProgram(string choice)
    {
        double num = 0;

        if (choice == "v")
        {
            Calculator.ShowLatestCalculations();

            var input = ValidateInput(Console.ReadLine(), "n", "u", "d");

            if (input == "d")
            {
                Calculator.DeleteLists();
                Console.WriteLine("List Deleted");
                Console.ReadLine();
            }

            if (input == "u")
            {
                num = Calculator.ShowAndUseLatestResults();    
            }
        }

        return num;
    }

    public static string GetOperator()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");

        string input = ValidateInput(Console.ReadLine(), "a", "s", "m", "d");

        return input;
    }
    public static double GetNumber()
    {
        Console.Write("Type a number, and then press Enter: ");
        string? numInput = Console.ReadLine();
        double cleanNum;

        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }

    public static string ValidateInput(string input, string s1, string s2)
    {
        input = input.ToLower().Trim();

        while (input != s1 && input != s2)
        {
            Console.WriteLine("Invalid input.");
            input = Console.ReadLine().ToLower().Trim();
        }

        return input;
    }
    public static string ValidateInput(string input, string s1, string s2, string s3)
    {
        input = input.ToLower().Trim();

        while (input != s1 && input != s2 && input != s3)
        {
            Console.WriteLine("Invalid input.");
            input = Console.ReadLine().ToLower().Trim();
        }

        return input;
    }

    public static string ValidateInput(string input, string s1, string s2, string s3, string s4)
    {
        input = input.ToLower().Trim();

        while (!Regex.IsMatch(input, $"[{s1}|{s2}|{s3}|{s4}]"))
        {
            Console.WriteLine("Invalid input.");
            input = Console.ReadLine().ToLower().Trim();
        }

        return input;
    }

    public static bool IsCalculationComplete()
    {
        Console.Write(@"Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");

        if (Console.ReadLine().ToLower() == "n") return true;
        else return false;
    }
}
