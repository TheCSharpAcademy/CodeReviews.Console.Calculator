// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        double cleanNum1 = 0, cleanNum2 = 0;
        double result = 0;
        Calculator calculator = new Calculator();

        static void Main(string[] args)
        {
            // Display title as the C# console calculator app.
          
            Program program = new Program();
            program.Menu();

            return;
        }

        double PastResult()
        {
            int index = 0;
            Console.Write("Which result you want to use? ");
            while (!int.TryParse(Console.ReadLine(), out index) )
            {
                Console.Write("This is not valid input. Please enter index from past result: ");
            }

            return calculator.GetResult(index);

        }
        bool UsePastResult()
        {
            Console.Write("\nWant to use past result? Press y to use past result or press any other key to continue: ");
            if (Console.ReadLine() == "y") return true;
            return false;
        }

        void Input(bool usePastResult, bool singleInput)
        {
            string? numInput1 = "";
            string? numInput2 = "";

            if (!usePastResult)
            {
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            else
            {
                cleanNum1 = PastResult();
                
            }

            if (!singleInput)
            {
                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }
            }
        }

        void Menu()
        {
            bool endApp = false;
            bool pastResult = false;


            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            int timesUsed = 0;
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine

                // Ask the user to choose an operator.
                Console.WriteLine("\nChoose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsqrt - Square Root");
                Console.WriteLine("\tpow - a^b");
                Console.WriteLine("\texp - 10^x");
                Console.WriteLine("\tsin - sine(theta)");
                Console.WriteLine("\tcos - cosine(theta)");
                Console.WriteLine("\ttan - tangent(theta)");
                Console.WriteLine("\tdel - Delete List");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();



                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sqrt|pow|exp|sin|cos|tan|del]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    if(op == "del")
                    {
                        Console.WriteLine("------------------------\n");
                        calculator.EmptyList();
                        Console.WriteLine("------------------------\n");
                        Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                        if (Console.ReadLine() == "n") break;
                        continue;
                    }
                    try
                    {
                        bool singleInput = false;
                        if(op == "sqrt" || op == "exp" || op == "sin" || op == "cos" || op == "tan")
                        {
                            singleInput = true;
                            
                        }
                        Input(pastResult, singleInput);
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

                Console.WriteLine("Recent Calculations:");
                calculator.ViewCalculation();

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                timesUsed++;
                
                if (Console.ReadLine() == "n") break;
                else pastResult = UsePastResult();


            }

            Console.WriteLine($"\nThe calculator was used {timesUsed} times");

            // Add call to close the JSON writer before return
            calculator.Finish();
        }
    }
}