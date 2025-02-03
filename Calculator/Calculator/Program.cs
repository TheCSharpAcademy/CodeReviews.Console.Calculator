// Program.cs
using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int timesCalculatorUsed = 0;
            List<LatestCalculation> latestCalculationsList = new();
            double previousResult = 0;

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

                if (latestCalculationsList.Count != 0)
                {
                    // Ask the user if they want to reuse a result
                    Console.Write("Type a number, and then press Enter, OR to use a previous result type h: then the line number you wish to use ");
                }
                else
                {
                    // Ask the user to type the first number.
                    Console.Write("Type a number, and then press Enter: ");
                }
                numInput1 = Console.ReadLine();
                if (numInput1.Length>2 &&  numInput1.Substring(0, 2) == "h:")
                {
                    // do history lookup
                    string indexStr = numInput1.Substring(2);
                    if (int.TryParse(indexStr, out int index))
                    {
                        numInput1 = latestCalculationsList[index-1].calculationResult.ToString();
                        Console.WriteLine($"The first number is {numInput1}");
                    }
                    else
                    {
                        Console.WriteLine("History index does not exist");
                    }
                }

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
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op, out string operand);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            LatestCalculation latestCalculation = new LatestCalculation( $"{cleanNum1} {operand} {cleanNum2}", result);
                            latestCalculationsList.Add(latestCalculation);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                timesCalculatorUsed++;
                Console.WriteLine($"Times Calculator used: {timesCalculatorUsed}");
                Console.WriteLine("Previous Calculations:");
                Console.WriteLine("------------------------");
                int countHistory = 0;
                foreach (var calculation in latestCalculationsList)
                {
                    countHistory ++;
                    Console.WriteLine($"{countHistory}. {calculation.calculationString} = {calculation.calculationResult}");
                }
                Console.WriteLine("------------------------\n");
                Console.WriteLine("\n"); // Friendly linespacing.;
                Console.Write("Press 'c' and Enter to clear the list, or press any other key and Enter to continue: ");

                if (Console.ReadLine() == "c")
                {
                    latestCalculationsList.Clear();
                }
                //Wait for the user to respond before closing.
               Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}