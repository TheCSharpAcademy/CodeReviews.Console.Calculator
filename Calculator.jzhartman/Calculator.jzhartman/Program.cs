using CalculatorLibrary;
using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorProgram
{
    /*
     *  ToDo: Ability to recall any previous calculations from history list
     *  ToDo: Remove New Calculation option and have menu built into main operation
     *  ToDo: Add other math operations sqrt, taking the power, 10x, and Trig
     * 
     * 
     */

    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            AppFlowManager(calculator);

            return;
        }

        private static void PressAnyKeyToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
        private static void PrintDividerLine(int fraction, int newLines)
        {
            int width = Console.WindowWidth;
            for (int i = 1; i <= width/fraction; i++)
                Console.Write("-");

            for(int i = 0; i <= newLines; i++)
                Console.WriteLine();
        }
        private static void PrintTitleBar(int fractions, int newLines)
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#");
            PrintDividerLine(fractions, newLines);
        }
        private static void PrintBriefHistoryBar(Calculator calculator)
        {
            if (calculator.History.Count > 0)
            {
                Console.WriteLine($"Number of calculations performed: {calculator.TimesUsed}");

                string result = (calculator.History.Last().HasError) ? "ERROR" : calculator.History.Last().Result.ToString();
                Console.WriteLine($"Last Calculation: {calculator.History.Last().Operand1} {calculator.History.Last().Operation} {calculator.History.Last().Operand2} = {result}");
                Console.WriteLine(); 
            }
        }
        private static void PrintOptionsMenu()
        {
            Console.WriteLine("Select a menu item from the following list:"
                                +"\n\tC: New Calculation"
                                +"\n\tR: Recall previous answer as first number"
                                +"\n\tH: View History"
                                +"\n\tD: Delete History"
                                +"\n\tX: Exit Applicationn");
            Console.WriteLine();
        }
        private static void PrintOperationsMenu()
        {
            Console.WriteLine("Choose an operator from the following list:"
                                +"\n\tA: Add"
                                +"\n\tS: Subtract"
                                +"\n\tM: Multiple"
                                +"\n\tD: Divide");
            Console.WriteLine();
        }

        private static string GetUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private static double CleanNumber(string number)
        {
            double cleanNumber = 0;

            while (!double.TryParse(number, out cleanNumber))
            {
                number = GetUserInput("ERROR: Invalid input. Please enter a numeric value: ");
            }

            return cleanNumber;
        }

        private static void PerformCalculations(Calculator calculator, bool recallAnswer = false, double previousAnswer = double.NaN)
        {
            double result = 0;

            double cleanNum1 = double.NaN;
            if (recallAnswer)
            {
                cleanNum1 = previousAnswer;
                Console.WriteLine($"Recalled first number:\t{cleanNum1}");
            }
            else
            {
                string numInput1 = GetUserInput("Enter first number:\t");
                cleanNum1 = CleanNumber(numInput1);
            }

            string numInput2 = GetUserInput("Enter second number:\t");
            double cleanNum2 = CleanNumber(numInput2);

            Console.WriteLine();
            PrintOperationsMenu();
            string operation = GetUserInput("Select your operation: ");
            Console.WriteLine();

            //Validate operation
            if (operation == null || !Regex.IsMatch(operation.ToLower(), "[a|s|m|d]"))
            {
                Console.WriteLine("ERROR: Invalid input.");
            }
            else
            {
                //Evaluate calculation
                string operationSymbol = Calculator.GetOperationSymbol(operation);
                bool calculationHasError = false;
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, operation);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation results in a mathematical error!");
                        calculationHasError = true;
                    }
                    else
                    {
                        Console.WriteLine("Your result: {1} {2} {3} = {0:0.##}\n", result, cleanNum1, operationSymbol, cleanNum2);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oh no! An exception occurred while trying to do the math!\n - Details: {e.Message}");
                    calculationHasError = true;

                }

                calculator.History.Add(new Calculation(cleanNum1, cleanNum2, operationSymbol, result, calculationHasError));
                calculator.TimesUsed++;
            }

            PressAnyKeyToContinue();

        }

        private static void AppFlowManager(Calculator calculator)
        {
            bool endApp = false;

            while (!endApp)
            {
                PrintTitleBar(1, 1);
                PrintBriefHistoryBar(calculator);

                if (calculator.History.Count > 0)
                {
                    PrintOptionsMenu();
                    string optionSelection = GetUserInput("Enter menu selection: ");
                    PrintTitleBar(1,1);

                    switch (optionSelection.ToLower())
                    {
                        case "c":
                            PrintBriefHistoryBar(calculator);
                            PerformCalculations(calculator);
                            break;
                        case "r":
                            PrintBriefHistoryBar(calculator);
                            PerformCalculations(calculator, true, calculator.History.Last().Result);
                            break;
                        case "h":
                            PrintHistory(calculator);
                            break;
                        case "d":
                            Console.WriteLine("Deleting calculator history...");
                            calculator.History.Clear();
                            Console.WriteLine("History deleted!");
                            PressAnyKeyToContinue();
                            break;
                        default:
                            break;
                    }  
                }
                else
                {
                    PerformCalculations(calculator);
                }
            }

            calculator.Finish();

        }

        private static void PrintHistory(Calculator calculator)
        {
            Console.Clear();
            Console.WriteLine("Printing calculator history");
            Console.WriteLine();

            int rowID = 1;
            foreach (Calculation calculation in calculator.History)
            {
                string result = (calculation.HasError) ? "ERROR" : calculation.Result.ToString();

                Console.WriteLine($"{rowID}:\t{calculation.Operand1} {calculation.Operation} {calculation.Operand2} = {result}");
                rowID++;
            }

            Console.WriteLine();
            PressAnyKeyToContinue();
        }
    }
}