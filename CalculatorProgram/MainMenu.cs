using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

public class MainMenu
{
    internal void ShowMenu()
    {
        bool isGameOn = true;

        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Console Calculator");

        Calculator calculator = new Calculator();

        while (isGameOn)
        {
            string? input1 = "";
            string? input2 = "";
            double result = 0;

            Console.WriteLine("Enter your first number: ");
            input1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(input1, out cleanNum1))
            {
                Console.Write("Invalid input. Please enter a numeric value: ");
                input1 = Console.ReadLine();
            }

            Console.WriteLine("Enter your second number: ");
            input2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(input2, out cleanNum2))
            {
                Console.Write("Invalid input. Please enter a numeric value: ");
                input2 = Console.ReadLine();
            }

            Console.WriteLine("What do you want to do?");
            Console.WriteLine("\t [A]dd numbers");
            Console.WriteLine("\t [S]ubtract numbers");
            Console.WriteLine("\t [M]ultiple numbers");
            Console.WriteLine("\t [D]ivide numbers");
            Console.WriteLine("\t [E]xit");

            string? choice = Console.ReadLine().Trim().ToLower();

            if (choice == null || !Regex.IsMatch(choice, "[a|s|m|d]"))
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
            else
            {
                try
                {
                    result = calculator.Calculate(cleanNum1, cleanNum2, choice);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }

            Console.WriteLine("----------------------------------------------------\n");
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") isGameOn = false;
            Console.WriteLine("\n");
        }

        calculator.Finish();

        return;
    }
}