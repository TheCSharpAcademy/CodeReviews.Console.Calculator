using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        private Calculator calculator;
        public Program()
        {
            calculator = new Calculator();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";

            bool endApp = false;
            while (!endApp)
            {
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.WriteLine("Type another number, and then press Enter: ");
                Console.WriteLine("OR");
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\tr - SquareRoot");
                Console.WriteLine("\tt - TenPower");
                Console.WriteLine("\tsine - Sin");
                Console.WriteLine("\tcosine - Cos");
                Console.WriteLine("\ttangent - Tan");

                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (string.IsNullOrEmpty(numInput2))
                {
                    Console.Write("Input cannot be empty. Please enter a numeric value or choose an operator:");
                    numInput2 = Console.ReadLine();
                }
                while (!double.TryParse(numInput2, out cleanNum2) && !Regex.IsMatch(numInput2, "[r|t|sine|cosine|tangent]"))
                {
                    Console.Write("This is not valid input. Please enter a numeric value or choose an operator:");
                    numInput2 = Console.ReadLine();
                }
                if (Regex.IsMatch(numInput2, "[r|t|sine|cosine|tangent]"))
                {
                    string op = numInput2;
                    program.SingleNumberCalc(cleanNum1, op);
                }
                else
                {
                    // Ask the user to choose an operator.
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.WriteLine("\tp - PowerOf");

                    Console.Write("Your option? ");

                    string? op = Console.ReadLine();

                    // Validate input is not null, and matches the pattern
                    while (op == null || !Regex.IsMatch(op, "[a|s|m|d|p]"))
                    {
                        Console.WriteLine("Error: Unrecognized input. Please enter a valid operator:");
                        op = Console.ReadLine();
                    }
                    program.DoubleNumberCalc(cleanNum1, cleanNum2, op);
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                bool endSection = false;
                while (!endSection)
                {
                    Console.WriteLine("You have used the calculator {0} times", program.calculator.GetUsageCount());
                    Console.WriteLine("Press 'n' and Enter to close the app,");
                    Console.WriteLine("Press 'h' and Enter to show the history, or press any other key and Enter to continue: ");
                    string? readInput = Console.ReadLine();
                    if (readInput == "n")
                    {
                        endApp = true;
                        endSection = true;
                    }
                    else if (readInput == "h")
                    {
                        Console.WriteLine("History:");
                        program.ShowHistory();
                        Console.WriteLine("\nPress 'd' and Enter delete the history,");
                        Console.WriteLine("\nPress 'c' and the index and Enter to perform new calculation using the result as first number (Example: c1), ");
                        Console.WriteLine("or press any other key and Enter to continue: ");
                        readInput = Console.ReadLine();

                        if (!string.IsNullOrEmpty(readInput))
                        {
                            if (readInput.Remove(1) == "d")
                            {
                                program.calculator.ClearHistory();
                            }
                            else if (readInput.Remove(1) == "c" && readInput.Length > 1)
                            {
                                program.HistoryResultCalc(readInput);
                            }
                        }
                    }
                    else
                    {
                        endSection = true;
                    }
                }
            }

            Console.WriteLine("\n"); // Friendly linespacing.
            program.calculator.Finish();
            return;
        }

        public void ShowHistory()
        {
            foreach (string historyItem in calculator.GetHistory())
            {
                Console.WriteLine(historyItem);
            }
        }
        public void HistoryResultCalc(string? readInput)
        {

            int index = 0;

            while (!int.TryParse(readInput.Trim().Remove(0, 1), out index)) //check if what comes after c is number
            {
                Console.WriteLine("Please enter a valid index (For example: c1): ");
                readInput = Console.ReadLine();
            }

            string? numInput1;
            while (true)
            {
                try
                {
                    numInput1 = calculator.GetHistory()[index - 1].Split(" ").Last(); //get the number after '=' in List
                    break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The index you enter doesn't exist in the history. ");
                    do
                    {
                        Console.WriteLine("Please enter a valid index (For example: c1): ");
                        readInput = Console.ReadLine();
                    } while (readInput == null || !int.TryParse(readInput.Trim().Remove(0, 1), out index)); //check if what comes after c is number
                }
            }

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("Something went wrong when using the result. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }
            Console.WriteLine("Type a number, and then press Enter: ");
            Console.WriteLine("OR");
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\tr - SquareRoot");
            Console.WriteLine("\tt - TenPower");
            Console.WriteLine("\tsine - Sin");
            Console.WriteLine("\tcosine - Cos");
            Console.WriteLine("\ttangent - Tan");

            string? numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (string.IsNullOrEmpty(numInput2))
            {
                Console.Write("Input cannot be empty. Please enter a numeric value or choose an operator:");
                numInput2 = Console.ReadLine();
            }
            while (!double.TryParse(numInput2, out cleanNum2) && !Regex.IsMatch(numInput2, "[r|t|sine|cosine|tangent]"))
            {
                Console.Write("This is not valid input. Please enter a numeric value or choose an operator:");
                numInput2 = Console.ReadLine();
            }
            if (Regex.IsMatch(numInput2, "[r|t|sine|cosine|tangent]"))
            {
                SingleNumberCalc(cleanNum1, numInput2);
            }
            else
            {
                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                while (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input. Please enter a valid operator:");
                    op = Console.ReadLine();
                }
                DoubleNumberCalc(cleanNum1, cleanNum2, op);
            }
        }

        public void SingleNumberCalc(double cleanNum1, string operation)
        {
            try
            {
                double result = calculator.DoOperation(cleanNum1, operation);
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

        public void DoubleNumberCalc(double cleanNum1, double cleanNum2, string operation)
        {
            try
            {
                double result = calculator.DoOperation(cleanNum1, cleanNum2, operation);
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
    }
}
