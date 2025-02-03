
using System.Text.RegularExpressions;


namespace CalculatorLibrary
{
    public class CalculatorMenu
    {
        bool endApp;
        double cleanNum1 = 0;
        double cleanNum2 = 0;
        public void ShowMenu()
        {
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

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter or h to use your calc history: ");
                numInput1 = Console.ReadLine();
                if (numInput1.Equals("h"))
                {
                    cleanNum1 = calculator.history.GetNumberFromList();
                }
                else
                {

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                }
                // Ask the user to type the second number.
                Console.Write("Type another number h to use your calc history, and then press Enter: ");
                numInput2 = Console.ReadLine();
                if (numInput2.Equals("h"))
                {
                    cleanNum2 = calculator.history.GetNumberFromList();
                }
                else
                {


                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                }


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
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result) || (double.IsInfinity(result)))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else if (!double.IsNormal(result))
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            Console.WriteLine("Warning! The result might not be precise due to the numbers provided");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                        calculator.history.LogResult(result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
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
    }
    }

