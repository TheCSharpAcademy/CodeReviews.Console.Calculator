using CalculatorLibrary;
namespace CalculatorProgram.K_MYR;

internal class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        Calculator calculator = new();
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            double cleanNum1;
            double cleanNum2;
            double result;
            double? returnValue = null;

            //Ask the user if he wants to use a previous result
            Console.WriteLine("Enter 'h' to show previous results to choose from, or press Enter to continue: ");
            string? readResult = Console.ReadLine();

            if (readResult == "h")
            {
                returnValue = calculator.ShowHistory();
                Console.Clear();
            }

            if (returnValue.HasValue)
            {
                cleanNum1 = returnValue.Value;
            }
            else
            {
                // Ask the user to type the first number.

                Console.Write("Type a number, and then press Enter: ");
                // Declare variables and set to empty.
                string numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            string numInput2 = Console.ReadLine();

            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Nth-Root");
            Console.WriteLine("\tp - Nth-Power");
            Console.Write("Your option? ");

            string[] listOfOperations = { "a", "s", "m", "d", "r", "p" };

            string op = Console.ReadLine().Trim().ToLower();

            while (!listOfOperations.Contains(op))
            {
                Console.WriteLine("Invalid Input! Please choose a operation from above");
                op = Console.ReadLine().Trim().ToLower();
            }

            try
            {
                Console.WriteLine("------------------------");
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.");
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}", result);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------");
            Console.Write("Enter 'n' to exit. Press Enter to continue:\n");

            // Wait for the user to respond before closing.            
            if (Console.ReadLine() == "n") endApp = true;
        }
        // Add call to close the JSON writer before return
        calculator.Finish();
        return;
    }
}
