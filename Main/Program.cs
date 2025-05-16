using CalculatorLibrary;
using System.Text.RegularExpressions;

bool endApp = false;
int timesUsed = 0;
// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");
Calculator calculator = new();

while (!endApp)
{
    timesUsed++;
    // Declare variables and set to empty.
    // Use Nullable types (with ?) to match type of System.Console.ReadLine
    string? numInput1;
    string? numInput2;
    double result;
    Console.Write($"You have used the calculator {timesUsed} times.\n");
    // Ask the user to type the first number
    Console.Write("Type a number");
    if (calculator.HasLastOperation())
    {
        Console.Write($", or type \"last\" to use last number ({calculator.GetLastOperation():0.##})");
    }
    Console.Write(", and then press Enter:");
    numInput1 = Console.ReadLine();
    double cleanNum1 = 0;

    if (numInput1 == "last" && calculator.HasLastOperation())
    {
        cleanNum1 = calculator.GetLastOperation();
    }
    else
    {
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }
    }

    // Ask the user to type the second number.
    Console.Write("Type another number, and then press Enter, or choose an operation from the following list:\n");
    Console.WriteLine("\tsqrt - Square root");
    Console.WriteLine("\t10x - 10x");
    Console.WriteLine("\tsin - Sin");
    Console.WriteLine("\tcos - Cos");
    Console.WriteLine("\ttan - Tan");

    numInput2 = Console.ReadLine();
    if (numInput2 != null && Regex.IsMatch(numInput2, "^(sqrt|10x|sin|cos|tan)$"))
    {
        try
        {
            result = calculator.DoOperation(numInput2, cleanNum1);
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
    else
    {
        double cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }

        // Ask the user to choose an operator.
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tn - Power of");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|n]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            try
            {
                result = calculator.DoOperation(op, cleanNum1, cleanNum2);
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
    }

    Console.WriteLine("------------------------\n");

    // Wait for the user to respond before closing.
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n"); // Friendly linespacing.
}
calculator.Finish();
return;