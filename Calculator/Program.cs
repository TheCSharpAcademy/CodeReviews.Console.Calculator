using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        string? userOption = "";

        while (!endApp)
        {
            switch (userOption)
            {
                case "cl":
                    calculator.ClearHistory();
                    System.Console.WriteLine("History cleared.");
                    break;
                default:
                    UserCalculation(calculator);
                    break;
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, 'cl' to clear the result history, or press any other key and Enter to continue: ");
            userOption = Console.ReadLine();
            if (userOption == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        // Add call to close the JSON writer before return
        calculator.Finish();

        return;
    }

    static void UserCalculation(Calculator calculator)
    {
        // Declare variables and set to empty.
        double result = 0;

        // Ask the user to choose an option.
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tp - Power");
        Console.WriteLine("\tq - Square Root");
        Console.WriteLine("\tt - 10x");
        Console.WriteLine("\ti - Sin");
        Console.WriteLine("\to - Cos");
        Console.WriteLine("\tn - Tan");
        Console.Write("Your option? ");

        string? op = Console.ReadLine();

        // Validate input is not null, and matches and pattern
        if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|q|t|i|o|n]"))
        {
            Console.WriteLine("Error: Unrecognized input.");
        }
        else
        {
            // Ask the user to type the first number.
            double cleanNum1 = RetrieveNumberFromUserInput(calculator, true);

            double cleanNum2 = 0;
            if (Regex.IsMatch(op, "[a|s|m|d|p]"))
            {
                // Ask the user to type the second number.
                cleanNum2 = RetrieveNumberFromUserInput(calculator, false);
            }

            try
            {
                if (Regex.IsMatch(op, "[a|s|m|d|p]"))
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                else
                    result = calculator.DoSingleInputOperation(cleanNum1, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
            }
        }
    }

    /** 
    * Returns true if the function is able to retrieve a value from the history,
    * otherwise it returns false (e.g. when the history is empty)
    */
    private static bool SelectResultFromHistory(Calculator calculator, out double previousResult)
    {
        List<double> resultHistory = calculator.GetResultHistory();


        if (resultHistory.Count == 0)
        {
            System.Console.WriteLine("The history is empty!");

            previousResult = 0;
            return false;
        }

        for (int i = 0; i < resultHistory.Count; i++)
        {
            System.Console.WriteLine($"({i + 1}): {resultHistory[i]}");
        }

        System.Console.WriteLine("Which result to load?");
        string? userInput = Console.ReadLine();
        int historySelection;
        while (!int.TryParse(userInput, out historySelection))
        {
            System.Console.WriteLine("This is not a valid input. Please enter a numberic value between 1 and {0}:", resultHistory.Count);
            userInput = Console.ReadLine();
        }

        previousResult = resultHistory[historySelection - 1];

        return true;
    }

    private static double RetrieveNumberFromUserInput(Calculator calculator, bool isFirstInput)
    {
        // Use Nullable types (with ?) to match type of System.Console.ReadLine
        string? numInput = "";

        Console.WriteLine("Type {0} number or write 'h' (to see result history), and then press Enter", (isFirstInput ? "a" : "another"));
        numInput = Console.ReadLine();

        double cleanNum = 0;
        bool retrievedFromHistory = false;
        if (numInput == "h")
        {
            retrievedFromHistory = SelectResultFromHistory(calculator, out cleanNum);
        }

        while (!retrievedFromHistory && !double.TryParse(numInput, out cleanNum))
        {
            System.Console.WriteLine("This is not a valid input. Please enter a numberic value:");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }
}


