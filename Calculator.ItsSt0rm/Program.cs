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
            while (calculator.readMenuOptions(menuOption))
            {
                calculator.showMenuOptions();
                menuOption = Console.ReadLine();
            }

            // Ask the user to type the first number.
            Console.Write("Type a number or type h to use a previous result, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            bool breakLoopFirstNumber = false;
            while (!breakLoopFirstNumber)
            {
                double.TryParse(numInput1, out cleanNum1);
                if (numInput1.Equals("h"))
                {
                    calculator.showLatestCalculations();
                    if (calculator.latestCalculationsCount() > 0)
                    {
                        int operationSelectedClean1 = 0;

                        Console.Write("Choose the number of the operation that you want to use: ");
                        operationSelected = Console.ReadLine();
                                               
                        while (!int.TryParse(operationSelected, out operationSelectedClean1) || double.IsNaN(previousOperation = calculator.previousOperationResult(operationSelectedClean1)))
                        {
                            Console.Write("This is not valid input. Please enter an available option: ");
                            operationSelected = Console.ReadLine();                            
                        }

                        cleanNum1 = previousOperation;
                        break;
                    }
                    else
                    {
                        Console.Write("Type a number and then press Enter: ");
                    }
                }
                else if (double.IsNormal(cleanNum1))
                {
                    break;
                }
                else
                {
                    Console.Write("This is not valid input. Please enter a valid value: ");
                }

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