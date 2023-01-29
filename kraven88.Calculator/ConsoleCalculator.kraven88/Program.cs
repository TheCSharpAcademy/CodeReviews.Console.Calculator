using CalculatorLibrary;
using CalculatorLibrary.Models;
using ConsoleCalculator.kraven88;
using System.Text;

namespace CalculatorProgram;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;  // This makes the root sign to properly display in Console window.

        string filePath = "CalculatorLog.json";
        List<Equation> equations = DataAccess.LoadEquations(filePath);
        Calculator calc = new Calculator(equations, filePath);

        var menu = new MainMenu(filePath, equations, calc);

        menu.SelectionScreen();
    }

    
}