using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{

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

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
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
                Console.WriteLine("\tsr - Square Root");
                Console.WriteLine("\tp - Taking the power");
                Console.WriteLine("\tx - 10x");
                Console.WriteLine("\tsin - Sine");
                Console.WriteLine("\tcos - Cosine");
                Console.WriteLine("\ttan - Tangent");
                Console.WriteLine("\tl - Latest Calculations");


                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sr|p|x|sin|cos|tan|l]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        if (op == "sr" || op == "x" || op == "sin" || op == "cos" || op == "tan")
                        {
                            result = calculator.DoOperation(cleanNum1, op);

                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0} of {1} is {2:0.##}\n", op, cleanNum1, result);

                            }

                            result = calculator.DoOperation(cleanNum2, op);

                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0} of {1} is {2:0.##}\n", op, cleanNum2, result);

                            }
                        }
                        else if (op == "l")
                        {
                            Console.Clear();
                            foreach (var game in Helper.games)
                            {
                                Console.WriteLine($"Number 1:  {game.Num1} - Number 2: {game.Num2} - Operation: {game.Operation} Result: {game.Result}, Date: {game.Date}");
                            }

                            Console.WriteLine("Do you want to delete this list?. Y/N ");
                            var rta = Console.ReadLine();
                            if (rta.ToUpper() == "Y")
                            {
                                Helper.games.Clear();
                            }

                        }
                        else
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else Console.WriteLine("Your result: {0:0.##}\n", result);
                        }
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