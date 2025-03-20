using consoleCalculator.Samuel_D1906;
using consoleCalculator.Samuel_D1906.CalculatorLibrary;


using Spectre.Console;
public class UserInterface
{
    private static bool _isRunning = true;
    static List<double> _calculations = [];
    internal static void GetMenu()
    {
        while (_isRunning)
        {

            var userOption = AnsiConsole.Prompt(new SelectionPrompt<Enums.MenuOptions>()
                .Title("Console Calculator in C#\r").AddChoices(Enums.MenuOptions.DoOperation,
                    Enums.MenuOptions.ShowList, Enums.MenuOptions.DeleteList, Enums.MenuOptions.Quit));

            switch (userOption)
            {
                case Enums.MenuOptions.DoOperation:
                    _calculations = CalculatorController.Operation(_calculations);
                    break;
                case Enums.MenuOptions.ShowList:
                    Calculator.ShowList(_calculations);
                    break;
                case Enums.MenuOptions.DeleteList:
                    Calculator.DeleteList(_calculations);
                    break;
                case Enums.MenuOptions.Quit:
                    Console.WriteLine("Goodbye!");
                    _isRunning = false;
                    break;
            }
        }
    }
    

    
}

