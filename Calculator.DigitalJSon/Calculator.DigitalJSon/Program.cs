using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            string? input = "";
            string choice = "";
            bool previousCalculationsExist = File.Exists("calculation.json");
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            if (previousCalculationsExist)
            {
                Console.WriteLine("List of previous calculations found. Would you like to keep it? (y/n)");
                do
                {
                    input = Console.ReadLine();
                    if (input != null)
                    {
                        choice = input.ToLower().Trim();
                    }
                    switch (choice)
                    {
                        case "y":
                            Console.WriteLine("Using existing list...");
                            calculator.LoadCalculationJson();
                            break;
                        case "n":
                            Console.WriteLine("Deleting list...");
                            calculator.DeleteJson();
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    }
                } while (choice != "y" && choice != "n");




            }
            
            while (!endApp)
            {
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                previousCalculationsExist = File.Exists("calculation.json");
               
                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\te - Taking the power");
                Console.WriteLine("\tsqr - Square Root");
                Console.WriteLine("\t10x - 10 raised to x");
                Console.WriteLine("\tsin - Sine function");
                Console.WriteLine("\tcos - Cosine function");
                Console.WriteLine("\ttan - Tangent function");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                while (op == null || !Regex.IsMatch(op, "^(a|s|m|d|sqr|e|10x|sin|cos|tan)$"))
                {
                    Console.WriteLine("Error: Unrecognized input. Please try again.");
                    op = Console.ReadLine();
                }
                try
                {
                    // Declare variable and set to empty.
                    // Use Nullable types (with ?) to match type of System.Console.ReadLine
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;
                    double previosResult = 0;
                    bool previousResultUsed = false;
                    bool num1Used = false;
                    bool num2Used = false;
                    int chosenOperand = 0;

                    if (previousCalculationsExist)
                    {
                        calculator.LoadCalculationJson();
                        Console.WriteLine("Would you like to reuse a result from a previous calculation? Type y and enter if yes and any other key if no.");
                        input = Console.ReadLine();
                        if (input != null)
                        {
                            choice = input.ToLower().Trim();
                        }
                        if (choice == "y")
                        {
                            calculator.ViewPreviousCalculations();
                            previosResult = calculator.UseResultAsOperand();
                            previousResultUsed = true;
                        }
                    }
                    if (op == "sqr" || op == "10x" || op == "sin" || op == "cos" || op == "tan")
                    {
                        if (!previousResultUsed)
                        {
                            Console.WriteLine("Type a number, then press Enter.");
                            numInput1 = Console.ReadLine();
                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not a valid input. Please enter a numeric value:");
                                numInput1 = Console.ReadLine();
                            }
                        }
                        else if (previousResultUsed)
                        {
                            cleanNum1 = previosResult;
                        }
                        result = calculator.DoOperation(cleanNum1, op, cleanNum2);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    else
                    {
                        if (previousResultUsed)
                        {
                            Console.WriteLine("Where would you like to use the previous result?");
                            Console.WriteLine("Type 1 if Operand 1 or 2 if Operand 2 then press Enter.");
                            input = Console.ReadLine();
                            while (!int.TryParse(input, out chosenOperand) || (chosenOperand < 1 || chosenOperand > 2))
                            {
                                Console.WriteLine("Invalid input. Please enter either 1 if Operand 1 or 2 if Operand 2");
                                input = Console.ReadLine();
                            }
                            if (chosenOperand == 1)
                            {
                                cleanNum1 = previosResult;
                                num1Used = true;

                            }
                            else if (chosenOperand == 2)
                            {
                                cleanNum2 = previosResult;
                                num2Used = true;
                            }
                        }
                        // Ask the user to type the first number.
                        if (!num1Used)
                        {
                            Console.WriteLine("Type a number, then press Enter (Operand 1)");
                            numInput1 = Console.ReadLine();

                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not a valid input. Please enter a numeric value:");
                                numInput1 = Console.ReadLine();
                            }
                        }
                        // Ask the user to type the second number.
                        if (!num2Used)
                        {
                            Console.WriteLine("Type a number, then press Enter (Operand 2)");
                            numInput2 = Console.ReadLine();

                            while (!double.TryParse(numInput2, out cleanNum2))
                            {
                                Console.Write("This is not a valid input. Please enter a numeric value:");
                                numInput2 = Console.ReadLine();
                            }
                        }
                        result = calculator.DoOperation(cleanNum1, op, cleanNum2);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }
                    
                catch (DivideByZeroException e)
                {
                    Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " + e.Message);
                }
                Console.WriteLine($"Calculator used {calculator.calcCounter} times");
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                calculator.SaveCalculationToJSon();
            }
            return;
        }
    }
}
