using System.Text.RegularExpressions;
using CalculatorLibrary;
internal class Menu
{
    public static void ShowMenu()
    {
        bool endApp = false;

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            Console.Clear();
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1 - New Calculation");
            Console.WriteLine("2 - View History");
            Console.WriteLine("3 - Clear History");
            Console.WriteLine("4 - Exit");
            Console.Write("\nYour choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    // Declare variables and set to empty.
                    // Use Nullable types (with ?) to match type of System.Console.ReadLine
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double cleanNum1 = 0;
                    double cleanNum2 = 0;

                    double result = 0;

                    Console.Write("Do you want to use previous calculations? (y/n): ");
                    var previousCalc = Console.ReadLine();

                    if (previousCalc.ToLower() == "y")
                    {
                        cleanNum1 = SetFirstNumber(calculator, ref numInput1);
                    }
                    else
                    {
                        AskFirstNumber(out numInput1, out cleanNum1);
                    }

                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }

                    // Ask the user to choose an operator.
                    Console.WriteLine("Choose an operator from the following list:\n");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.Write("\nYour option? ");

                    string? op = Console.ReadLine();

                    // Validate input is not null, and matches the pattern
                    if (op == null || !Regex.IsMatch(op, "^[asmd]$"))
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
                            else Console.WriteLine("\nYour result: {0:0.##}\n", result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    Console.WriteLine("------------------------\n");

                    // Wait for the user to respond before closing.
                    Console.Write("Press any key to continue...");
                    if (Console.ReadLine() == "n") continue;

                    Console.WriteLine("\n"); // Friendly linespacing.
                    break;
                case "2":
                    calculator.ShowHistory();
                    Console.Write("\nPress any key to return to menu...");
                    Console.ReadKey();
                    break;
                case "3":
                    calculator.ClearHistory();
                    Console.WriteLine("History cleared.");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                    break;
                case "4":
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
        // Add call to close the JSON writer before return
        calculator.Finish();
        return;
    }

    private static double SetFirstNumber(Calculator calculator, ref string? numInput1)
    {
        double cleanNum1;
        if (!calculator.HasHistory())
        {
            Console.WriteLine("There aren't previous calculations");
            AskFirstNumber(out numInput1, out cleanNum1);
        }
        else
        {
            calculator.ShowHistory();
            Console.WriteLine("\nSelect the calculation by its ID: ");
            var inputId = Console.ReadLine();
            int id;

            while (!int.TryParse(inputId, out id))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                inputId = Console.ReadLine();
            }

            var calculation = calculator.GetCalculationById(id);
            if (calculation == null)
            {
                Console.WriteLine("No calculation found with that ID.");
                AskFirstNumber(out numInput1, out cleanNum1);
            }
            else
            {
                cleanNum1 = calculation.Result;
                Console.WriteLine($"\nYou selected: {cleanNum1}");
            }
        }

        return cleanNum1;
    }

    private static void AskFirstNumber(out string? numInput1, out double cleanNum1)
    {
        // Ask the user to type the first number.
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }
    }
}

