using System.Text.RegularExpressions;
using CalculatorLibrary;

bool endApp = false;
int counter = 0;
List<string> operations = new List<string>();
List<double> results = new List<double>();

Calculator calculator = new Calculator();

do
{
    Console.Clear(); // Clear the console each time the user makes a selection.

    // Display title as the C# console calculator app.
    Console.WriteLine("====================================================");
    Console.WriteLine("Console Calculator in C#\r");
    Console.WriteLine("====================================================");


    // Declare variables and set to empty.
    // Use Nullable types (with ?) to match type of System.Console.ReadLine
    string? numInput1 = "";
    string? numInput2 = "";
    double result = 0;
    counter++;

    // Ask the user to type the first number.
    Console.WriteLine("Type a number (or 'v' to select from history), and then press Enter: ");
    numInput1 = Console.ReadLine();
    Console.WriteLine("====================================================");
    double cleanNum1 = 0;

    if (!double.TryParse(numInput1, out cleanNum1) && operations.Count < 1)
    {
        Console.WriteLine("History Empty. . .");
        Console.WriteLine("====================================================");
    }
    else
    {
        switch (numInput1)
        {
            case "v":
                Console.WriteLine("Viewing History. . .");
                Thread.Sleep(1000);
                Console.WriteLine("====================================================");
                calculator.PrintOperations(operations);
                Console.WriteLine("====================================================");
                Console.WriteLine("Press Enter key to continue. . .");
                Console.ReadKey();
                break;
            default:
                break;
        }
    }

    while (!double.TryParse(numInput1, out cleanNum1))
    {
        Console.Write("This is not a valid input. Please enter a numeric value: ");
        numInput1 = Console.ReadLine();
    }

    // Ask the user to type a second number.
    Console.WriteLine("Type another number (or 'v' to select from history), and then press Enter: ");
    numInput2 = Console.ReadLine();
    Console.WriteLine("====================================================");
    double cleanNum2 = 0;

    if (!double.TryParse(numInput2, out cleanNum2) && operations.Count < 1)
    {
        Console.WriteLine("History Empty. . .");
        Console.WriteLine("====================================================");
    }
    else
    {
        switch (numInput1)
        {
            case "v":
                Console.WriteLine("Viewing History. . .");
                Thread.Sleep(1000);
                Console.WriteLine("====================================================");
                calculator.PrintOperations(operations);
                Console.WriteLine("====================================================");
                Console.WriteLine("Press Enter key to continue. . .");
                Console.ReadKey();
                break;
            default:
                break;
        }
    }

    while (!double.TryParse(numInput2, out cleanNum2))
    {
        Console.Write("This is not a valid input. Please enter a numeric value: ");
        numInput2 = Console.ReadLine();
    }

    // Ask the user to choose an operator.
    Console.WriteLine("Choose an operator from the following list: ");
    Console.WriteLine("\ta - Add");
    Console.WriteLine("\ts - Subtract");
    Console.WriteLine("\tm - Multiply");
    Console.WriteLine("\td - Divide");
    Console.Write("Your option: ");
    string? op = Console.ReadLine();
    Console.WriteLine("====================================================");


    // Validate input is not null, and matches the pattern
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
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                string opSymbol = "";
                switch (op)
                {
                    case "a":
                        opSymbol = "+";
                        break;
                    case "s":
                        opSymbol = "-";
                        break;
                    case "m":
                        opSymbol = "*";
                        break;
                    case "d":
                        opSymbol = "/";
                        break;
                }
                string answer = $"{cleanNum1} {opSymbol} {cleanNum2} = {result}";
                Console.WriteLine("Your result: {0}", result);
                operations = calculator.AddOperation(operations, answer);

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
        }
    }

    Console.WriteLine("====================================================");
    Console.WriteLine("Total Calculations: {0}", counter);
    Console.WriteLine("====================================================");

    // Wait for the user to respond before closing.
    Console.WriteLine("Press 'r' to reset counter and delete history");
    Console.WriteLine("Press 'v' to view previous calculations");
    Console.WriteLine("Press 'n' To Exit");
    Console.WriteLine("Press Enter other key to continue. . .");
    Console.WriteLine("====================================================");
    

    switch (Console.ReadLine())
    {
        case "n":
            Console.WriteLine("Exiting. . .");
            Thread.Sleep(500);
            endApp = true;
            break;

        case "r":
            Console.WriteLine("Clearing History. . .");
            counter = 0;
            calculator.ClearOperations(operations);
            Thread.Sleep(1000);
            Console.WriteLine("====================================================");
            Console.WriteLine("History Cleared. . .");
            Console.WriteLine("====================================================");
            Console.WriteLine("Total Calculations: {0}", counter);
            Console.WriteLine("====================================================");
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
            break;

        case "v":
            Console.WriteLine("Viewing History. . .");
            Thread.Sleep(1000);
            Console.WriteLine("====================================================");
            calculator.PrintOperations(operations);
            Console.WriteLine("====================================================");
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
            break;
        default:
            break;
    }

    Console.WriteLine("\n"); // Friendly linespacing.

} while (!endApp);

// Add call to close the JSON writer before return
calculator.Finish();
return;