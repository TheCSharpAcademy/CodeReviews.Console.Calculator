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
        JsonParse jsonParse = new();
        CalculatorMenu calculatorMenu = new();

        while (!endApplication)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            
            double[] numbers = calculatorMenu.InputValues();

            string operation = calculatorMenu.GameMenu();
            calculatorMenu.GameOperation(numbers[0], numbers[1], operation);

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                endApplication = true;
            }
            else if(Console.ReadLine() == "x")
            {
                calculatorMenu.RecordResultsJSON();
                Console.WriteLine(jsonParse.CalculationHistory().Count());
                foreach (string test in jsonParse.CalculationHistory())
                {  Console.WriteLine(test); }
                jsonParse.CalculationHistory().Clear();
                Console.WriteLine(jsonParse.CalculationHistory().Count());
            }

            Console.WriteLine("\n");

        }
        calculatorMenu.RecordResultsJSON();
        return;
    }
    
}


