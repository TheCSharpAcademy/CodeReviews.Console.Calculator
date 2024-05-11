using System.Text.RegularExpressions;

namespace MSCalculator;
using CalculatorLibrary;

internal class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        int usage = 0;

        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        
        // call calculator class 
        Calculator calculator = new Calculator();
        
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            var numInput1 = "";
            var numInput2 = "";
            double cleanNum1 = 0.0;
            double cleanNum2 = 0.0;
            double result = 0.0;
            
            usage++;
            Console.WriteLine($"Calculator Usage: {usage}");
            
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\ts - Square Root"); 
            
            Console.Write("Your option? ");

            var op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|h|p|sr]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            } 
            else if (op == "sr")
            {
                Console.Write("Type a number: ");
                var x = Console.ReadLine();

                double newNum = 0.0;
                double placeHolder = 0.0;

                while (!double.TryParse(x, out newNum))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    x = Console.ReadLine();
                }

                result = calculator.DoOperation(newNum, placeHolder, op);
                Console.WriteLine(result);
            }
            else 
            {
                try
                {
                    // Ask the user to type the first number
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                    
                    // Ask the user for another number
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }
                    
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
            
            // Wait for the user to respond before closing.
            Console.WriteLine("Press 'n' and Enter to close the app");
            Console.WriteLine("Press h - Show History");
            Console.WriteLine("Press any other key and Enter to continue: ");

            var input = Console.ReadLine();
            
            switch (input)
            {
                case "n":
                    endApp = true;
                    break;
                case "h":
                    calculator.ShowHistory();
                    break;
            }

            Console.WriteLine("\n"); // Friendly lines pacing.
        }
        calculator.Finish(); 
    }
}