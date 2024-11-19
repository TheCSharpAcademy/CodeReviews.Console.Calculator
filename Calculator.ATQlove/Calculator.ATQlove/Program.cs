using System.Text.RegularExpressions;
using CalculatorLibrary.ATQlove;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();

        int count = 0; // A count that record the times that usr use the calculator
        List<string> calculationHistory = new List<string>(); // A list to store the latest calculations
        Dictionary<int, double> resultMap = new Dictionary<int, double>();

        while (!endApp)
        {
            count++;
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;
            double cleanNum1 = 0;

            Console.WriteLine("Calculation history:");
            int index = 1;
            foreach (var calc in calculationHistory)
            {
                Console.WriteLine($"{index}. {calc}");
                index++;
            }

            Console.WriteLine("\nDo you want to use a result from the history? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Enter the number corresponding to the result: ");
                string? selected = Console.ReadLine();
                if (int.TryParse(selected, out int choice) && choice > 0 && choice <= resultMap.Count)
                {
                    Console.WriteLine($"You selected: {resultMap[choice]}");
                    cleanNum1 = resultMap[choice];
                }
                else
                {
                    Console.WriteLine("Invalid choice, proceeding with manual input.");
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
            }
            else
            {
                // Ask the user to type the first number if no history is used
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
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

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                string op_str = "";
                switch (op)
                {
                    case "a":
                        op_str = "+"; break;
                    case "s":
                        op_str = "-"; break;
                    case "m":
                        op_str = "*"; break;
                    case "d":
                        op_str = "/"; break;
                    default: break;
                }
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        string calculation = $"{cleanNum1} {op} {cleanNum2} = {result:0.##}";
                        Console.WriteLine("Your result: {0:0.##}\n", result);

                        // Add the calculation to the history and resultMap
                        calculationHistory.Add(calculation);
                        resultMap[calculationHistory.Count] = result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            Console.WriteLine("\nDo you want to clear the calculation history? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                calculationHistory.Clear();
                resultMap.Clear();
                Console.WriteLine("Calculation history cleared.\n");
            }

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
            Console.Write($"Times the calculator was used: {count}\n");

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}