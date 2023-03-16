using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int timesUsed = 0;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to type the first number. Or choose a result of a previous calculation.
                Console.Write("Type a number or 's' to use a previous result, then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!(double.TryParse(numInput1, out cleanNum1) || numInput1 == "s"))
                {
                    Console.Write("This is not valid input. Please enter a number: ");
                    numInput1 = Console.ReadLine();
                }
                if (numInput1 == "s")
                {
                    cleanNum1 = calculator.UseAPreviousResult();
                }

                // Ask the user to type the second number. Or choose a result of a previous calculation.
                Console.Write("Type another number or 's' to use another result, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!(double.TryParse(numInput2, out cleanNum2) || numInput2 == "s"))
                {
                    Console.Write("This is not valid input. Please enter a number: ");
                    numInput2 = Console.ReadLine();
                }
                if (numInput2 == "s")
                {
                    cleanNum2 = calculator.UseAPreviousResult();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsr - Square root (num1)");
                Console.WriteLine("\tpw - Power");
                Console.WriteLine("\tsin - Sin(num1)");
                Console.WriteLine("\tcos - Cos(num1)");
                Console.WriteLine("\ttan - Tang(num1)");
                Console.Write("Your option? ");

                string op = Console.ReadLine();

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
                        timesUsed++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.WriteLine(@"Press 'n' to close the app,
Press 'd' to delete the calculation history,
or press any other key to continue, then press Enter: ");

                string userRespond = Console.ReadLine();
                if (userRespond == "n")
                {
                    endApp = true;
                    Console.WriteLine($"Thanks for using our Calculator, you did {timesUsed} calculations. See you!");
                } else if (userRespond == "d")
                {
                    calculator.DeleteCalculationHistory();
                }
                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}
