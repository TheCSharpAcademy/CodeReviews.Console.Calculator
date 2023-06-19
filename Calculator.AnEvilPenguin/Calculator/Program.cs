using CalculatorLibrary;

internal class Program
{
    private static Calculator calculator = new Calculator();
    private static List<string> soloOperators = new List<string> { "r", "x", "i", "c", "t" };

    private static double GetHistoryValue()
    {
        int loop = 0;
        List<double> history = calculator.GetHistory();

        if (history.Count == 0)
        {
            throw new InvalidOperationException("No values in history");
        }

        history.ForEach(result =>
        {
            Console.WriteLine($"{loop}: {result}");
            loop++;
        });

        Console.WriteLine();
        Console.WriteLine("Select a historic value");
        string input = Console.ReadLine();
        int selection = 0;

        while (!int.TryParse(input, out selection))
        {
            input = Console.ReadLine();
        }

        return history[selection];
    }

    private static void Main(string[] args)
    {
        bool endApp = false;
        int useCount = 0;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            // Record how many times the calculator has been used.
            Console.WriteLine($"Used for {useCount} calculations");
            useCount++;

            // Declare variables and set to empty.
            double result = 0;
            double cleanNum1 = double.NaN;
            double cleanNum2 = double.NaN;

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tx - 10x");
            Console.WriteLine("\ti - Sin");
            Console.WriteLine("\tc - Cos");
            Console.WriteLine("\tt - Tan");
            Console.Write("Your option? ");

            string op = Console.ReadLine();

            // Get numbers to work with.
            if (soloOperators.Contains(op))
            {
                cleanNum1 = SelectInput();
            }
            else
            {
                cleanNum1 = SelectInput();
                cleanNum2 = SelectInput();
            }

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, 'c' and Enter to clear the history and continue, or press any other key and Enter to continue: ");
            string endSelection = Console.ReadLine();
            if (endSelection == "n")
            {
                endApp = true;
            }
            else if (endSelection == "c")
            {
                calculator.ClearHistory();
                Console.WriteLine("History cleared");
                continue;
            }

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        calculator.Finish();
        return;
    }

    private static double SelectInput()
    {
        // Declare variables and set to empty.
        string input = string.Empty;
        double result = double.NaN;

        string errorMessage = "This is not valid input. Please enter an integer value or h: ";

        // Ask the user to type the first number.
        Console.Write("Type a number, or h to access a historic result, and then press Enter: ");
        input = Console.ReadLine();

        do
        {
            // Check if input is acceptable and re-ask if not.
            while (input != "h" && !double.TryParse(input, out result))
            {
                Console.Write(errorMessage);
                input = Console.ReadLine();
            }

            // Get historic values if possible.
            if (input == "h")
            {
                try
                {
                    return GetHistoryValue();
                }
                catch
                {
                    errorMessage = "This is not valid input. Please enter an integer value: ";
                    Console.WriteLine(errorMessage);
                    input = Console.ReadLine();
                }
            }
        } while (result == double.NaN);

        return result;
    }
}