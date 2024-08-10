namespace Console.Calculator.cyanitol;

using global::Calculator;

class Program
{
    public static void Main(string[] args)
    {
        Calculator calculator = new();
        Calculator.MainMenu.ShowMenu(footerContent: Calculator.GetMemory());
        calculator.GetOperands();

        while (true)
        {
            Calculator.MainMenu.ShowMenu(footerContent: Calculator.GetMemory());
            Calculator.GetSelection();
            calculator.DoOperation();
        }
    }
}