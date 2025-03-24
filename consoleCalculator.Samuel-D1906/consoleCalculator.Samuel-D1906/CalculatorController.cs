using System.Globalization;
using System.Text.RegularExpressions;
using consoleCalculator.Samuel_D1906.CalculatorLibrary;
using Spectre.Console;

namespace consoleCalculator.Samuel_D1906;

public abstract class CalculatorController
{
    private static bool _endApp;
    private static int _count;
    private static List<double> _calculations = [];
    private static int _savedItems;

    static readonly Calculator Calculator = new Calculator();

    public static List<double> Operation(List<double> calculations)
    {
        while (!_endApp)
        {
            Console.Write("Calculations: ");
            _calculations.ForEach(item => Console.Write(item + ","));
            Console.WriteLine("\n");
            var numInput1 = "";
            var numInput2 = "";
            double result = 0;
            Console.WriteLine("Calculator used: " + _count);
            

            double cleanNum1;
            numInput1 = Calculator.UseNumberFromList(numInput1, _calculations);
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            


            double cleanNum2;
            numInput2 = Calculator.UseNumberFromList(numInput2, _calculations);
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsq - Square Root");
            Console.WriteLine("\tp - 10x Power");
            Console.WriteLine("\tsin - Trigonometry functions Sin");
            Console.WriteLine("\tcos - Trigonometry functions Cos");
            Console.WriteLine("\ttan - Trigonometry functions tan");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sq|p|sin|cos|tan]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = Calculator.DoOperation(cleanNum1, cleanNum2, op, _calculations);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " +
                                      e.Message);
                }

                _count = Calculator.CountOperations(_count);
                _calculations = Calculator.SaveInList(_calculations, result);
            }

            Console.WriteLine("Calculation saved in List!");
            _savedItems = _savedItems + 1;
            Console.WriteLine("Calculator used: " + _count);
            Console.WriteLine("------------------------\n");


            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                _endApp = true;
                _count = 0;
                Console.WriteLine("\n Calculator count reseted!");
                Calculator.Finish();
            }

            Console.WriteLine("\n"); 
        }

        return _calculations;
        
    }
}


