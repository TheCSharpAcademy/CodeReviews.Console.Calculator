using CalculatorLibrary.Enums;
using CalculatorLibrary.Models;
using CalculatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProgram
{
    internal static class ConsoleUI
    {
        internal static void PressAnyKeyToContinue()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
        internal static void PrintDividerLine(int fraction, int newLines)
        {
            int width = Console.WindowWidth;
            for (int i = 1; i <= width / fraction; i++)
                Console.Write("-");

            for (int i = 0; i <= newLines; i++)
                Console.WriteLine();
        }
        internal static void PrintTitleBar(int fractions, int newLines)
        {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#");
            PrintDividerLine(fractions, newLines);
        }
        internal static void PrintBriefHistoryBar(Calculator calculator)
        {
            string calcHistory = (calculator.History.Count > 0) ? GenerateCalculationDisplayText(calculator.History.Last()) : "<HISTORY EMPTY>";

            Console.WriteLine($"Number of calculations performed: {calculator.TimesUsed}");
            Console.WriteLine($"Last Calculation: {calcHistory}");
            Console.WriteLine();
        }
        internal static void RefreshScreen(Calculator calculator)
        {
            PrintTitleBar(1, 1);
            PrintBriefHistoryBar(calculator);
        }
        internal static void PrintOptionsMenu()
        {
            Console.WriteLine("Select a menu item from the following list:"
                                + "\n\tC: New Calculation"
                                + "\n\tR: Recall previous answer as first number"
                                + "\n\tH: View History"
                                + "\n\tD: Delete History"
                                + "\n\tX: Exit Applicationn");
            Console.WriteLine();
        }
        internal static void PrintOperationsMenu()
        {
            Console.WriteLine("Choose an operation from the following list:"
                                + "\n\tA: Add" +    "\t\tQ: Sqare Root"
                                + "\n\tS: Subtract" + "\tR: y Root(x)"
                                + "\n\tM: Multiply" + "\tP: Power (y^x)"
                                + "\n\tD: Divide");
            Console.WriteLine();
        }


        internal static string GetUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        internal static double CleanNumber(string number)
        {
            double cleanNumber = 0;

            while (!double.TryParse(number, out cleanNumber))
            {
                number = GetUserInput("ERROR: Invalid input. Please enter a numeric value: ");
            }

            return cleanNumber;
        }

        internal static void PerformCalculations(Calculator calculator, bool recallAnswer = false, double previousAnswer = double.NaN)
        {
            CalculationModel calculation = new CalculationModel();
            RefreshScreen(calculator);

            if (recallAnswer)
            {
                calculation.Operand1 = previousAnswer;
                Console.WriteLine($"Recalled first number:\t{previousAnswer}");
            }
            else
            {
                string numInput1 = GetUserInput("Enter first number:\t");
                calculation.Operand1 = CleanNumber(numInput1);
            }

            Console.WriteLine();
            PrintOperationsMenu();

            bool firstAttempt = true;
            while (calculation.Operation == Operation.Error)
            {
                string message = (firstAttempt) ? "Select your operation: " : "ERROR: Please select a valid operation: ";

                string operation = GetUserInput(message);
                SetCalculationOperation(calculation, operation);

                firstAttempt = false;
            }
            Console.WriteLine();


            //Validate operation
            if (calculation.Operation == Operation.Error)
            {
                Console.WriteLine("ERROR: Invalid input.");
            }
            else
            {
                if (calculation.Operation != Operation.SquareRoot)
                {
                    string partialCalculationText = string.Empty;
                    if (calculation.Operation != Operation.Root)
                    {
                        partialCalculationText = $"{calculation.Operand1} {calculation.OperationSymbol} ?";
                    }
                    else
                    {
                        partialCalculationText = $"? {calculation.OperationSymbol}({calculation.Operand1})";
                    }

                    RefreshScreen(calculator);
                    Console.WriteLine($"Current Calculation: {partialCalculationText}");
                    Console.WriteLine();
                    string numInput2 = GetUserInput("Enter second number:\t");
                    calculation.Operand2 = CleanNumber(numInput2);
                    Console.WriteLine();
                }

                //Evaluate calculation
                try
                {
                    calculation.Result = calculator.DoOperation(calculation);
                    if (double.IsNaN(calculation.Result))
                    {
                        Console.WriteLine("This operation results in a mathematical error!");
                        calculation.HasError = true;
                    }
                    else
                    {
                        Console.WriteLine(GenerateCalculationDisplayText(calculation));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oh no! An exception occurred while trying to do the math!\n - Details: {e.Message}");
                    calculation.HasError = true;

                }

                calculator.History.Add(calculation);
                calculator.TimesUsed++;
            }

            Console.WriteLine();
            PressAnyKeyToContinue();

        }

        internal static void PrintHistory(Calculator calculator)
        {
            Console.Clear();
            Console.WriteLine("Printing calculator history");
            Console.WriteLine();
            Console.WriteLine("ID\tCalculation");
            PrintDividerLine(2, 1);

            int rowID = 1;
            foreach (CalculationModel calculation in calculator.History)
            {
                Console.WriteLine($"{rowID}:\t{GenerateCalculationDisplayText(calculation)}");
                rowID++;
            }

            Console.WriteLine();
        }
        internal static (bool recallResult, double result) ManageHistory(Calculator calculator)
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
                PrintHistory(calculator);

                string idSelection = string.Empty;
                int cleanID = 0;
                bool idIsValid = false;

                while (!idIsValid)
                {
                    idSelection = GetUserInput("Enter the ID number of the result to recall: ");
                    idIsValid = int.TryParse(idSelection, out cleanID);

                    if (cleanID < 1 || cleanID > calculator.History.Count || calculator.History[cleanID - 1].HasError)
                    {
                        idIsValid = false;
                        Console.WriteLine("ERROR: Cannot recall a calculation that resulted in a mathematical error!");
                    }
                }

                recallResult = true;
                result = calculator.History[cleanID - 1].Result;

                Console.WriteLine();
                Console.WriteLine($"Recalling {result} from history entry {cleanID}!");
                Console.WriteLine();
                PressAnyKeyToContinue();
            }

            return (recallResult, result);
        }

        internal static void SetCalculationOperation(CalculationModel calculation, string input)
        {
            switch (input.ToLower())
            {
                case "a":
                    calculation.Operation = Operation.Add;
                    calculation.OperationSymbol = "+";
                    break;
                case "s":
                    calculation.Operation = Operation.Subtract;
                    calculation.OperationSymbol = "-";
                    break;
                case "m":
                    calculation.Operation = Operation.Multiply;
                    calculation.OperationSymbol = "*";
                    break;
                case "d":
                    calculation.Operation = Operation.Divide;
                    calculation.OperationSymbol = "/";
                    break;
                case "q":
                    calculation.Operation = Operation.SquareRoot;
                    calculation.OperationSymbol = "sqrt";
                    break;
                case "r":
                    calculation.Operation = Operation.Root;
                    calculation.OperationSymbol = "root";
                    break;
                case "p":
                    calculation.Operation = Operation.Power;
                    calculation.OperationSymbol = "^";
                    break;
                default:
                    calculation.Operation = Operation.Error;
                    break;
            }
        }

        internal static string GenerateCalculationDisplayText(CalculationModel c)
        {
            string output = string.Empty;
            string result = (c.HasError) ? "ERROR" : c.Result.ToString();

            switch (c.Operation)
            {
                case Operation.Add:
                case Operation.Subtract:
                case Operation.Multiply:
                case Operation.Divide:
                case Operation.Power:
                    output = $"{c.Operand1} {c.OperationSymbol} {c.Operand2} = {result}";
                    break;
                    break;
                case Operation.SquareRoot:
                    output = $"{c.OperationSymbol}({c.Operand1}) = {result}";
                    break;
                case Operation.Root:
                    output = $"{c.Operand2} {c.OperationSymbol}({c.Operand1}) = {result}";
                    break;
                default:
                    break;
            }

            return output;
        }

    }
}
