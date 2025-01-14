using CalculatorLibrary;
using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CalculatorLibrary.Models;
using CalculatorLibrary.Enums;

namespace CalculatorProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            bool endApp = false;

            while (!endApp)
            {
                ConsoleUI.RefreshScreen(calculator);

                if (calculator.History.Count > 0)
                {
                    ConsoleUI.PrintOptionsMenu();
                    string optionSelection = ConsoleUI.GetUserInput("Enter menu selection: ");

                    switch (optionSelection.ToLower())
                    {
                        case "c":
                            ConsoleUI.PerformCalculations(calculator);
                            break;
                        case "r":
                            ConsoleUI.PerformCalculations(calculator, true, calculator.History.Last().Result);
                            break;
                        case "h":
                            (bool recallResult, double resultToRecall) = ConsoleUI.ManageHistory(calculator);

                            if (recallResult)
                            {
                                ConsoleUI.PerformCalculations(calculator, true, resultToRecall);
                            }
                            break;
                        case "d":
                            Console.WriteLine("Deleting calculator history...");
                            calculator.History.Clear();
                            Console.WriteLine("History deleted!");
                            Console.WriteLine();
                            ConsoleUI.PressAnyKeyToContinue();
                            break;
                        case "x":
                            endApp = true;
                            Console.WriteLine("Goodbye!");
                            ConsoleUI.PressAnyKeyToContinue();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ConsoleUI.PerformCalculations(calculator);
                }
            }

            calculator.Finish();

            return;
        }

        
    }
}