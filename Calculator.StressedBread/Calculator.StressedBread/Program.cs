using System.Text.RegularExpressions;
using Calculator.StressedBread;

bool endApp = false;

Console.WriteLine("Console Calculator\r");
Console.WriteLine("------------------\n");

while (!endApp)
{
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;

    Console.WriteLine("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();

    double cleanNum1 = 0;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
    }

    Console.WriteLine("Type a number, and then press Enter: ");
    numInput2 = Console.ReadLine();

    double cleanNum2 = 0;
    while (!double.TryParse(numInput2, out cleanNum2))
    {
        Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
    }

    Console.WriteLine(@"Choose an operator from the following list:
a - Add
s - Subtract
m - Multiply
d - Divide
Your option?");

    string? op = Console.ReadLine();

    if (op == null || ! Regex.IsMatch(op, "[asmd]"))
    {
        Console.WriteLine("Error: Unrecognized input.");
    }
    else
    {
        try
        {
            result = CalculatorBrain.DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.####}\n", result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("And exception occured trying to do the math. \n - Details: " + ex.Message);
        }
    }
    Console.WriteLine("------------------\n");

    Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n");
}
return;