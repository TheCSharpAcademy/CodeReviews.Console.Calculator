using CalculatorLibrary;

var isAppRunning = true;
var calculator = new Calculator();

Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (isAppRunning)
{
    Console.WriteLine("Type a number, and then press Enter");
    var num1 = GetValidDouble();

    Console.WriteLine("Type another number, and then press Enter");
    var num2 = GetValidDouble();

    DisplayMenu();

    var selectedOption = GetValidMenuOption();

    switch (selectedOption)
    {
        case "a":
        case "s":
        case "m":
        case "d":
        {
            if (selectedOption == "d")
            {
                while (num2 == 0)
                {
                    Console.WriteLine("Enter a non zero divisor: ");
                    num2 = GetValidDouble();
                }
            }

            try
            {
                var result =
                    calculator.PerformOperation(num1, num2, OperationMapper.StringToOperation(selectedOption));
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }

                Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            break;
        }
        case "n":
        {
            isAppRunning = false;
            break;
        }
    }

    Console.WriteLine("------------------------\n");
    if (selectedOption != "n")
    {
        Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
        if (Console.ReadLine() == "n")
        {
            isAppRunning = false;
        }
    }

    Console.WriteLine("\n");
}

Console.Write("Press any key to close the Calculator console app...");
Console.ReadKey();

calculator.Finish();

return;

double GetValidDouble()
{
    double formattedInput;

    while (!double.TryParse(Console.ReadLine(), out formattedInput))
    {
        Console.WriteLine("Please enter a valid number.");
    }

    return formattedInput;
}

string GetValidMenuOption()
{
    var availableOptions = new List<string> { "a", "s", "m", "d", "n" };
    var input = Console.ReadLine();

    while (!(input is not null && availableOptions.Contains(input.Trim().ToLower())))
    {
        Console.Write("Please enter a valid menu option: ");
        input = Console.ReadLine();
        Console.WriteLine();
    }

    return input;
}

void DisplayMenu()
{
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\tn - Exit");
    Console.Write("Your option? ");
}