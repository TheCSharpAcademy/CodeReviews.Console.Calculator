using CalculatorLibrary;
using CalculatorLibrary.Models;

namespace CalculatorProgram;

public static class CalculatorEngine
{
    private static List<CalculationModel> calculations = new List<CalculationModel>();

    public static void InitCalculator(Calculator calculator, bool useSecondNumber = true)
    {
        
        double? num2 = null;

        double num1 = UserInputHandler.AskForNumber();

        if (useSecondNumber == true)
        {
            num2 = UserInputHandler.AskForNumber();
            Menu.DisplayCalculationMenu();
        }
        else
        {
            Menu.DisplayAdvancedCalculationMenu();
        }

        ResultHander(num1, num2, calculator);
    }    

    public static void InitHistoryCalculator(Calculator calculator)
    {
        if (calculations.Any() == false)
        {
            Console.WriteLine("You haven't performed any calculation yet.");
            return;
        }

        double num1 = 0;
        double? num2 = null;

        Menu.DisplayCalculationOptions();
        string choice = Console.ReadLine();

        if (choice != "p" && choice != "a")
        {
            return;
        }

        PrintCalculations();

        if (choice == "p")
        {
            Console.WriteLine("Please select two numbers, from the following list range, that correspond to the calculation history");                        

            int index1 = UserInputHandler.AskForInt();
            index1 = UserInputHandler.GetIntAfterOutOfBoundsCheck(index1, calculations.Count);
            num1 = calculations[index1 - 1].Result;

            int index2 = UserInputHandler.AskForInt();
            index2 = UserInputHandler.GetIntAfterOutOfBoundsCheck(index2, calculations.Count);
            num2 = calculations[index2 - 1].Result;

            Menu.DisplayCalculationMenu();
            
        }
        else if (choice == "a")
        {
            Console.WriteLine("Please select one number, from the following list range, that correspond to the calculation history");

            int index1 = UserInputHandler.AskForInt();
            index1 = UserInputHandler.GetIntAfterOutOfBoundsCheck(index1, calculations.Count);
            num1 = calculations[index1 - 1].Result;

            Menu.DisplayAdvancedCalculationMenu();            
        }

        ResultHander(num1, num2, calculator);
    }    

    private static void StoreCalculation(double firstOperand, double? secondOperand, string operation, double result)
    {
        calculations.Add(new CalculationModel
        {
            FirstOperand = firstOperand,
            SecondOperand = secondOperand,
            Operation = operation,
            Result = result
        });
    }

    public static void PrintCalculations()
    {
        if (calculations.Any() == false)
        {
            Console.WriteLine("You haven't performed any calculation yet.");
            return;
        }

        int index = 1;

        foreach (var calculation in calculations)
        {
            if (calculation.SecondOperand == null)
            {
                Console.WriteLine($"{index}) {calculation.Operation}({calculation.FirstOperand}) = {calculation.Result} ");
            }
            else
            {
                Console.WriteLine($"{index}) {calculation.FirstOperand} {calculation.Operation} {calculation.SecondOperand} = {calculation.Result} ");
            }
            index++;
        }
    }

    public static void DeleteCalculations()
    {
        calculations.Clear();
        Console.WriteLine("All the Calculations have been deleted.");
    }

    private static void ResultHander(double num1, double? num2, Calculator calculator)
    {
        double result = 0;

        string operation = UserInputHandler.GetOperation();

        result = GetResult(num1, num2, operation, calculator);

        if (double.IsNaN(result))
        {
            Console.WriteLine("This operation will result in a mathematical error.\n");
            return;
        }

        Printer.PrintResult(result);

        StoreCalculation(num1, num2, operation, result);

        Console.WriteLine("------------------------\n");
    }

    private static double GetResult(double firstNumber, double? secondNumber, string operation, Calculator calculator)
    {
        double result = 0;

        try
        {
            result = calculator.DoOperation(firstNumber, secondNumber, operation);
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }

        return result;
    }
}
