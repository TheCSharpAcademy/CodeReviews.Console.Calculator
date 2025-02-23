using System.Text.RegularExpressions; 
using CalculatorLibrary; 

// Initialize necessary variables
int calculatorUsed = 0; 
bool endApp = false; 
CalculatorBrain calculatorBrain = new(); 

// Main loop of the calculator application
while (!endApp)
{
    // Clear the console to update the display
    Console.Clear();

    // Display the current usage of the calculator
    Console.WriteLine($"Console Calculator. You have used the calculator {calculatorUsed} times.\r");
    Console.WriteLine("------------------");

    double result = 0;

    // Display options for the user to choose from
    Console.WriteLine(@"Choose an option from the following list:
a - Add
s - Subtract
m - Multiply
d - Divide
c - Input from history result
h - History");
    Console.WriteLine("------------------");

    string? op = Console.ReadLine();

    // Handle history option
    if (op == "h")
    {
        calculatorBrain.PrintHistory(); 
    }
    // Handle input from history option
    else if (op == "c")
    {
        calculatorBrain.PrintHistoryForInput();

        Console.WriteLine("Choose the order number of the result you want to use and press Enter.");

        string? indexStr = Console.ReadLine();
        int index = 0;

        while (!int.TryParse(indexStr, out index))
        {
            Console.WriteLine("This is not a valid input. Please enter a numeric value: ");
            indexStr = Console.ReadLine(); 
        }
        calculatorBrain.InputFromHistory(index);
    }
    // Check if the operation input is invalid (anything other than a valid operation)
    else if (op == null || !Regex.IsMatch(op, "[asmd]"))
    {
        Console.WriteLine("Error: Unrecognized input."); 
    }
    else
    {
        // Get the two numbers for calculation from the user
        double[] numbers = calculatorBrain.InputNumbers();
        double cleanNum1 = numbers[0];
        double cleanNum2 = numbers[1];

        try
        {
            // Perform the operation based on the user's choice
            result = calculatorBrain.DoOperation(cleanNum1, cleanNum2, op);

            // Check if the result is NaN
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                calculatorUsed++;

                // Add the operation to the history based on the user's chosen operation
                switch (op)
                {
                    case "a":
                        calculatorBrain.AddToHistory(cleanNum1, cleanNum2, "+", result);
                        break;
                    case "s":
                        calculatorBrain.AddToHistory(cleanNum1, cleanNum2, "-", result);
                        break;
                    case "m":
                        calculatorBrain.AddToHistory(cleanNum1, cleanNum2, "*", result);
                        break;
                    case "d":
                        calculatorBrain.AddToHistory(cleanNum1, cleanNum2, "/", result);
                        break;
                }
                Console.WriteLine("\nYour result: {0:0.####}\n", result);
            }
        }
        catch (Exception ex)
        {
            // Catch and display any exception that occurs during the calculation
            Console.WriteLine("An exception occurred trying to do the math. \n - Details: " + ex.Message);
        }
    }

    // If the user did not choose history option, give them the option to continue or close
    if (op != "h")
    {
        Console.WriteLine("------------------\n");
        Console.WriteLine("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
        if (Console.ReadLine() == "n") endApp = true;
    }
}
// Finalize and add to JSON the number of times the calculator was used
calculatorBrain.Finish(calculatorUsed);
return;
