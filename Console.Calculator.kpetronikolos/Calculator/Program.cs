using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        var calculator = new Calculator();

        while (!endApp)
        {
            Menu.DisplayMainMenu(); 

            string selection = Console.ReadLine();

            switch (selection.Trim().ToLower())
            {
                case "p":
                    CalculatorEngine.InitCalculator(calculator);
                    break;
                case "a":
                    CalculatorEngine.InitAdvancedCalculator(calculator);
                    break;
                case "c":
                    Printer.PrintCalculatorCount(calculator.GetCalculatorCount());
                    break;
                case "v":
                    CalculatorEngine.PrintCalculations();
                    break;
                case "d":
                    CalculatorEngine.DeleteCalculations();
                    break;
                case "h":
                    CalculatorEngine.InitHistoryCalculator(calculator);
                    break;
                case "q":
                    Console.WriteLine("Thanks for playing. \nGame Over ...");
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.\n");
                    break;
            }

            Console.WriteLine("\n");
        }

        calculator.Finish();
        return;
    }
}