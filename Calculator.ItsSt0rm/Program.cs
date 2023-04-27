using CalculatorLibrary;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            string menuOption = "";
            string operationSelected = "";
            double previousOperation = double.NaN;
            double result = 0;

            // Shows first menu
            calculator.showMenuOptions();

            menuOption = Console.ReadLine();
            Console.WriteLine(""); // Friendly linespacing.       
            while (!calculator.ReadMenuOptions(menuOption))
            {
                Console.Write("This is not valid input. Please enter an available option: ");
                menuOption = Console.ReadLine();
            }

            if (menuOption.Equals("n"))
            {
                return;
            }

            // Ask the user to type the first number.           
            double cleanNum1 = calculator.ReadNumber();
            Console.WriteLine("First number saved");

            // Ask the user to type the second number.
            double cleanNum2 = calculator.ReadNumber();
           Console.WriteLine("Second number saved");

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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
            Console.WriteLine("Total operations done: {0}\n", calculator.OperationsDone);
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        // Add call to close the JSON writer before return
        calculator.Finish();
        return;
    }
}