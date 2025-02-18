using System.Text.RegularExpressions;

CalculatorLibrary.Calculator calculator = new ();

while (true)
{
    // Declare variables and then initialize to zero.
    double num1 = 0; double num2 = 0;

    // Display title as the C# console calculator app.
    Console.Clear();
    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("------------------------\n");

    // Ask the user to type the first number.
    Console.WriteLine("Type a number, and then press Enter");
    while (true)
    {
        var input = Console.ReadLine();
        if (double.TryParse(input, out num1))
            break;
        Console.WriteLine($"\"{input}\" is not a valid number, insert a number and then press Enter");
    }

    // Ask the user to type the second number.
    Console.WriteLine("Type another number, and then press Enter");
    while (true)
    {
        var input = Console.ReadLine();
        if (double.TryParse(input, out num2))
            break;
        Console.WriteLine($"\"{input}\" is not a valid number, insert a number and then press Enter");
    }

    // Ask the user to choose an option.
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");

    var op = Console.ReadLine();

    // Validate input is not null, and matches the pattern
    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
    {
        Console.WriteLine("Error: Unrecognized input.");
    }
    else
    {
        try
        {
            var result = calculator.DoOperation(num1, num2, op);

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
    Console.Write("Press \"n\" key to close the Calculator console app or any other key to continue...");
    if (Console.ReadKey(true).Key == ConsoleKey.N)
    {
        calculator.Finish();
        break;
    }
}