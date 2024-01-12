using CalculatorLibrary;

namespace CalculatorProgram.frockett;

internal class Engine
{
    ListFunctions listFunctions = new ListFunctions();
    CalculatorLog log = new CalculatorLog();

    bool endApp;
    int totalComputations;

    public void ShowMenu()
    {
        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("\t1. New Calculation");
            Console.WriteLine("\t2. View History");
            Console.WriteLine("\t3. Clear History");
            Console.WriteLine("\t4. Exit Application\n");

            string? readResult = Console.ReadLine();
            int menuSelection = 0;
            while (!int.TryParse(readResult, out menuSelection))
            {
                Console.WriteLine("Input a valid integer menu selection");
                readResult = Console.ReadLine();
            }
            switch (menuSelection)
            {
                case 1:
                    Console.Clear();
                    ProcessCalcInput();
                    break;
                case 2:
                    Console.Clear();
                    listFunctions.PrintList();
                    break;
                case 3:
                    Console.Clear();
                    listFunctions.ClearList();
                    break;
                case 4:
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid input, please enter a valid integer menu selection");
                    break;
            }
        }
        // Add call to close JSON writer before return
        log.Finish(totalComputations);
        return;
    }

    public void ProcessCalcInput()
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
        Console.WriteLine("\nChoose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide\n");
        Console.Write("Your option? \n");

        string? op = Console.ReadLine();

        try
        {
            result = DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else Console.WriteLine("\nYour result: {0:0.##}\n", result);
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

    public double DoOperation(double num1, double num2, string op)
    {
        string operation = "";
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                operation = "Add";
                break;
            case "s":
                result = num1 - num2;
                operation = "Subtract";
                break;
            case "m":
                result = num1 * num2;
                operation = "Multiply";
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                operation = "Divide";
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        log.WriteToLog(num1, num2, operation, result);
        // Add to tally
        totalComputations++;
        // Write to list for user access during runtime
        listFunctions.WriteList(num1, num2, operation, result, totalComputations);
        return result;
    }
}