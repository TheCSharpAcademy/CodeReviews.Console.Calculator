using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary;

public class ListFunctions
{
    static List<CalculationHistory> history = new();

    public void WriteList(double num1, double num2, string operation, double result, int index)
    {
        history.Add(new CalculationHistory
        {
            Num1 = num1,
            Num2 = num2,
            Operation = operation,
            Result = result,
            Index = index,
        });
    }

    public void PrintList()
    {
        Console.WriteLine("\nCalculation History");
        Console.WriteLine("___________________");
        foreach (CalculationHistory calculation in history)
        {
            Console.WriteLine($"Calculation #{calculation.Index}: {calculation.Num1} {calculation.Operation} {calculation.Num2} = {calculation.Result}");
        }
        Console.WriteLine("\nPress any key to return to main menu");
        Console.ReadLine();
    }

    public void ClearList()
    {
        history.Clear();
        Console.WriteLine("History has been cleared\n");
        Console.WriteLine("Press enter to return to main menu...");
        Console.ReadLine();
    }

}
