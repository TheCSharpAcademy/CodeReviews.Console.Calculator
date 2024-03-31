using CalculatorLibrary.Arashi256;
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

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                DisplayCalcHistory(calculator);
                string? op = DisplayOperationsMenu();
                if (Regex.IsMatch(op, "q"))
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
                if (Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    // Regular 'big 4' operations.
                    ProcessBigFour(calculator, op);
                }
                else
                {
                    // Other operations which do not take 2 arguments. 
                    if (Regex.IsMatch(op, "r"))
                    {
                        // Square root of a number.
                        ProcessSquareRoot(calculator, op);
                    }
                    if (Regex.IsMatch(op, "p"))
                    {
                        // Number to the nth power.
                        ProcessPower(calculator, op);
                    }
                    if (Regex.IsMatch(op, "t"))
                    {
                        // Number to the nth power.
                        ProcessPowerTen(calculator);
                    }
                    if (Regex.IsMatch(op, "[i|c|z]"))
                    {
                        // Trig operations.
                        ProcessTrig(calculator, op);
                    }
                }
                Console.WriteLine("------------------------\n");
                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.WriteLine("\n"); // Friendly linespacing.
            }
            return;
        }

        static string? DisplayOperationsMenu()
        {
            bool isValid = false;
            string? op = null;
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operation from the following list:");
            Console.WriteLine("\ta - Addition");
            Console.WriteLine("\ts - Subtraction");
            Console.WriteLine("\tm - Multiplication");
            Console.WriteLine("\td - Division");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tt - Power of 10");
            Console.WriteLine("\ti - Trig Sin");
            Console.WriteLine("\tc - Trig Cos");
            Console.WriteLine("\tz - Trig Tan");
            Console.WriteLine("\tq - Quit");
            do
            {
                Console.Write("Your option? ");
                op = Console.ReadLine();
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|t|i|c|z|q]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            } while (!isValid);
            return op;
        }

        static void ProcessTrig(Calculator calculator, string op)
        {
            string numInput;
            double num = CheckPreviousCalculation(calculator);
            double result;
            if (Double.IsNaN(num))
            {
                Console.Write($"Type the angle value to pass to trig {calculator.TranslateOperationString(op)}(), and then press Enter: ");
                numInput = Console.ReadLine();
                while (!double.TryParse(numInput, out num))
                {
                    Console.Write("This is not valid input. Please enter an valid value: ");
                    numInput = Console.ReadLine();
                }
            }
            try
            {
                result = calculator.DoTrig(num, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine($"Your result: {result.ToString("N4")}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }

        static void ProcessPowerTen(Calculator calculator)
        {
            string numInput;
            double num = CheckPreviousCalculation(calculator);
            double result;
            if (Double.IsNaN(num))
            {
                Console.Write("Type the number to raise to the 10th power, and then press Enter: ");
                numInput = Console.ReadLine();
                while (!double.TryParse(numInput, out num))
                {
                    Console.Write("This is not valid input. Please enter an valid value: ");
                    numInput = Console.ReadLine();
                }
            }
            try
            {
                result = calculator.DoPowerTen(num);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine($"Your result: {result.ToString("N4")}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }

        static void ProcessSquareRoot(Calculator calculator, string op)
        {
            string numInput;
            double num = CheckPreviousCalculation(calculator);
            double result;
            if (Double.IsNaN(num))
            {
                Console.Write("Type the number to get the square root from, and then press Enter: ");
                numInput = Console.ReadLine();
                while (!double.TryParse(numInput, out num))
                {
                    Console.Write("This is not valid input. Please enter an valid value: ");
                    numInput = Console.ReadLine();
                }
            }
            try
            {
                result = calculator.DoSquareRoot(num, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine($"Your result: {result.ToString("N4")}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }

        static void ProcessPower(Calculator calculator, string op)
        {
            string numInput1, numInput2;
            double num1 = CheckPreviousCalculation(calculator);
            double num2;
            double result;
            if (Double.IsNaN(num1))
            {
                Console.Write("Type the base number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                while (!double.TryParse(numInput1, out num1))
                {
                    Console.Write("This is not valid input. Please enter an valid value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            Console.Write("Type exponent number, and then press Enter: ");
            numInput2 = Console.ReadLine();
            while (!double.TryParse(numInput2, out num2))
            {
                Console.Write("This is not valid input. Please enter an valid value: ");
                numInput2 = Console.ReadLine();
            }
            try
            {
                result = calculator.DoPowerOperation(num1, num2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine($"Your result: {result.ToString("N4")}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }

        static void ProcessBigFour(Calculator calculator, string op)
        {
            string numInput1, numInput2;
            double num1 = CheckPreviousCalculation(calculator);
            double num2;
            double result;
            if (Double.IsNaN(num1))
            {
                Console.Write("Type the first number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                while (!double.TryParse(numInput1, out num1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();
            while (!double.TryParse(numInput2, out num2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }
            try
            {
                result = calculator.DoSumOperation(num1, num2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine($"Your result: {result.ToString("N4")}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }
        }

        static double CheckPreviousCalculation(Calculator calculator)
        {
            double num = double.NaN;
            if (calculator.CalcHistory.Count > 0)
            {
                if (GetValidYesNoResponse("Do you want to use the result of the previous calculation in this new calculation (Y/N)? ") == 'Y')
                {
                    calculator.ListHistory();
                    int choice = GetNumberRangeResponse($"Which calculation do you wish to use (1 - {calculator.CalcHistory.Count})? ", 1, calculator.CalcHistory.Count);
                    num = calculator.GetResultFromList(choice);
                    Console.WriteLine($"Okay, we will use {num} as the first number in the calculation");
                }
            }
            return num;
        }

        static void DisplayCalcHistory(Calculator calculator)
        {
            Console.WriteLine($"There have been {calculator.OperationsCount} operation(s) this session.");
            calculator.ListHistory();
            if (calculator.CalcHistory.Count > 0)
            {
                if (GetValidYesNoResponse("Do you want to clear the calculation history (Y/N)? ") == 'Y')
                {
                    calculator.CalcHistory.Clear();
                }
            }
        }

        static char GetValidYesNoResponse(string question)
        {
            bool isValid = true;
            string? response;
            do
            {
                Console.WriteLine(question);
                response = Console.ReadLine();
                if (response == null || !Regex.IsMatch(response.ToLower().Trim(), "^[ynYN]$")) {
                    isValid = false;
                    Console.WriteLine("Please enter 'y' or 'n'");
                }

            } while (!isValid);
            return Convert.ToChar(response.ToUpper());
        }

        static int GetNumberRangeResponse(string? question, int min, int max)
        {
            int response;
            bool isValidInput = false;
            do
            {
                if (question != null) Console.Write(question);
                string input = Console.ReadLine();
                isValidInput = int.TryParse(input, out response);
                if (!isValidInput || response < min || response > max)
                {
                    Console.WriteLine($"Invalid input. Please enter a value between {min} and {max}.");
                }
            } while (!isValidInput || response < min || response > max);
            return response;
        }
    }
}