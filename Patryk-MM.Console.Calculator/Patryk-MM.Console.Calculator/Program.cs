using CalculatorLibrary;
using System.Text.RegularExpressions;

Calculator calculator = new Calculator();
bool endApp = false;
//Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (!endApp) {
    //Declare variables and set to empty.
    //Use Nullable types to match type of System.Console.ReadLine
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;

    //Ask the user to input the first number.
    Console.Write("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();

    double cleanNum1 = 0;
    while (!double.TryParse(numInput1, out cleanNum1)) {
        Console.Write("This is not a valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
    }

    //Ask the user to input the second number.
    Console.Write("Type a number, and then press Enter: ");
    numInput2 = Console.ReadLine();

    double cleanNum2 = 0;
    while (!double.TryParse(numInput2, out cleanNum2)) {
        Console.Write("This is not a valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
    }

    // Ask the user to choose an operator.
    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option? ");

    string? op = Console.ReadLine();

    //Validate if input is not null and matches the pattern
    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]")) {
        Console.WriteLine("Error: Unrecognized input.");
    } else {
        try {
            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result)) {
                Console.WriteLine("Your operation will result in a mathematical error.");
            } else Console.WriteLine($"Your result: {result:0.##}");
        } catch (Exception e) { Console.WriteLine("An error occured during the operation.\n" + e.Message); }
    }
    Console.WriteLine("------------------------\n");

    //Wait for the user to respond before closing.
    Console.Write("Press n to close the app, press any other key to continue to next operation. ");
    if (Console.ReadLine() == "n") endApp = true;
    Console.WriteLine("\n");
 
}

calculator.Finish();

