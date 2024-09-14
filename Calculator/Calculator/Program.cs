using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            if(File.Exists("counter.txt"))
            {
                int readLine = Int32.Parse(File.ReadAllText("counter.txt"));
                Console.WriteLine("You used the calculator " + readLine + " times so far! \n\n");
                File.WriteAllText("counter.txt", (readLine + 1).ToString());
            } 
            else
            {
                Console.WriteLine("You are using the calculator for the first time!");
                File.WriteAllText("counter.txt", "1");
            }
            bool endApp = false;
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
                if(ShortTermMemory.ListWithResults.Count > 0)
                {
                    Console.Write("Type a number, and then press Enter or type p to print last results: ");
                    numInput1 = Console.ReadLine();
                    if (numInput1 == "p") 
                    {
                        ShortTermMemory.PrintResults();
                        Console.WriteLine("Type a result's index number and press enter");
                        try
                        {
                            int index = Int32.Parse(Console.ReadLine());
                            numInput1 = ShortTermMemory.GetResult(index);
                        }
                        catch { }
                    }
                }
                else
                {
                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();
                }

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                if (ShortTermMemory.ListWithResults.Count > 0)
                {
                    Console.Write("Type another number, and then press Enter or type p to print last results: ");
                    numInput2 = Console.ReadLine();
                    if (numInput2 == "p")
                    {
                        ShortTermMemory.PrintResults();
                        Console.WriteLine("Type a result's index number and press enter");
                        try
                        {
                            int index = Int32.Parse(Console.ReadLine());
                            numInput2 = ShortTermMemory.GetResult(index);
                        }
                        catch { }
                    }
                }
                else
                {
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();
                }

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsr - Square root - only first number used for calculation");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\tsin - Sinus - only first number used for calculation");
                Console.WriteLine("\tcos - Cosine - only first number used for calculation");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|sr|p|sin|cos]"))
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
                        ShortTermMemory.AddResult(result.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, press 'd' to delete all saved calculations, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "d")
                {
                    ShortTermMemory.ClearListWithResults();
                    Console.WriteLine("Press any key to continue");
                }
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}