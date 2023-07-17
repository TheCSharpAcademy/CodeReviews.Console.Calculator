using Calculator_lib.Model;
using System.Text.RegularExpressions;

namespace Calculator_lib;

internal static class Helpers
{
    // validate menu option
    internal static string ValidateOption(string option)
    {
       while(string.IsNullOrEmpty(option))
        {
            Console.WriteLine("\nYou need to choose some of the options from the menu.");
            Console.Write("Your option: ");
            option = Console.ReadLine();
        }

       return option;
    }

    // list of calculations
    internal static readonly List<Calculator> calculatorHistory= new List<Calculator>();
    

    // input number
    private static bool IsValidNumber(string number) 
    {
        Regex regex = new(@"^[+-]?\d+(\.\d+)?$");
        return regex.IsMatch(number);
    }
        
    internal static double InputNumber(char op = 'a')
    {
        Console.Clear();

        string text = (op.Equals('p')) ? "exponent" : "number";
        
        Console.Write($"Enter a {text}: ");
        string number = Console.ReadLine();

        while(!IsValidNumber(number))
        {
            Console.Clear();
            Console.WriteLine("Please enter a valid number.");
            Console.Write($"Enter a {text}: ");
            number = Console.ReadLine(); 
        }
        
        return Convert.ToDouble(number);
    }
    
    // get a number from a list
    internal static double GetNumber() 
    {
        if(calculatorHistory.Count > 0)
        {
            Console.WriteLine($"Do you want to continue calculation with previous result ({calculatorHistory[^1].Result})?\nType 'y' or 'n'");
            string choice = GetChoice(Console.ReadLine());

            if(choice.ToLower() == "y")
            {
                return calculatorHistory[^1].Result;
            }

            else
            {
                Console.Clear();
                return InputNumber();
            }
        }

        else
        {
            Console.Clear();
            return InputNumber();
        }
    }

    private static bool IsValidChoice(string choice)
    {
        Regex regex = new("^[YyNn]$");
        return regex.IsMatch(choice);
    }

    private static string GetChoice(string choice)
    {
        while(!IsValidChoice(choice))
        {
            Console.Clear();
            Console.Write("Please enter either 'y' or 'n': ");
            choice = Console.ReadLine();
        }

        return choice;
    }

    // add result to history
    internal static void AddToHistory(TypeOfCalculation type, double result)
    {
        calculatorHistory.Add(new Calculator()
        {
            DateTime = DateTime.Now,
            TypeOfCalculation = type,
            Result = result
        });

        Calculator.NumberOfTimesUsed++;
}

    // print calculator history
    internal static void PrintHistory()
    {
        if(calculatorHistory.Count > 0)
        {
            foreach(Calculator calculator in calculatorHistory)
            {
                Console.WriteLine($"Date of calculation: {calculator.DateTime} - Type of calculation: {calculator.TypeOfCalculation}, Result: {calculator.Result}");
            }

            Console.WriteLine($"\nCalculator was used {Calculator.NumberOfTimesUsed} times");
        }

        else if(calculatorHistory.Count == 0 && Calculator.NumberOfTimesUsed == 0)
        {
            Console.WriteLine("Your history is empty!\nPress any key to continue");
            Console.ReadKey();
        }
        
        else if(calculatorHistory.Count == 0 && Calculator.NumberOfTimesUsed > 0)
        {
            Console.WriteLine("Your history is empty!");
            Console.WriteLine($"\nNumber of times calculator has been used: {Calculator.NumberOfTimesUsed}");
            Console.ReadKey();
        }
    }

    // clear history
    internal static void ClearHistory()
    {
        calculatorHistory.Clear();
    }

    // angle to radian
    internal static double AngleInRadian(double angle) => Math.PI * angle / 180;


}