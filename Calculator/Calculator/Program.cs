using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool endApp = false;
        bool usingHistoryResult = false;
        uint timesUsed = 0; // Variable to count the number of times the calculator is used.
        string[,] historyLog = new string[1, 2]; // Base empty 2D array.
        double historyNum = 0;


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
            double result = 0;
            string? history = "";
            double cleanNum1 = 0;


            if (!usingHistoryResult)
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
            else
            {
                historyNum = cleanNum1;
            }

                usingHistoryResult = false; // Reset the usingHistoryResult variable to false.

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
            Console.WriteLine("\tsrt - Square Root");
            Console.WriteLine("\tpow - Power");
            Console.WriteLine("\ttrig sin - trigonometry");
            Console.WriteLine("\ttrig cos - trigonometry");
            Console.WriteLine("\ttrig tan - trigonometry");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|srt|pow|trig sin|trig cos|trig tan]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    history = calculator.History(cleanNum1, cleanNum2, op);
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

            timesUsed++; // Increment the number of times the calculator has been used.

            for (int interation = 0; interation < timesUsed; interation++) // Store the values inside a 2D array to check the latest calculations.
            {
                string[,] dynamicResizingArray = new string[timesUsed, 2];

                // Copy the values from the base array to the new array.
                for (int i = 0; i < timesUsed - 1; i++)
                {
                    for (int j = 0; j < 2; j++) // 2 because of numbers of colum in the array
                    {
                        dynamicResizingArray[i, j] = historyLog[i, j];
                    }
                }

                historyLog = dynamicResizingArray; // Assign the new array to the base array.

                // Add the new values to the new array.
                historyLog[timesUsed - 1, 0] = $"{timesUsed}#: {history}"; // Record the value into the new array.
                historyLog[timesUsed - 1, 1] = $"{result}"; // Record the value into a colum to be used later .
            }

            // Wait for the user to respond before closing.
            Console.WriteLine($"This calculator has been used {timesUsed} times");
            Console.WriteLine("Check the list of operations below to see the latest calculations:\n");

            // Display the history of calculations.
            Console.WriteLine("History of calculations:");
            for (int i = 0; i < timesUsed; i++)
            {
                Console.WriteLine($"{historyLog[i, 0]}{historyLog[i, 1]}");
            }
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Press 'n' and Enter to close the app");
            Console.WriteLine("Press 'd' and Enter to delete the history");
            Console.WriteLine("Press 'r' and Enter to use the result of one of the previous operations");
            Console.WriteLine("Press any other key and Enter to continue:");

            string? endMenuChoice = Console.ReadLine();


            if (endMenuChoice == "n")
            {
                endApp = true;
            }

            if (endMenuChoice == "d")
            {
                timesUsed = 0;
            }

            if (endMenuChoice == "r")
            {
                usingHistoryResult = true;

                Console.WriteLine("Please enter the number of the operation you want to use:");
                numInput1 = Console.ReadLine();
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
                while(cleanNum1 <= 0)
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
                
                historyNum = double.Parse(historyLog[(int)cleanNum1 - 1, 1]);
            }

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;

    }
}