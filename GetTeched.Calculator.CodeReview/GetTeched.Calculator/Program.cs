/*
 * For your second project you'll build a Console Calculator App with the help of Microsoft’s Documentation.
 * This project shouldn't be more difficult than the first, but you'll learn important skills such as having multiple
 * projects in a solution, writing to files, and debugging. It will also serve as practice in a very important skill
 * following written documentation. This is something you’ll be doing on a regular basis as a professional
 * developer, so it’s essential that you’re comfortable applying text-based instructions when developing software.
 * 
 ******************************************CHALLENGES***************************************************************
 *
 * Create a functionality that will count the amount of times the calculator was used.
 * Store a list with the latest calculations. And give the users the ability to delete that list.
 * Allow the users to use the results in the list above to perform new calculations.
 * Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.
 */
using CalculatorLibrary;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
namespace CalculatorProgram;

class Program
{
    
    static void Main(string[] args)
    {
        bool endApplication = false;
        bool jsonActive = false;
        JsonParse jsonParse = new();
        CalculatorMenu calculatorMenu = new();
        Calculator calculator = new();
        CalculatorData calculatorData = new();


        while (!endApplication)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            string operation = calculatorMenu.GameMenu();

            double[] numbers = calculatorMenu.InputValues();

            
  
            double result = 0;
            if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d|l|u]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else if (Regex.IsMatch(operation, "l"))
            {
                calculator.Finish();
                calculatorData.CalculatorHistory();
            }
            else if (Regex.IsMatch(operation, "u"))
            {
                calculator.Finish();
                int usage = jsonParse.GetCalculatorUsageStats();
                Console.WriteLine($"Total Calculations Computed: {usage}");
            }
            else
            {
                try
                {
                    calculator.Start();
                    result = calculator.DoOperation(numbers[0], numbers[1], operation);
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
                endApplication = true;
            }
            else if(Console.ReadLine() == "x")
            {
                calculator.Finish();
                Console.WriteLine(jsonParse.CalculationHistory().Count());
                foreach (string test in jsonParse.CalculationHistory())
                {  Console.WriteLine(test); }
                jsonParse.CalculationHistory().Clear();
                Console.WriteLine(jsonParse.CalculationHistory().Count());
            }

            Console.WriteLine("\n");

        }
        calculator.Finish();
        return;
    }

}


