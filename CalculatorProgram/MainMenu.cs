using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

public class MainMenu
{
    internal void ShowMenu()
    {
        bool isGameOn = true;
        int calculationCount = 0;
        double result;

        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Console Calculator");

        Calculator calculator = new Calculator();

        while (isGameOn)
        {
            Calculator.PrintWelcomeMessage();

            string choice = Console.ReadLine().Trim().ToLower();

            if (choice == null || !Regex.IsMatch(choice, "[v|a|s|m|d|pow|sqrt|sin|cos|tan]"))
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
            else if (choice == "v")
            {
                Calculator.PrintCalculationList();
            }
            else if (choice == "sqrt" || choice == "sin" || choice == "cos" || choice == "tan")
            {
                var cleanNum = Calculator.GetSingleNumber();

                Console.Clear();
                result = calculator.CalculateOneNumber(cleanNum, choice);

                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    var operationType = Calculator.GetOperator(choice);
                    Console.WriteLine("----------------------------------------------------\n");
                    Calculator.PrintAdvancedCalculation(cleanNum, operationType, result);
                    calculationCount++;
                    Console.WriteLine("----------------------------------------------------\n");
                    Console.WriteLine($"\t You have performed {calculationCount} calculations.\n");
                    Calculator.AddToCalculationList($"Calculation {calculationCount}: {operationType} {cleanNum} = {result}");
                }
            }
            else
            {
                try
                {
                    var twoNumbers = Calculator.GetTwoNumbers();
                    double cleanNum1 = twoNumbers[0];
                    double cleanNum2 = twoNumbers[1];

                    Console.Clear();
                    result = calculator.CalculateTwoNumbers(cleanNum1, cleanNum2, choice);

                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        var operationType = Calculator.GetOperator(choice);
                        Console.WriteLine("----------------------------------------------------\n");
                        Calculator.PrintCalculation(cleanNum1, cleanNum2, operationType, result);
                        calculationCount++;
                        Console.WriteLine("----------------------------------------------------\n");
                        Console.WriteLine($"\t You have performed {calculationCount} calculations.\n");
                        Calculator.AddToCalculationList($"Calculation {calculationCount}: {cleanNum1} {operationType} {cleanNum2} = {result}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }

            Console.WriteLine("----------------------------------------------------\n");
            Console.Write("Press 'q' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "q") isGameOn = false;
        }

        calculator.Finish();

        return;
    }
}