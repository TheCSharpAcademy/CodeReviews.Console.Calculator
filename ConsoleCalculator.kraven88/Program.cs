using CalculatorLibrary;
using ConsoleCalculator.kraven88;

namespace CalculatorProgram;

internal class Program
{
    private static void Main(string[] args)
    {
        var calculator = new Calculator();

        MainMenu.SelectionScreen(calculator);
        
    }

    
}