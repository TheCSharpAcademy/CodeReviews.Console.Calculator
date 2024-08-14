using System.Text.RegularExpressions;
using MansoorAZafar.CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;

        Calculator calculator = new Calculator();

        while (!endApp)
        {
            Console.Clear();
            // Display title as the C# console calculator app
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------");
            // Declare variables and set to empty.
            double result = 0;

            double cleanNum1 = double.NaN;
            double cleanNum2 = 0;

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tx - 10x");
            Console.WriteLine("\tt - Trigonometry Functions (for both numbers)");
            Console.WriteLine("\tv - View History");
            Console.WriteLine("\tb - Use a previous existing resultant as the first-number (any decimal will be rounded down)");
            Console.Write("Your option?: ");

            string? op = Console.ReadLine();
            if (op == "b")
            {
                double index = 0;
                GetAndPutValidInput(ref index, message: "Enter the Calculation Number #\n> ");
                cleanNum1 = calculator.GetExistingResult((int)index);
                if (double.IsNaN(cleanNum1))
                {
                    Console.WriteLine("Going Back to the Start\nPress any key to restart...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                else
                {
                    Console.Write("Choose an Operation\n> ");
                    op = Console.ReadLine();
                }
            }

            // Validate input is not null, and matches the pattern
            op = op?.ToLower()[0].ToString(); // we do this because the program could break if we use the whole string
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|x|t|v]"))
            {
                Console.WriteLine("Error: Bad Input.");
                goto EndOfLoop;
            }
            if(op == "v")
            {
                calculator.ViewHistory();
                goto EndOfLoop;
            }
            const double tenX = 10;

            if(double.IsNaN(cleanNum1)) GetAndPutValidInput(ref cleanNum1, message: "Enter the value for the first number\n> ");

            try
            {
                switch (op)
                {
                    //Square Root
                    case "r":
                        result = calculator.DoOperation(num1: cleanNum1, op:op);
                        break;
                    //10x
                    case "x":
                        result = calculator.DoOperation(num1: tenX, num2: cleanNum1, op: op);
                        break;
                    //Trig
                    case "t":
                        //The 2nd value doesn't matter for this only the first
                        result = calculator.DoOperation(num1: cleanNum1, op: op);
                        break;
                    //All other operations
                    default:
                        //We need the 2nd number
                        if (op == "p") op = "x"; // we want the power operation, but we just arent using 10x
                        GetAndPutValidInput(ref cleanNum2, message: "Enter the value for the second number\n> ");
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        break;
                }
                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                else Console.WriteLine("Your result: {0:0.##}\n", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + ex.Message);
            }


            //Marked as end of the loop so I can avoid making else and else-if branches
            EndOfLoop:
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }

    static void GetAndPutValidInput(ref double cleanNum, string message = "Type a number, and then press Enter: ")
    {
        string? numInput = "";

        // Ask the user to type the first number.
        Console.Write(message);
        numInput = Console.ReadLine();

        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput = Console.ReadLine();
        }
    }
}