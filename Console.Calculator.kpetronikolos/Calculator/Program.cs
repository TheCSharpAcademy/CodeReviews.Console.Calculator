using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        var calculator = new Calculator();

        Printer.PrintWelcomeMessage();

        Menu.MainFunctionality(calculator);        

        calculator.Finish();
        return;
    }
}