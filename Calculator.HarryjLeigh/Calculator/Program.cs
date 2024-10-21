
using System.Text.RegularExpressions;
using CalculatorLibrary;

bool endApp = false;
Calculator calculator = new Calculator();
// Display title as the C# console calculator app.
Console.WriteLine("Console Calculator in C#\r");
Console.WriteLine("------------------------\n");

while (!endApp)
{
    // Declare variables and set to empty.
    // Use Nullable types (with ?) to match type of System.Console.ReadLine
    string? numInput1 = "";
    string? numInput2 = "";
    double cleanNum1 = 0;
    double cleanNum2 = 0;
    double result = 0;
    string? op = "";
    string? previousCalculationInput = "";



    // Ask user if they would like to delete previous calculations
    if (calculator.PreviousCalculations.Count > 0)
    {
        Console.WriteLine("Delete previous calculations?  (y/n) ");
        string? input = Console.ReadLine();
        if (input == null || !Regex.IsMatch(input, "[y|n]"))
        {
            Console.WriteLine("Error: Unrecognised input.");
        }
        else
        {
            if (input == "y") { calculator.DeletePreviousCalculations(); Console.WriteLine("Previous calculations deleted."); }
        }
    }

    // Ask user if they would like to use previous calculation
    if (calculator.PreviousCalculations.Count > 0)
    {
        Console.WriteLine("Would you like to use a previous calculation? (y/n) ");
        previousCalculationInput = Console.ReadLine();
        if (previousCalculationInput == null || !Regex.IsMatch(previousCalculationInput, "[y|n]"))
        {
            Console.WriteLine("Error: Unrecognised input");
        }
    }

    // Uses previous calculation as first number
    if (previousCalculationInput == "y")
    {
        //  Ask for number from previous calculations, parses number and perfoms operation
        numInput1 = calculator.UsePreviousCalculations();
        cleanNum1 = parseStringToDouble(numInput1);
        op = chooseOperation();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|sqrt|10x|sin|cos|tan]"))
        {
            Console.WriteLine("Error: Unrecognised input.");
        }
        else
        {
            try
            {
                // Checks if input is one number operation
                if (op == "sqrt" || op == "10x" || op == "sin" || op == "cos" || op == "tan")
                {
                    result = calculator.DoOneNumberOperation(cleanNum1, op);
                }
                else
                {
                    // Runs if input is a two number operation - Asks for number, parses number and performs operation
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                    cleanNum2 = parseStringToDouble(numInput2);
                    result = calculator.DoTwoNumberOperation(cleanNum1, cleanNum2, op);
                }
                // Error checking
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
    }

    // Runs when user would like new numbers 
    if (previousCalculationInput == "" || previousCalculationInput == "n")
    {
        // Ask the user to type the first number. Parses number and performs operation
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();
        cleanNum1 = parseStringToDouble(numInput1);
        op = chooseOperation();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|sqrt|10x|sin|cos|tan]"))
        {
            Console.WriteLine("Error: Unrecognised input.");
        }
        else
        {
            try
            {
                // Checks if input is one number operation
                if (op == "sqrt" || op == "10x" || op == "sin" || op == "cos" || op == "tan")
                {
                    result = calculator.DoOneNumberOperation(cleanNum1, op);
                }
                else
                {
                    // Runs if input is a two number operation - Asks for number, parses number and performs operation
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                    cleanNum2 = parseStringToDouble(numInput2);
                    result = calculator.DoTwoNumberOperation(cleanNum1, cleanNum2, op);
                }
                // Error checking
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
    }

    Console.WriteLine("------------------------\n");

    // Wait for the user to respond before closing.
    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
    if (Console.ReadLine() == "n")
    {
        Console.WriteLine($"Number of calculations: {calculator.CalculationsCount}");
        endApp = true;
    }
    Console.WriteLine("\n"); // Friendly linespacing.
}
// Finish JSON File
calculator.Finish();

// FUNCTIONS

string? chooseOperation()
{
    // Ask the user to choose an operator.
    Console.WriteLine("Choose an operator from the following list:");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.WriteLine("\tp - Power");
    Console.WriteLine("\tsqrt - Square Root");
    Console.WriteLine("\t10x - Ten Times");
    Console.WriteLine("\tsin - Sin");
    Console.WriteLine("\tcos - Cos");
    Console.WriteLine("\ttan - Tan");
    Console.Write("Your option? ");

    string? choice = Console.ReadLine();
    return choice;
}

double parseStringToDouble(string? numInput)
{
    double cleanNum = 0;
    while (!double.TryParse(numInput, out cleanNum))
    {
        Console.Write("This is not valid input. Please enter a numeric value: ");
        numInput = Console.ReadLine();
    }
    return cleanNum;
}