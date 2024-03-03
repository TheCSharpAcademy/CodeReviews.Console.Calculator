﻿using CalculatorLibrary;

var endApp = false;
var calculator = new Calculator();
// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (!endApp)
{
    // Declare variables and set to empty.
    string numInput1;
    string numInput2;
    double result;

    // Ask the user to choose an operator.
    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\tsqrt - Square Root");
    Console.WriteLine("\tpow - Power");
    Console.WriteLine("\ttenx - Ten X");
    Console.WriteLine("\tsin - Sine");
    Console.WriteLine("\tcos - Cosine");
    Console.WriteLine("\ttan - Tangent");
    Console.WriteLine("\th - History");
    Console.Write("Your option? ");
    
    var op = Console.ReadLine();

    if (op == "h")
    {
        Console.WriteLine($"You used the calculator {calculator.UsageCount} times this session.");
        if (calculator.CalculationHistory.Count > 0)
        {
            calculator.CalculationHistory.ForEach(Console.WriteLine);
        }
        else
        {
            Console.WriteLine("History is empty.");
        }
        Console.ReadKey();
        continue;
    }
    
    // Ask the user to type the first number.
    Console.Write("Type a number, and then press Enter: ");
    numInput1 = Console.ReadLine();

    double cleanNum1;
    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not valid input. Please enter an integer value: ");
        numInput1 = Console.ReadLine();
    }

    double cleanNum2 = 0;
    if (op is "a" or "s" or "m" or "d" or "pow")
    {
        // Ask the user to type the second number.
        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();
        
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput2 = Console.ReadLine();
        }
    }

    try
    {
        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
        if (double.IsNaN(result))
            Console.WriteLine("This operation will result in a mathematical error.\n");
        else Console.WriteLine("Your result: {0:0.##}\n", result);
    }
    catch (Exception e)
    {
        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
    }

    Console.WriteLine("------------------------\n");

    // Wait for the user to respond before closing.
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n") endApp = true;

    Console.WriteLine("\n"); // Friendly linespacing.
}

// Add call to close the JSON writer before return
calculator.Finish();