using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int usageCount = 0;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");

            Calculator calculator = new Calculator();
            var calculations = calculator.GetCalculations();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                double cleanNum1 = 0;
                double cleanNum2 = 0;

                Console.WriteLine("------------------------\n");
                Console.WriteLine("Select operation type using the choice1 below:");
                Console.WriteLine("\ta - Addition");
                Console.WriteLine("\ts - Subtraction");
                Console.WriteLine("\tm - Multiplication");
                Console.WriteLine("\td - Division");
                Console.WriteLine("\tsq - Square Root");
                Console.WriteLine("\tp - Taking the Power");
                Console.WriteLine("\tt - Raising 10 to the Power of x");
                Console.WriteLine("\tsin - Sine");
                Console.WriteLine("\tcos - Cosine");
                Console.WriteLine("\ttan - Tangent");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sq|p|t|sin|cos|tan]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    if (op == "a" || op == "s" || op == "m" || op == "d" || op == "p") GetTwoOperands();
                    if (op == "sq" || op == "t" || op == "sin" || op == "cos" || op == "tan") GetOneOperand();

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
                Console.WriteLine("------------------------\n");

                void GetOneOperand()
                {
                    if (op == "sq")
                    {
                        Console.WriteLine("√x");
                    }
                    if (op == "t")
                    {
                        Console.WriteLine("10^x");
                    }
                    if (op == "sin" || op == "cos" || op == "tan")
                    {
                        Console.WriteLine("sin(x) or cos(x) or tan(x) where x is an angle in degrees.");
                    }
                    
                    // Ask the user to type the first number.

                    Console.Write(calculations.Count == 0
                        ? "Type a number x: "
                        : "Type a number x or enter letter 'y' to use the result of the last operation: ");

                    numInput1 = Console.ReadLine();

                    if (calculations.Count != 0 && numInput1 == "y")
                    {
                        var lastOperation = calculations[^1];
                        cleanNum1 = lastOperation.result;
                    }
                    else
                    {
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }
                    }
                }


                
                

                void GetTwoOperands()
                {
                    if (op == "p")
                    {
                        Console.Write("This number will be the base. ");
                    }
                    // Ask the user to type the first number.
                    Console.Write(calculations.Count == 0
                        ? "Type a number: "
                        : "Type a number or enter letter 'y' to use the result of the last operation: ");

                    numInput1 = Console.ReadLine();

                    if (calculations.Count != 0 && numInput1 == "y")
                    {
                        var lastOperation = calculations[^1];
                        cleanNum1 = lastOperation.result;
                    }
                    else
                    {
                        while (!double.TryParse(numInput1, out cleanNum1))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput1 = Console.ReadLine();
                        }
                    }

                    if (op == "p")
                    {
                        Console.Write("This number will be the exponent. ");
                    }

                    // Ask the user to type the second number.
                    Console.Write(calculations.Count == 0
                        ? "Type another number: "
                        : "Type another number or enter letter 'y' to use the result of the last operation: ");

                    numInput2 = Console.ReadLine();

                    if (calculations.Count != 0 && numInput2 == "y")
                    {
                        var lastOperation = calculations[^1];
                        cleanNum2 = lastOperation.result;
                    }
                    else
                    {
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                    }
                }

                usageCount++;

                // Wait for the user to respond before closing.
                Console.WriteLine("Enter:");
                Console.WriteLine("\tv - To View Latest Calculations");
                Console.WriteLine("\tn - To Close the App");
                Console.WriteLine("\tc - To Continue");

                string? choice1 = Console.ReadLine();

                if (choice1 == null || !Regex.IsMatch(choice1, "[v|n|c]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    if (choice1 == "c") continue;
                    if (choice1 == "n") endApp = true;

                    
                    if (choice1 == "v")
                    {
                        Console.WriteLine("Latest Calculations");
                        Console.WriteLine("------------------------");
                        foreach (var calc in calculations)
                        {
                            string output = calc.symbol switch
                            {
                                "+" or "-" or "*" or "/" or "^" => $"{calc.num1} {calc.symbol} {calc.num2} = {calc.result}",
                                "√" or "10^" or "sin" or "cos" or "tan" => $"{calc.symbol}({calc.num1}) = {calc.result}",
                                _ => "Error!, unknown symbol."
                            };
                            Console.WriteLine(output);
                        }

                        Console.WriteLine("\n------------------------\n");
                        Console.WriteLine("Enter:");
                        Console.WriteLine("\td - To Delete this list");
                        Console.WriteLine("\tc - to continue");
                        Console.WriteLine("\tn - To close the App");

                        string? choice2 = Console.ReadLine();

                        if (choice2 == null || !Regex.IsMatch(choice2, "[d|new|n|c]"))
                        {
                            Console.WriteLine("Error: Unrecognized input.");
                        }
                        else
                        {
                            if (choice2 == "c") continue;
                            if (choice2 == "n") endApp = true;
                            if (choice2 == "d") calculations.Clear();
                        }

                    }
                    
                }
                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish(usageCount);
            return;
            }
        }
    }