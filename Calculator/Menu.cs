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

                    Console.Write("Do you want to use previous calculations? (y/n): ");
                    string? previousCalc = Console.ReadLine();

                    if (previousCalc.ToLower() == "y")
                        cleanNum1 = SetFirstNumber(calculator, ref numInput1);
                    else
                        AskNumber("Type a number, and then press Enter: ",out numInput1, out cleanNum1);

                    Console.WriteLine("Choose an operator from the following list:\n");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tp - Power (a^b)");
                    Console.WriteLine("\tr - Square root");
                    Console.WriteLine("\tt - 10^a");
                    Console.WriteLine("\tsin - Sine");
                    Console.WriteLine("\tcos - Cosine");
                    Console.WriteLine("\ttan - Tangent");
                    Console.Write("\nYour option? ");

                    string? op = Console.ReadLine();

                    bool isSingleOperand = op == "r" || op == "t" || op == "sin" || op == "cos" || op == "tan";

                    if (!isSingleOperand)
                    {
                        AskNumber("Type another number, and then press Enter: ", out numInput2, out cleanNum2);
                    }

                    if (op == null || !Regex.IsMatch(op, @"^(a|s|m|d|p|r|t|sin|cos|tan)$"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    }
                    else
                    {
                        try
                        {
                            var result = calculator.DoOperation(cleanNum1, cleanNum2, op);

                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation resulted in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine($"\nYour result: {result:0.##}\n");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred: " + e.Message);
                        }
                    }
                    Console.WriteLine("------------------------\n");

                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
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
        return;
    }

    private static double SetFirstNumber(Calculator calculator, ref string? numInput1)
    {
        double cleanNum1;
        if (!calculator.HasHistory())
        {
            Console.WriteLine("There aren't previous calculations");
            AskNumber("Type a number, and then press Enter: ",out numInput1, out cleanNum1);
        }
        else
        {
            calculator.ShowHistory();
            Console.WriteLine("\nSelect the calculation by its ID: ");
            string? inputId = Console.ReadLine();
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
                AskNumber("Type a number, and then press Enter: " ,out numInput1, out cleanNum1);
            }
            else
            {
                cleanNum1 = calculation.Result;
                Console.WriteLine($"\nYou selected: {cleanNum1}");
            }
        }

        return cleanNum1;
    }

    private static void AskNumber(string message,out string? numInput1, out double cleanNum1)
    {
        // Ask the user to type the first number.
        Console.Write(message);
        numInput1 = Console.ReadLine();

        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput1 = Console.ReadLine();
        }
    }
}

