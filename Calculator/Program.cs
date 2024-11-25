using System.Text.RegularExpressions;
using CalculatorLibrary;

var endApp = false;
var calculator = new CalculatorLib();

Console.WriteLine("Console Calculator in C#\n");
Console.WriteLine("------------------------\n");

while (!endApp)
{
    Console.WriteLine("Type a number, and then press Enter");
    var numInput1 = Console.ReadLine();

    double cleanNum1;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not a valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
    }

    Console.WriteLine("Type another number, and then press Enter");
    var numInput2 = Console.ReadLine();

    double cleanNum2;
    while (!double.TryParse(numInput2, out cleanNum2))
    {
        Console.Write("This is not a valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
    }

    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");

    var op = Console.ReadLine();
    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
        Console.WriteLine("Error: Unrecognized input.");
    else
        try
        {
            var result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result))
                Console.WriteLine("This operation will result in a math error.\n");
            else
                Console.WriteLine($"Your result: {result:0.##}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + e.Message);
        }

    Console.WriteLine("------------------------\n");
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");

    if (Console.ReadLine() == "n")
        endApp = true;

    Console.WriteLine("\n");
}

calculator.Finish();