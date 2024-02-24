using CalculatorLibrary;

bool endApp = false;
string? readlineResult;
Calculator calculator = new Calculator();
double cleanNum1 = 0;
int count = 0;

// Display title as the C# console calculator app.
Console.Clear();
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");
while (!endApp)
{
    // Declare variables and set to empty.
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;

    if (cleanNum1 == 0)
    {    // Ask the user to type the first number.
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput1 = Console.ReadLine();
        }
    }
    Console.Clear();
    Console.WriteLine(cleanNum1);
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
    string? op = "";
    while (string.IsNullOrEmpty(op))
    {
        Console.Clear();
        Console.WriteLine($"{cleanNum1} {cleanNum2}");
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.Write("Your option: ");
        op = Console.ReadLine();
    }
    try
    {
        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error.\n");
        }
        else
        {
            Console.Clear();
            op = calculator.Operator(op);
            Console.WriteLine($"{cleanNum1} {op} {cleanNum2} = ");
            Console.WriteLine("Your result: {0:0.##}\n", result);
            count++;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        calculator.Finish();
    }

    Console.WriteLine("------------------------\n");
    Console.WriteLine($"Calculations done:{count}");
    // Wait for the user to respond before closing.
    Console.WriteLine("Press 'n' and Enter to close the app");
    Console.WriteLine("Press 'h' and Enter to view history");
    Console.WriteLine("Press any other key and/or Enter to do another calculation");
    readlineResult = Console.ReadLine();
    if (readlineResult == "n") endApp = true;
    else if (readlineResult == "h")
    {
        cleanNum1 = calculator.ShowHistory();
    }
    else
        cleanNum1 = 0;

    Console.WriteLine("\n"); // Friendly linespacing.
}
calculator.Finish();