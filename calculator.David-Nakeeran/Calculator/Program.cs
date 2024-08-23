using System.Text.RegularExpressions;

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

            CalculatorHistory calculatorHistory = new CalculatorHistory();
            CalculatorCounter calculatorCounter = new CalculatorCounter();
            CalculatorOperationHandler calculatorOperationHandler = new CalculatorOperationHandler(calculatorCounter, calculatorHistory);


            while (!endApp)
            {
                // Declare variables
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;

                // View previous calculation results
                string? readInput;
                Console.WriteLine("To view previous calculation results, type 'v' and then enter");
                readInput = Console.ReadLine();
                if (readInput == "v")
                {

                    if (calculatorHistory.CalculationsCount == 0)
                    {
                        Console.WriteLine("No previous calculations");
                        (cleanNum1, cleanNum2) = HandleNewCalculationInput();

                    }
                    else
                    {
                        calculatorHistory.PrintCalculatorHistory();
                        Console.WriteLine("Clear calculation History? Type y or n and then press enter");
                        readInput = Console.ReadLine();
                        readInput = CheckInputNullOrWhitespace(readInput);

                        switch (readInput)
                        {
                            case "y":
                                calculatorHistory.ClearHistory();
                                Console.WriteLine("Calculation history cleared");
                                (cleanNum1, cleanNum2) = HandleNewCalculationInput();
                                break;
                            case "n":
                                calculatorHistory.PrintCalculatorHistory();

                                Console.WriteLine("If you wish to use previous calculation result as a number for a new calculation, please enter corrensponding index number");
                                Console.WriteLine("Type the first index number, and then press Enter");
                                readInput = Console.ReadLine();

                                readInput = CheckInputNullOrWhitespace(readInput);
                                int index1 = GetValidIntegerIndexInput(calculatorHistory, readInput);

                                // Ask user for second input
                                Console.WriteLine("Type the second index number, and then press Enter");
                                readInput = Console.ReadLine();

                                readInput = CheckInputNullOrWhitespace(readInput);
                                int index2 = GetValidIntegerIndexInput(calculatorHistory, readInput);

                                double[] indexArr = calculatorHistory.GetCalculationByIndex(index1, index2);
                                cleanNum1 = indexArr[0];
                                cleanNum2 = indexArr[1];

                                break;

                        }
                    }
                }
                else
                {
                    (cleanNum1, cleanNum2) = HandleNewCalculationInput();

                }



                // Ask the user to choose an operator.
                Console.WriteLine($"Calculator used: {calculatorCounter.CounterValue}\n");
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

                        result = calculatorOperationHandler.PerformOperation(cleanNum1, cleanNum2, op);

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
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculatorOperationHandler.Finish();
            return;
        }

        static string CheckInputNullOrWhitespace(string? readInput)
        {
            while (String.IsNullOrWhiteSpace(readInput))
            {
                Console.WriteLine("Please enter the index number");
                readInput = Console.ReadLine();
            }

            return readInput;
        }

        static double ParseInput(string? input)
        {
            double cleanNum;
            while (!double.TryParse(input, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter an numeric value: ");
                input = Console.ReadLine();
            }
            return cleanNum;
        }

        static int GetValidIntegerIndexInput(CalculatorHistory calculatorHistory, string? readInput)
        {
            int index;

            while (!int.TryParse(readInput, out index) || !calculatorHistory.IsValidIndex(index))
            {
                // If input is not a valid integer
                if (!int.TryParse(readInput, out index))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                }
                // If integer is now valid index
                else if (!calculatorHistory.IsValidIndex(index))
                {
                    Console.WriteLine($"Index out of range. Please enter a number between 1 and {calculatorHistory.CalculationsCount}");
                }

                readInput = Console.ReadLine();
            }
            return index;
        }

        static (double, double) HandleNewCalculationInput()
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double cleanNum1 = 0;
            double cleanNum2 = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();


            cleanNum1 = ParseInput(numInput1);

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();


            cleanNum2 = ParseInput(numInput2);

            return (cleanNum1, cleanNum2);
        }
    }
}
