using System.Text.RegularExpressions;
using calculador.Models;

bool endApp = false;
int numberOfUses = 0;
Results operations = new Results();

// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (!endApp)
{
    // Declare variables and set to empty.
    // Use Nullable types (with ?) to match type of System.Console.ReadLine
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
    Console.WriteLine("\tl - List of previously calculations");
    Console.Write("Your option? ");

    string? op = Console.ReadLine();

    if (op != "l")
    {
        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            try
            {
                result = Calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
                operations.AddResultToList(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
   
        }
    } else
    {
        operations.ListOfResults();
    }
    Console.WriteLine("------------------------\n");
    
    numberOfUses++;
    Console.WriteLine($"The calculator was used {numberOfUses} times.");

    // Wait for the user to respond before closing.
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n"); // Friendly linespacing.
}

return;
