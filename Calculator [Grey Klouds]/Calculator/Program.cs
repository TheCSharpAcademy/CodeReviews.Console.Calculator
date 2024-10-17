using System.Text.RegularExpressions;
using CalculatorLibrary;
using System.Collections.Generic;
using System.Reflection.Emit; // Importing the name space to create a List

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int count = 0;
           List<double> list = new List<double>();

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
                double cleanNum1 = 0;
                double cleanNum2 = 0;

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\th - History");
                Console.WriteLine("\tn - Exit");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();
                if (op.Trim() == "h")
                {
                    calculator.getHistory(list);
                    continue;
                }
                if(op.Trim() == "n")
                {
                    endApp = true;
                    return;
                }
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();


                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();


                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|h|n]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op.Trim(), count);
                        list.Add(result);
                        
                        
                        

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

                // Show the number of uses at the end of each calculation.
                // After, wait for the user to respond before closing.
                Console.WriteLine("You have used this app: " + ++count + " time(s).");
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            
            // Add call to close the JSON writer before return
            calculator.Finish();
           
            return;
        }
    }
}