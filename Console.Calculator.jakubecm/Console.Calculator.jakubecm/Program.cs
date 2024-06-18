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
    // Declare variables and set to empty.
    // Use Nullable types (with ?) to match type of System.Console.ReadLine
    string? numInput1 = "";
    string? numInput2 = "";
    bool memoryEmpty = calculator.IsMemoryEmpty();
    double cleanNum1 = 0;
    double cleanNum2 = 0;
    double result = 0;


    // Ask the user to type the first number.
    Console.Write("Type a number, or M to select result from memory (if not empty), and then press Enter: ");
    numInput1 = Console.ReadLine();

    if (numInput1!.ToLower() == "m" && !memoryEmpty)
    {
        cleanNum1 = calculator.SelectResultFromMemory();
    }
    else
    {
        cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }
    }

    // Ask the user to type the second number.
    Console.Write("Type another number, or M to select result from memory (if not empty), and then press Enter: ");
    numInput2 = Console.ReadLine();

    if (numInput2!.ToLower() == "m" && !memoryEmpty)
    {
        cleanNum2 = calculator.SelectResultFromMemory();
    }
    else
    {
        cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput2 = Console.ReadLine();
        }
    }
    

    // Ask the user to choose an operator.
    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\troot - Nth root (second numbers root of the first)");
    Console.WriteLine("\tpow - Power (first num to the power of second)");
    Console.WriteLine("\tt - 10x (will use the first entered number)");
    Console.WriteLine("\tsin - Sinus (will use the first entered number)");
    Console.WriteLine("\tcos - Cosinus (will use the first entered number)");
    Console.WriteLine("\ttg - Tangens (will use the first entered number)");
    Console.WriteLine("\tcotg - Cotangens (will use the first entered number)");
    Console.Write("Your option? ");

    string? op = Console.ReadLine();

    // Validate input is not null, and matches the pattern
    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sqrt|pow|t|sin|cos|tg|cotg]"))
    {
        Console.WriteLine("Error: Unrecognized input.");
    }
    else
    {
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
