namespace Academy.Console.Calculator;

using CalculatorLibrary;

class Program
{
    public static void Main(string[] args)
    {
        Calculator calculator = new();
        Calculator.MainMenu.ShowMenu();
        Calculator.ShowMemory();
        calculator.GetOperands();

        while (true)
        {
            Calculator.MainMenu.ShowMenu();
            Calculator.ShowMemory();
            Calculator.GetSelection();
            calculator.DoOperation();
        }
    }
}