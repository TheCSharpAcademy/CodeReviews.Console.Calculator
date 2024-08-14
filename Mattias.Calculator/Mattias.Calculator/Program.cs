using CalculatorLibrary;
using System;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int numOfCalculations = 0;
            var results = new List<double>(); 
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
                Console.WriteLine("\tq - Square Root of the first number entered");
                Console.WriteLine("\tp - Power, the first number at the power of second");
                Console.WriteLine("\tt - 10x of first number entered");
                Console.WriteLine("\tsin - sine of the first number entered");
                Console.WriteLine("\tcos - cos of the first number entered");
                Console.WriteLine("\ttan - tan of the first number entered");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|q|p|t|sin|cos|tan]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
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

                results.Add(result);
                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") { 
                   
                    
                    endApp = true; 
                    numOfCalculations++;

                   
                    int i = 0;
                    Console.WriteLine($"Results: ");
                    foreach (var resulting in results)
                        Console.WriteLine($"{i++}){resulting}");

                    Console.WriteLine("Do you want to delete the list? y/n");
                    if (Console.ReadLine() == "y")
                        results.Clear();
                    else
                    {
                        Console.WriteLine("Do you want to use the results in another calculation? y/n");
                        if (Console.ReadLine() == "y")
                        {
                            bool usingResult = true;
                            while (usingResult)
                            {
                                Console.WriteLine("Which one do you wanna use as first number?");
                                int choice = Convert.ToInt32(Console.ReadLine());
                                for (int j = 0; j < results.Count; j++)
                                {
                                    cleanNum1 = results[choice - 1];
                                }

                                Console.WriteLine("Second number: ");
                                cleanNum2 = Convert.ToDouble(Console.ReadLine());

                                Console.WriteLine("Choose an operator from the following list:");
                                Console.WriteLine("\ta - Add");
                                Console.WriteLine("\ts - Subtract");
                                Console.WriteLine("\tm - Multiply");
                                Console.WriteLine("\td - Divide");
                                Console.WriteLine("\tq - Square Root of the first number entered");
                                Console.WriteLine("\tp - Power, the first number at the power of second");
                                Console.WriteLine("\tt - 10x of first number entered");
                                Console.WriteLine("\tsin - sine of the first number entered");
                                Console.WriteLine("\tcos - cos of the first number entered");
                                Console.WriteLine("\ttan - tan of the first number entered");
                                Console.Write("Your option? ");

                                string? opSecond = Console.ReadLine();

                                result = calculator.DoOperation(cleanNum1, cleanNum2, opSecond);
                                Console.WriteLine($"Result is = {result}");

                                usingResult = false;
                            }
                        }
                    }
                   

                    
                }
                else numOfCalculations++;
                

                Console.WriteLine("\n"); // Friendly linespacing.
            }

            Console.WriteLine($"Times ran: {numOfCalculations}");

            calculator.Finish();
            return;
        }
    }
}