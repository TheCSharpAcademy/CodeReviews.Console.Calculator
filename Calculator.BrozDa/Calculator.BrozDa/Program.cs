using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CalculatorLibrary;
namespace CalculatorProgram
{
    internal class Program
    {

        /*
        1. ✓ Create a functionality that will count the amount of times the calculator was used.
        2. ✓ Store a list with the latest calculations. And give the users the ability to delete that list.
        3. ✓ Allow the users to use the results in the list above to perform new calculations.
        4. Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.
        */
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            bool endApp = false;
            // Display title as the C# console calculator app.
            

            while (!endApp)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                calculator.PrintTitle();
                calculator.PrintHistory();
                Console.WriteLine("------------------------");
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                double cleanNum1;
                // Ask the user to type the first number.
                if (calculator.GettingResultFromHistory == false) 
                {
                    cleanNum1 = calculator.GetNumber(1);
                }
                else
                {
                    cleanNum1 = calculator.GetNumberFromResult();
                    calculator.GettingResultFromHistory = false;
                }

                

                // Ask the user to type the second number.
                double cleanNum2 = calculator.GetNumber(2);

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

                // Wait for the user to respond before closing.
                Console.WriteLine("Press [e] and [Enter] to close the app");
                Console.WriteLine("Press [o] and [Enter] to show options");
                Console.WriteLine("Press [r] and [Enter] to use historical result as first operand");
                Console.WriteLine("Or press [Enter] to continue");
                string input = Console.ReadLine();
                if (input == "e")
                {
                    endApp = true;
                }
                else if (input == "o")
                {
                    calculator.OptionsMenu();
                }
                else if (input == "r")
                {
                    calculator.GettingResultFromHistory = true;
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
        
        
    }
}
