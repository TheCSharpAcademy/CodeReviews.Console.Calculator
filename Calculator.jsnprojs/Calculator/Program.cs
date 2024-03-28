using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;
            int numberPosition1 = 0;

            double cleanNum1 = 0;
            double cleanNum2 = 0;

            Console.WriteLine($"Do you want to use one of the number from the calculator's history? y/n");
            string? userChoice = Console.ReadLine();
            if (calculator.resultsHistory.Count() > 0 &&!string.IsNullOrEmpty(userChoice) && userChoice == "y")
            {
                if (calculator.resultsHistory.Count > 0)
                {
                    foreach (double r in calculator.resultsHistory)
                    {
                        Console.Write($"{r} ");
                    }
                    Console.WriteLine("\n\ntype the number's position");
                    userChoice = Console.ReadLine();
                    while (string.IsNullOrEmpty(userChoice) || 
                           !int.TryParse(userChoice, out numberPosition1) || 
                           numberPosition1 < 0 || 
                           numberPosition1 > calculator.resultsHistory.Count()) 
                    {
                        Console.WriteLine("Type the number's position that's not outside of range:");
                        userChoice = Console.ReadLine();
                    }

                    // At this point, numberPosition1 contains a valid index
                    cleanNum1 = calculator.resultsHistory[numberPosition1 - 1];

                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput2 = Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no results in your calculator history.");
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide"); 
            Console.WriteLine("\tr - SquareRoot");
            Console.WriteLine("\tp - Taking the power");
            Console.WriteLine("\tt - Tangent");
            Console.WriteLine("\t if you choose r or t, only the first number entered will be used");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|t]"))
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
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");
            Console.WriteLine($"List of results:");

            if (calculator.resultsHistory.Count > 0)
            {
                foreach (double r in calculator.resultsHistory)
                {
                    Console.Write($"{r} ");
                }
            }
            
            Console.WriteLine("\n\nClear list of results? y - yes, n or any other key - no");
            userChoice = Console.ReadLine();
            if(!string.IsNullOrEmpty(userChoice) && userChoice == "y")
            {
                calculator.resultsHistory.Clear();                    
            }
                   
            Console.WriteLine();
            Console.WriteLine($"Number of times the calculator was used: {calculator.numsOfTimesUsed}");
            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
}