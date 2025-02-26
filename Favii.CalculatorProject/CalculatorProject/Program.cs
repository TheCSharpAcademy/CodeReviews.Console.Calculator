using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        string? input = "";
        bool endApp = false;
        Calculator calculator = new Calculator();
        

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");


        while (!endApp)
        {
            Console.WriteLine("Chose an option from the following list: ");
            Console.WriteLine("\tC - Do a calculation:");
            Console.WriteLine("\tH - See calculation's history");
            input = Console.ReadLine();
            input = input.ToLower();
            if (input == "h")
            {
                calculator.ShowCalculationsHistory();
                Console.WriteLine("\nWould you like to delete the list? y/n");
                input = Console.ReadLine().ToLower();
                if (input == "y")
                {
                    calculator.DeleteHistory();
                }
            }
            else
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Square Root");
                Console.WriteLine("\tp - Taking the Power");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                double cleanNum2 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                if (op != "r") //Only ask for the second number if the operation is not one of the ops that only use one number
                {
                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }

                }


                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p]"))
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
                            calculator.AddCalculation(cleanNum1, cleanNum2, result, op);
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            calculator.ShowCount();

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
          }
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}