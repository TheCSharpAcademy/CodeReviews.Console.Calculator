using CalculatorLibrary;
using System.Text.RegularExpressions;

bool endApp = false;
Calculator calculator = new Calculator();
int timesUsed = 0; // tracker for the amount of times the calc was used

// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (!endApp)
{
    bool memoryEmpty = calculator.IsMemoryEmpty();
    double result = 0;

    CalculatorInterface.DisplayMenu();

    Console.Write("Selected option: ");
    string? op = Console.ReadLine();

    // Validate input is not null, and matches the pattern
    if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|sqrt|pow|x10|sin|cos|tg|cotg)$"))
    {
        Console.WriteLine("Error: Unrecognized input.");
    }
    else
    {
        bool requiresTwoOperands = Calculator.TwoOperandsRequired(op);
        double[] operands = CalculatorInterface.ParseOperands(memoryEmpty, calculator, requiresTwoOperands);

        try
        {
            result = calculator.DoOperation(operands[0], operands[1], op);

            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
    }

    Console.WriteLine("------------------------\n");

    // Wait for the user to respond before closing.
    Console.WriteLine($"Amount of times the calculator was used: {++timesUsed}");
    Console.Write("Press 'n' and Enter to close the app, press 'd' to delete calculator memory, or press any other key and Enter to continue: ");

    string? action = Console.ReadLine();

    switch (action)
    {
        case "n":
            endApp = true;
            break;
        case "d":
            calculator.DeleteMemory();
            break;
        default:
            calculator.AddResultToMemory(result);
            break;
    }

    Console.WriteLine("\n"); // Friendly linespacing.
}

calculator.Finish(); // JSON writer closing call
return;
