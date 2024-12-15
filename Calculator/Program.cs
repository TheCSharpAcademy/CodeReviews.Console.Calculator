using CalculatorLibrary;
using Spectre.Console;

class Program
{
    static async Task Main()
    {
        Calculator calculator = new Calculator();

        while (true)
        {
            if(MainMenu.ShowMainMenu(calculator) == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Goodbye![/]");
                await Task.Delay(1000);
                return;
            }
        }
    }
}
