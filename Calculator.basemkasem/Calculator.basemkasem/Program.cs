namespace CalculatorProgram;

using CalculatorLibrary;
using System.Text.RegularExpressions;
class Program
{
    //Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.
    static void Main(string[] args)
    {
        bool endApp = false;
        Calculator calculator = new Calculator();

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1;
            string? numInput2;
            double result = 0;

            Console.Clear();
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsqr - Square Root");
            Console.WriteLine("\tp - Taking the Power(num1^num2)");
            Console.WriteLine("\tx - 10x (num1.10^num2)");
            Console.WriteLine("\tsin - num1.sin(num2)");
            Console.WriteLine("\tcos - num1.cos(num2)");
            Console.WriteLine("\ttan - num1.tan(num2)");
            Console.WriteLine("\thistory - Show your last operations you did.");

            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "(a|s|m|d|sqr|p|x|sin|cos|tan|history)"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else if (op == "history")
            {
                calculator.ShowHistory();
            }
            else if (op =="sqr" || op == "sin" || op == "cos" || op == "tan") 
            {
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
                result = calculator.DoOperation(cleanNum1, 0, op);
                Console.WriteLine("Your result: {0:0.##}\n", result);
                calculator.calculationsAmount++;
            }
            else
            {
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
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
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        calculator.calculationsAmount++;
                    } 
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oh no! An exception occurred trying to do the math.\n - Details: {e.Message}");
                }
            }
            Console.WriteLine("------------------------\n");
            
            Console.Write("Press 'n' and Enter to close the app,'clear' to delete history, or press any other key and Enter to continue: ");
            string userInput = Console.ReadLine();
            if (userInput == "clear") calculator.ClearHistory();
            if (userInput== "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        Console.WriteLine($"Calculator were used {calculator.calculationsAmount} times.");
        calculator.Finish();
        return;
    }
}


