// See https://aka.ms/new-console-template for more information
// Declare variables and then initialize to zero.
using System.Text.RegularExpressions;
using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.Title = "Console Calculator in C#\\";

        Calculator calculator = new();
        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\tc - Calculate two numbers");
            Console.WriteLine("\ts - Show a list of last calculations");
            Console.WriteLine("\td - Delete the list of last calculations");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[c|s|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
                try
                {
                    Console.Clear();
                    switch (op)
                    {
                        case "c": Calculate(ref calculator); break;
                        case "s": calculator.ShowList(); break;
                        case "d": calculator.DeleteList(); break;
                        default: break;
                    }
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
        calculator.Finish();
        return;
    }
    private static void Calculate(ref Calculator calculator)
    {
        // Declare variables and set to empty.
        // Use Nullable types (with ?) to match type of System.Console.ReadLine
        //string? numInput1 = "";
        //string? numInput2 = "";
        double result = 0;

        // Ask the user to type the first number.
        Console.WriteLine("Type a number, and then press Enter: ");
        Console.WriteLine("(Type 'list' to retreive a number from your last calculations)");
        double cleanNum1 = selectOperand(ref calculator);

        // Ask the user to type the second number.
        Console.WriteLine("Type another number, and then press Enter: ");
        Console.WriteLine("(Type 'list' to retreive a number from your last calculations)");
        double cleanNum2 = selectOperand(ref calculator);

        // Ask the user to choose an operator.
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tp - Power");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        // Validate input is not null, and matches the pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            try
            {
                result = calculator.DoOperation((double)cleanNum1, (double)cleanNum2, op);
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
    // supports either reading a number or selecting from the list
    // (motivation: user loses the 'selecting from a list' functionality if
    // he provided an invalid input the first time (i.e input = "lisr")
    private static double selectOperand(ref Calculator calculator)
    {
        string? numInput;
        double? num = null;
        while (num == null)
        {
            numInput = Console.ReadLine();
            if ("list".Equals(numInput))
                num = selectFromList(ref calculator);
            else if (double.TryParse(numInput, out double clean))
                num = clean;
            else
                Console.WriteLine("This is not valid input. Please enter a numeric value: ");

            if (num == null) Console.WriteLine("try again");
        }
        return (double)num;
    }
    // Returns null if the number is not in the list (i.e list is empty)
    private static double? selectFromList(ref Calculator calculator)
    {
        calculator.ShowList();
        int? choice = Input.readInt(1, calculator.calculations.Count);
        return (choice == null) ? null :
        calculator.calculations.ElementAt((int)choice - 1).result;
    }
}

    
