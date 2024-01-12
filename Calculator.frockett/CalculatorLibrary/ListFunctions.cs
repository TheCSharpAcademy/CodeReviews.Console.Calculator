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
        Console.Clear();
        Console.WriteLine("Calculation History");
        Console.WriteLine("___________________");
        foreach (CalculationHistory calculation in history)
        {
            Console.WriteLine($"{calculation.Num1} {calculation.Operation} {calculation.Num2} = {calculation.Result} at index {calculation.Index}");
        }
        Console.WriteLine("\nPress any key to return to main menu");
        Console.ReadLine();
    }

    public void ClearList()
    {
        history.Clear();
    }

}
