using System.Text.RegularExpressions;
using CalculatorLibrary;
namespace CalculatorProgram
{
    internal class Program
    {

        /*
        CHALLENGES:
        1. ✓ Create a functionality that will count the amount of times the calculator was used.
        2. ✓ Store a list with the latest calculations. And give the users the ability to delete that list.
        3. ✓ Allow the users to use the results in the list above to perform new calculations.
        4. ✓ Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.
        */
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            bool endApp = false;

            while (!endApp)
            {
                double result = 0;
                string? operation;
                string? inputPostCalculation;

                calculator.Start();
                operation = Console.ReadLine();

                if (operation == null || !Regex.IsMatch(operation, "^(a|s|m|d|p|sr|10x|sin|cos|tg)$"))
                {
                    Console.WriteLine("Error: Unrecognized input. Press any key to continue");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    result = calculator.Calculate(operation);
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

                // Wait for the user to respond before closing.
                Console.WriteLine("Press [Enter] to continue");
                Console.WriteLine("Or press [o] and [Enter] to show options");
                Console.WriteLine("Or press [r] and [Enter] to use historical result as first operand");
                Console.WriteLine("Or press [e] and [Enter] to close the app");

                inputPostCalculation = Console.ReadLine();

                switch (inputPostCalculation)
                {
                    case "e": 
                        endApp = true;
                        break;
                    case "o":
                        calculator.OptionsMenu(); 
                        break;
                    case "r":
                        calculator.GettingResultFromHistory = true; 
                        break;
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
        
        
    }
}
