using CalculatorLibrary;
using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CalculatorLibrary.Models;
using CalculatorLibrary.Enums;

namespace CalculatorProgram
{
    /*
     *  Seems to work....
     *  
     *  Need to recheck some things....
     *  
     *  Make it a bit prettier and less busy....
     *  
     *  Fix output decimal places
     *  
     *  ToDo: Remove New Calculation option and have menu built into main operation
     *  ToDo: Add other math operations 10x, and Trig
     */

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