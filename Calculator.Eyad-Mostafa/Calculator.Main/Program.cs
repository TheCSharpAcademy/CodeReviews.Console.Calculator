using CalculatorLibrary;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        var calculator = new Calculator();

        int numberOfCalculations = 0;
        while (!endApp)
        {
            Console.Clear();
            Console.WriteLine($"Calculations number : {++numberOfCalculations}\n");
            Console.WriteLine(@"Please Choose Mode:
1- Two Operands Calculations
2- Trigonometry functions
3- More ( Square Root, 10x )
4- Latest calculations
");
            ChooseMode(calculator, ref numberOfCalculations);

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine()?.Trim().ToLower() == "n")
                endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }

    static string ChooseMode(Calculator calculator, ref int numberOfCalculation)
    {
        string? mode = Console.ReadLine();
        switch (mode)
        {
            case "1":
                CalculatorEngine. TwoOperandsCalculations(calculator, ref numberOfCalculation);
                break;
            case "2":
                CalculatorEngine.TrigonometryFunctions(calculator, ref numberOfCalculation);
                break;
            case "3":
                CalculatorEngine.MoreOperations(calculator, ref numberOfCalculation);
                break;
            case "4":
                CalculatorEngine.ShowHistory();
                break;
            default:
                Console.WriteLine("This is not valid input. Please enter an one numeric value:");
                ChooseMode(calculator, ref numberOfCalculation);
                break;
        }
        return mode;
    }
    
}