using CalculatorLibrary;

bool endApp = false;
Calculator calculator = new();

while (!endApp)
{
    Console.Clear();
    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("------------------------");
    Console.WriteLine($"Calculator used {calculator.Iteration} times\n");

    string numInput1 = "";
    string numInput2 = "";
    double result = 0;

    if (calculator.GetHistoryCount() == 0)
    {
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();
    }
    else 
    {
        Console.WriteLine("Type a number or 'p' to get a previous result, and then press Enter: ");
        numInput1 = Console.ReadLine();

        if (numInput1 == "p")
        {
            numInput1 = calculator.PreviousResult().ToString();
        }
    }

    double cleanNum1 = 0;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
    }

    if (calculator.GetHistoryCount() == 0) 
    {
        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Type another number or 'p' to get a previous result, and then press Enter: ");
        numInput2 = Console.ReadLine();

        if (numInput2 == "p")
        {
            numInput2 = calculator.PreviousResult().ToString();
        }
    }

    double cleanNum2 = 0;
    while (!double.TryParse(numInput2, out cleanNum2))
    {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
    }

    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");

    string op = Console.ReadLine();

    try
    {
        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error. \n");
        }
        else
        {
            Console.WriteLine("Your result: {0:0.##}\n", result);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + ex.Message);
    }

    Console.WriteLine("------------------------\n");

    Console.Write("Press\n'n' close the app,\n'h' for history\nany other key to continue: ");
    string input = Console.ReadLine();

    if (input == "n") endApp = true;
    if (input == "h") calculator.History();

    Console.WriteLine("\n");
}

calculator.Finish();