using System.Text.RegularExpressions;
using CalculatorLibrary;

bool endApp = false;

Console.WriteLine("Console calculator");
Console.WriteLine("------------------------\n");

Calculator calculator = new Calculator();

while (!endApp)
{
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;

    Console.Write("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();

    double cleanNum1 = 0;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
    }

    Console.Write("Type another number, and then press Enter: ");
    numInput2 = Console.ReadLine();

    double cleanNum2 = 0;
    while (!double.TryParse(numInput2, out cleanNum2))
    {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
    }

    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");


    string? op = Console.ReadLine();

    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
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
                Console.WriteLine("The Operation will result in Mathematical error\n");
            }
            else
            {
                Console.WriteLine($"Result: {result:0.##}\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured!" + e);
        }
    }

    Console.WriteLine("------------------------\n");

    // Wait for the user to respond before closing.
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n"); // Friendly linespacing.

}