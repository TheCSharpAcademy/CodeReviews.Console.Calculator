using CalculatorLibrary;
using System.ComponentModel.Design;
using System.Security.AccessControl;

namespace CalculatorProgram.frockett;

internal class Program
{
    CalculatorLog calculatorlog = new CalculatorLog();

    static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.ShowMenu();
        //calculator.Finish();
    }

    public static void ProcessCalcInput()
    {
        // Declare variables and set to empty.
        string? numInput1 = "";
        string? numInput2 = "";
        double result = 0;

        // Ask the user to type the first number.
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput1 = Console.ReadLine();
        }

        // Ask the user to type the second number.
        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();

        double cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput2 = Console.ReadLine();
        }

        // Ask the user to choose an operator.
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        try
        {
            result = calculatorLog.DoOperation(cleanNum1, cleanNum2, op);
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

        Console.WriteLine("------------------------\n");

        // Wait for the user to respond before closing.
        Console.Write("Press enter to return to main menu");
        Console.WriteLine("\n"); // Friendly linespacing.
        Console.ReadLine();
    }
}
