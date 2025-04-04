using System.Text.RegularExpressions;
using CalculatorLibrary;

bool endApp = false;

Console.WriteLine("Console calculator");
Console.WriteLine("------------------------\n");

Calculator calculator = new Calculator();

while (!endApp)
{
    double result = 0;

    double cleanNum1 = HandleUserInput();
    double cleanNum2 = HandleUserInput();

    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\tr - Square Root");
    Console.WriteLine("\tp - Power");
    Console.WriteLine("\tt - 10x");
    Console.WriteLine("\tsin - sin");
    Console.WriteLine("\tcos - cos");
    Console.WriteLine("\ttan - tan");

    Console.Write("Your option? ");


    string? op = Console.ReadLine();

    if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|t|sin|cos|tan]"))
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

double HandleUserInput()
{
    double cleanNum = double.NaN;
    string? input;

    if (calculator.HasHistory())
    {
        Console.WriteLine("Would you like to use Previous Answer? Enter H  or press any other key and Enter to continue: ");
        if (Console.ReadLine()?.ToLower().Trim() == "h")
        {
            calculator.DisplayHistory();
            int index;
            do
            {
                Console.WriteLine("Please enter a index within correct range: ");
                int.TryParse(Console.ReadLine(), out index);
                cleanNum = calculator.GetResultFromHistory(index);
            }
            while (double.IsNaN(cleanNum));

            return cleanNum;
        }

    }

    Console.Write("Type a number, and then press Enter: ");
    input = Console.ReadLine();

    while (!double.TryParse(input, out cleanNum))
    {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        input = Console.ReadLine();
    }

    return cleanNum;
}