using CalculatorLibrary;
using System.Diagnostics;
using System.Text;
namespace CalculatorProgram.Doc415
{
    internal class Program
    {
        static public int operationCount = 0;           //Calculate how many operations made in the session
        static public bool endApp = false;
        static Calculator calculator = new Calculator();

        static void Main(string[] args)
        {
            AppStartCounter.AppStartCounter.GetAndSaveStartCount(); // Shows and saves how many times calculator application run
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SetInterval(() =>                           //write the elapsed time in async
            {
                int x = Console.CursorLeft; int y = Console.CursorTop;
                Console.SetCursorPosition(83, 1);
                Console.WriteLine($"Calculator started {AppStartCounter.AppStartCounter.counter} times");
                Console.SetCursorPosition(83, 2);
                Console.WriteLine($"Total {operationCount} operations completed");

                Console.SetCursorPosition((int)x, (int)y);
            }, TimeSpan.FromSeconds(1));
            Console.OutputEncoding = Encoding.Unicode;

            string userInput = "";
            while (!endApp)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Console Calculator in C#\r");
                    Console.WriteLine("------------------------\n\n");
                    Console.WriteLine("Select operation type: (s)imple/(c)omplex    type (clear) to clear operation history");
                    userInput = Console.ReadLine();
                } while (userInput != "s" && userInput != "c" && userInput != "clear");

                if (userInput == "s")
                    simpleOperationsCalculation();
                else if (userInput == "c")
                    complexOperationsCalculation();
                else
                {
                    calculator.showRecentOperations();
                    calculator.ClearHistory();
                }
            }
            calculator.Finish();
            return;
        }

        static async Task SetInterval(Action action, TimeSpan timeout)
        {
            await Task.Delay(timeout).ConfigureAwait(false);
            action();
            SetInterval(action, timeout);
        }



        static void simpleOperationsCalculation()
        {
            bool continueSimpleOperations = true;
            while (continueSimpleOperations)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;
                // Ask the user to type the first number.
                calculator.showRecentOperations();
                Console.WriteLine("Type a number or select result from previous operations (id) and then press Enter: ");
                double cleanNum1 = GetInput();
                Console.WriteLine("Type a number or select result from previous operations (id) and then press Enter: ");
                double cleanNum2 = GetInput();
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tp - Power");
                Console.Write("Your option? ");
                string op = Console.ReadLine();
                try
                {
                    result = calculator.DoOperation(cleanNum1, op, cleanNum2);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    operationCount++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");
                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, Press'm' to return main menu or press any other key and Enter to continue: ");
                string? userInput = Console.ReadLine();
                if (userInput == "n")
                {
                    continueSimpleOperations = false;
                    endApp = true;

                }
                if (userInput == "m") continueSimpleOperations = false;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
        }
        static double GetInput()
        {
            double cleanNum1 = 0;
            bool validInput = false;
            do
            {
                string numInput1 = Console.ReadLine();
                if (numInput1.StartsWith("a"))
                {
                    string parsed = numInput1.Substring(1);
                    bool valid = int.TryParse(parsed, out int index);
                    if (valid)
                    {
                        cleanNum1 = calculator.operationsHistory[index].result;
                        validInput = true;
                    }
                    else { validInput = false; }
                }
                else
                {
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter an integer value: ");
                        numInput1 = Console.ReadLine();
                    }
                    validInput = true;
                }
            } while (!validInput);
            return cleanNum1;
        }
        static void complexOperationsCalculation()
        {
            bool continueComplexOperations = true;
            while (continueComplexOperations)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                double result = 0;
                // Ask the user to type the first number.
                calculator.showRecentOperations();
                Console.WriteLine("Type a number or select result from previous operations (id) and then press Enter: ");
                double cleanNum1 = GetInput();
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\tsin - Sin");
                Console.WriteLine("\tcos - Cos");
                Console.WriteLine("\tsqr - Square Root");
                Console.WriteLine("\tlog - Logarithm");
                Console.Write("Your option? ");
                string op = Console.ReadLine();
                try
                {
                    result = calculator.DoOperation(cleanNum1, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                    operationCount++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");
                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, Press'm' to return main menu or press any other key and Enter to continue: ");
                string? userInput = Console.ReadLine();
                if (userInput == "n")
                {
                    continueComplexOperations = false;
                    endApp = true;

                }
                if (userInput == "m") continueComplexOperations = false;

                Console.WriteLine("\n"); // Friendly linespacing.
            }

        }
    }
}
