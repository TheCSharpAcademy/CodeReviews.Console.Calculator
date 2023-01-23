using CalculatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.kraven88;
internal static class MainMenu
{
    internal static void SelectionScreen(Calculator calculator)
    {
        GreetingMessage();
        var calc = calculator;
        var isRunning = true;

        while (isRunning)
        {
            DisplayMenuItems();
            isRunning = TakeUserInput("12q") switch
            {
                '1' => NewEquasion(calc),
                '2' => PreviousEquasions(calc),
                'q' => false,
            };
        }
    }


    private static bool NewEquasion(Calculator calculator)
    {
        var nextEquasion = true;

        while (nextEquasion)
        {
            Console.Clear();
            // Declare variables and initialize with zero;
            var numText1 = "";
            var numText2 = "";
            var result = 0.0;

            // Ask the user to type the first number;
            Console.Write("Type a number, then press Enter: ");
            numText1 = Console.ReadLine()!.Trim();

            var num1 = 0.0;
            while (double.TryParse(numText1, out num1) == false)
            {
                Console.Write("Invalid number. Please enter a numeric value: ");
                numText1 = Console.ReadLine()!.Trim();
            }

            // Ask the user to type the second number;
            Console.Write("Type another number, then press Enter: ");
            numText2 = Console.ReadLine()!.Trim();

            var num2 = 0.0;
            while (double.TryParse(numText2, out num2) == false)
            {
                Console.Write("Invalid number. Please enter a numeric value: ");
                numText2 = Console.ReadLine()!.Trim();
            }

            // Ask the user to choose an option;
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your choice: ");

            var operation = Console.ReadLine()!.Trim();

            try
            {
                result = calculator.DoOperation(num1, num2, operation);

                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in mathematical error.\n");
                else
                {
                    calculator.counter++;
                    Console.WriteLine($"Your result: {result:0.##}. You used the Calculator {CountFormatter(calculator.counter)}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oh no! An exception occured while doing the math. \nDetails: {ex.Message}");
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing the app;
            Console.Write("Do you want another equasion? (Y/N): ");
            if (Console.ReadLine().ToLower() == "n")
                nextEquasion = false;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        
        return true;
    }

    private static bool PreviousEquasions(Calculator calculator)
    {
        Console.Clear();
        Console.WriteLine("List of previous equasions");
        Console.WriteLine("--------------------------");
        foreach (var eq in calculator.equasions)
        {
            Console.WriteLine(eq);
        }
        Console.WriteLine("\nType 'delete' to clear the list, or any key to go back\n");
        if (Console.ReadLine()!.ToUpper() == "DELETE")
        {
            calculator.DeleteEquasions();
        }

        return true;
    }

    private static char TakeUserInput(string availableOptions)
    {
        var input = Console.ReadKey(true).KeyChar;
        if (availableOptions.Contains(input) == false)
        {
            Console.WriteLine("Invalid input. Please choose from the options listed above");
            input = TakeUserInput(availableOptions);
        }
        return input;
    }

    private static void DisplayMenuItems()
    {
        Console.Clear();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("\t1 - New equasion");
        Console.WriteLine("\t2 - Previous equasions");
        Console.WriteLine("\tQ - Quit");
        Console.WriteLine();
    }

    internal static void GreetingMessage()
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Wecome to Console Calculator App by kraven88");
        Console.WriteLine("--------------------------------------------\n");
    }

    private static string CountFormatter(int count)
    {
        var output = $"{count} time";
        if (count > 1) output += "s";
        return output;
    }
}
