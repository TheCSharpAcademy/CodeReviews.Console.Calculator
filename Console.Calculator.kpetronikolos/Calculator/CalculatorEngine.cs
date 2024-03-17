using CalculatorLibrary;
using CalculatorLibrary.Models;

namespace CalculatorProgram;

public static class CalculatorEngine
{
    private static List<CalculationModel> calculations = new List<CalculationModel>();

    public static void InitCalculator(Calculator calculator)
    {

        // Declare variables and set to empty.
        string numInput1 = "";
        string numInput2 = "";
        double result = 0;

        // Ask the user to type the first number.
        Console.Write("Type a number, and then press Enter: ");
        numInput1 = Console.ReadLine();

        double cleanNum1 = 0;
        while (!double.TryParse(numInput1, out cleanNum1))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput1 = Console.ReadLine();
        }

        // Ask the user to type the second number.
        Console.Write("Type another number, and then press Enter: ");
        numInput2 = Console.ReadLine();

        double cleanNum2 = 0;
        while (!double.TryParse(numInput2, out cleanNum2))
        {
            Console.Write("This is not valid input. Please enter an integer value: ");
            numInput2 = Console.ReadLine();
        }

        Menu.DisplayCalculationMenu();

        string op = Console.ReadLine();

        try
        {
            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else
            {
                Console.WriteLine("Your result: {0:0.##}\n", result);
                StoreCalculation(cleanNum1, cleanNum2, op, result);
            }
                

        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }

        Console.WriteLine("------------------------\n");
    }

    private static void StoreCalculation(double firstOperand, double secondOperand, string operation, double result)
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

        if (calculations.Any() == true)
        {
            foreach (var calculation in calculations)
            {
                Console.WriteLine($"{calculation.FirstOperand} {calculation.Operation} {calculation.SecondOperand} = {calculation.Result} ");
            }
        }
        else
        {
            Console.WriteLine("You haven't performed any calculation yet.");
        }
    }

    public static void DeleteCalculations()
    {
        calculations.Clear();
        Console.WriteLine("All the Calculations have been deleted.");
    }
}
