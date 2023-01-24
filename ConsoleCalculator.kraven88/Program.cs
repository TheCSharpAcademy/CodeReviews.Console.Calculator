using CalculatorLibrary;
using CalculatorLibrary.Models;
using ConsoleCalculator.kraven88;
using System.Text;

namespace CalculatorProgram;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        string filePath = "CalculatorLog.json";
        List<Equasion> equasions = DataAccess.LoadEquasions(filePath);
        Calculator calc = new Calculator(equasions, filePath);

        var menu = new MainMenu(filePath, equasions, calc);

        menu.SelectionScreen();
    }

    
}