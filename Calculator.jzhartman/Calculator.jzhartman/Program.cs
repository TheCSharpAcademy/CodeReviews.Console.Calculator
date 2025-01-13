using CalculatorLibrary;
using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CalculatorLibrary.Models;

namespace CalculatorProgram
{
    /*
     *  ToDo: Remove New Calculation option and have menu built into main operation
     *  ToDo: Add other math operations sqrt, taking the power, 10x, and Trig
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
            Console.WriteLine("Choose an operation from the following list:"
                                + "\n\tA: Add"
                                + "\n\tS: Subtract"
                                + "\n\tM: Multiply"
                                + "\n\tD: Divide"
                                + "\n\tQ: Sqare Root"
                                + "\n\tR: y Root(x)"
                                + "\n\tP: Power (y^x)");
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

            Console.WriteLine();
            PrintOperationsMenu();
            string operation = GetUserInput("Select your operation: ");
            Console.WriteLine();


            //Validate operation
            if (operation == null || !Regex.IsMatch(operation.ToLower(), "[a|s|m|d|q|r|p]"))
            {
                Console.WriteLine("ERROR: Invalid input.");
            }
            else
            {
                double cleanNum2 = double.NaN;
                string operationSymbol = Calculator.GetOperationSymbol(operation);
                if (operation.ToLower() != "q")
                {
                    Console.WriteLine($"{cleanNum1} {operationSymbol} ?");
                    string numInput2 = GetUserInput("Enter second number:\t");
                    cleanNum2 = CleanNumber(numInput2);
                }

                //Evaluate calculation
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
                        if (Regex.IsMatch(operation.ToLower(), "[a|s|m|d|p]"))
                        {

                            Console.WriteLine("Your result: {1} {2} {3} = {0:0.##}\n", result, cleanNum1, operationSymbol, cleanNum2);
                        }
                        else if (Regex.IsMatch(operation.ToLower(), "[q]"))
                        {
                            Console.WriteLine("Your result: {2}({1}) = {0:0.##}\n", result, cleanNum1, operationSymbol);
                        }
                        else if (Regex.IsMatch(operation.ToLower(), "[r]"))
                        {
                            Console.WriteLine("Your result: {3} {2}({1}) = {0:0.##}\n", result, cleanNum1, operationSymbol, cleanNum2);

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oh no! An exception occurred while trying to do the math!\n - Details: {e.Message}");
                    calculationHasError = true;

                }

                calculator.History.Add(new CalculationModel(cleanNum1, cleanNum2, operationSymbol, result, calculationHasError));
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
                            (bool recallResult, double resultToRecall) = ManageHistory(calculator);

                            if (recallResult)
                            {
                                PrintTitleBar(1, 1);
                                PrintBriefHistoryBar(calculator);
                                PerformCalculations(calculator, true, resultToRecall);
                            }
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
            Console.WriteLine("ID\tCalculation");
            PrintDividerLine(2, 1);

            int rowID = 1;
            foreach (CalculationModel calculation in calculator.History)
            {
                string result = (calculation.HasError) ? "ERROR" : calculation.Result.ToString();

                Console.WriteLine($"{rowID}:\t{calculation.Operand1} {calculation.Operation} {calculation.Operand2} = {result}");
                rowID++;
            }

            Console.WriteLine();
        }


        private static (bool recallResult, double result) ManageHistory(Calculator calculator)
        {
            bool recallResult = false;
            double result = double.NaN;

            PrintHistory(calculator);

            string response = GetUserInput("Recall previous result as first number? (y/n): ");

            while (response.ToLower() != "y" && response.ToLower() != "n")
            {
                Console.Write("ERROR: Please enter \"y\" for yes or \"n\" for no: ");
                response = Console.ReadLine();
            }

            if (response.ToLower() == "n")
            {
                recallResult = false;
                PressAnyKeyToContinue();
            }
            else
            {
                string idSelection = string.Empty;
                int cleanID = 0;
                bool idIsValid = false;

                while (!idIsValid)
                {
                    idSelection = GetUserInput("Enter the ID number of the result to recall: ");
                    idIsValid = int.TryParse(idSelection, out cleanID);

                    if (cleanID < 1 || cleanID > calculator.History.Count || calculator.History[cleanID-1].HasError)
                    {
                        idIsValid = false;
                        Console.WriteLine("ERROR: Invalid entry!");
                    }
                }

                recallResult = true;
                result = calculator.History[cleanID - 1].Result;

                Console.WriteLine($"Recalling {result} from history entry {cleanID}!");
                PressAnyKeyToContinue();
            }

            return (recallResult, result);
        }
    }
}