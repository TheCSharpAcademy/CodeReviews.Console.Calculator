using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
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
                case "c":
                    Printer.PrintCalculatorCount(calculator.GetCalculatorCount());
                    break;
                case "q":
                    Console.WriteLine("Thanks for playing. \nGame Over ...");
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.\n");
                    break;
            }

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        // Add call to close the JSON writer before return
        calculator.Finish();
        return;
    }
}