using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            bool usingCalculator = false;
            bool reuseCalculation = false;

            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Logger logger = new Logger();
            Calculator calculator = new Calculator(logger);
            int usedTimes = 0;

            while (!endApp)
            {
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\tc - Calculator");
                Console.WriteLine("\th - Show History");
                Console.WriteLine("\tdh - Delete History");
                Console.WriteLine("\tr - Reuse Calculation");
                Console.WriteLine("\te - Exit App");
                Console.Write("Your option? ");
                string? option = Console.ReadLine();

                calculator.ParseOption(option, out usingCalculator, out endApp, out reuseCalculation);

                while (reuseCalculation)
                {
                    if (calculator.IsHistoryEmpty())
                    {
                        Console.WriteLine("History is empty. Nothing to reuse.");
                        reuseCalculation = false;
                        break;
                    }

                    calculator.ShowHistory(); // Display all past calculations with indexes

                    Console.Write("Enter the number of the calculation you want to reuse: ");
                    if (int.TryParse(Console.ReadLine(), out int index))
                    {
                        if (calculator.GetCalculation(index - 1, out var calc))
                        {
                            Console.WriteLine($"Selected: {calculator.GetFormattedString(calc)}");

                            Console.Write("Enter new value for num1 (or press Enter to keep it): ");
                            string? newNum1 = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newNum1) && double.TryParse(newNum1, out double parsed1))
                            {
                                calc.Num1 = parsed1;
                            }

                            Console.Write("Enter new value for num2 (or press Enter to keep it): ");
                            Console.WriteLine("Second number will be ignored if it is not normally utilized in calculation.");

                            string? newNum2 = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newNum2) && double.TryParse(newNum2, out double parsed2))
                            {
                                calc.Num2 = parsed2;
                            }

                            Console.Write("Enter new operation (a|s|m|d|sqrt|pow|10x|sin|cos|tan) or press Enter to keep it: ");
                            string? newOp = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newOp))
                            {
                                calc.Operation = newOp;
                            }

                            Calculator.Calculation newCalc = calculator.DoOperation(calc.Num1, calc.Num2 ?? 0, calc.Operation);

                            Console.WriteLine($"Updated result: {calculator.GetFormattedString(newCalc)}");
                            reuseCalculation = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid index.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
                while (usingCalculator)
                {
                    // Declare variables and set to empty.
                    // Use Nullable types (with ?) to match type of System.Console.ReadLine
                    string? numInput1 = "";
                    string? numInput2 = "";

                    // Ask the user to choose an operator.
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tsqrt - Square Root (first number only)");
                    Console.WriteLine("\tpow - Power");
                    Console.WriteLine("\tl - Logarithm");
                    Console.WriteLine("\tsin - Sine");
                    Console.WriteLine("\tcos - Cosine");
                    Console.WriteLine("\ttan - Tangent");
                    Console.WriteLine("\te - Exit Calculator");
                    Console.Write("Your option? ");

                    string? op = Console.ReadLine();

                    var validOperations = new HashSet<string> { "a", "s", "m", "d", "sqrt", "pow", "l", "sin", "cos", "tan", "e" };
                    while (op == null || !validOperations.Contains(op))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                        op = Console.ReadLine();
                    }

                    if (op == "e")
                    {
                        usingCalculator = false;
                        break;
                    }

                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    double cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }

                    // Ask the user to type the second number, only if useable in the calculation
                    double cleanNum2 = 0;
                    var binaryOperations = new HashSet<string> { "a", "s", "m", "d", "pow" };
                    if (binaryOperations.Contains(op))
                    {
                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                    }

                    try
                    {
                        Calculator.Calculation calc = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(calc.Result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine($"Your result: {calc.Result}\n");

                        // only add to usedItems if we've received a result
                        usedTimes++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    Console.WriteLine("------------------------\n");

                    // Wait for the user to respond before closing.
                    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "n")
                    {
                        usingCalculator = false;
                        Console.WriteLine($"You have used the calculator {usedTimes} times.");
                    }

                    Console.WriteLine("\n"); // Friendly linespacing.
                }
                
            }
            logger.Dispose();
            return;
        }
    }
}
