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
using System.Text.RegularExpressions;
namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApplication = false;

        CalculatorMenu calculatorMenu = new();

        while (!endApplication)
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            string operation = calculatorMenu.CalculatorOptions();
            if (Regex.IsMatch(operation, "[l|u]"))
            {
                endApplication = calculatorMenu.CalculatorOperation(0, 0, operation);
            }
            double[] numbers = calculatorMenu.InputValues();
            endApplication = calculatorMenu.CalculatorOperation(numbers[0], numbers[1], operation);
        }
        return;
    }
}


