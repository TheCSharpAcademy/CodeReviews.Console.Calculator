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

using GetTeched.Calculator;
using System.Text.RegularExpressions;

bool endApplication = false;


Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (!endApplication)
{
    string? firstNumber = "";
    string? secondNumber = "";
    double result = 0;

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

    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");

    string operation = Console.ReadLine();

    if (operation == null || !Regex.IsMatch(operation, "[a|s|m|d]"))
    {
        Console.WriteLine("Error: Unrecognized input.");
    }
    else
    {
        try
        {
            result = Calculator.DoOperation(cleanFirstNumber, cleanSecondNumber, operation);
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
    if (Console.ReadLine() != null) endApplication = true;

    Console.WriteLine("\n");

}
return;

