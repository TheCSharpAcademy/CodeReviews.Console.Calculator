﻿using System.Text.RegularExpressions;
using CalculatorLibrary;

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

            double lastResult = 0;
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;
                
                Console.Clear();
                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tv - View history");
                Console.WriteLine("\tc - Clear history");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|v|c]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    if (op == "c")
                    {
                        calculator.ClearHistory();
                        Console.WriteLine("History cleared.");
                    }
                    // If the user chooses 'v', view the history
                    else if (op == "v")
                    {
                        var history = calculator.GetHistory();
                        Console.WriteLine("History:");
                        foreach (var operation in history)
                        {
                            Console.WriteLine(operation);
                        }
                    }
                    else
                    {
                        // Ask the user to type the first number.
                        Console.Write("Type a number, or 'r' to use the result of the last operation, and then press Enter: ");
                        numInput1 = Console.ReadLine();

                        double cleanNum1 = 0;
                        if (numInput1 == "r")
                        {
                            cleanNum1 = lastResult;
                        }
                        else
                        {
                            while (!double.TryParse(numInput1, out cleanNum1))
                            {
                                Console.Write("This is not valid input. Please enter an integer value: ");
                                numInput1 = Console.ReadLine();
                            }
                        }
                        
                        // Ask the user to type the second number.
                        Console.Write("Type another number, or 'r' to use the result of the last operation, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        double cleanNum2 = 0;
                        if (numInput2 == "r")
                        {
                            cleanNum2 = lastResult;
                        }
                        else
                        {
                            while (!double.TryParse(numInput2, out cleanNum2))
                            {
                                Console.Write("This is not valid input. Please enter an integer value: ");
                                numInput2 = Console.ReadLine();
                            }
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
                                lastResult = result;
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
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