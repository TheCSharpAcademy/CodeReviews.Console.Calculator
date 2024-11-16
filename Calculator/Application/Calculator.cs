using CalculatorLibrary.Logic;
using CalculatorLibrary.UI;
using CalculatorLibrary.UI.ChoiceReader;
using CalculatorLibrary.UI.Menu;

namespace Calculator.Application;

public class Calculator(
    MainMenu mainMenu,
    IChoiceReader choiceReader,
    IKeyAwaiter keyAwaiter,
    CalculationRunner calculationRunner)
{
    private readonly Operations _performedOperations = new();

    public void Run()
    {
        var shouldQuit = false;
        do
        {
            Console.Clear();
            Console.WriteLine($"You used the calculator {_performedOperations.Count} times.");
            MenuRenderer.Render(mainMenu);
            var menuChoice = choiceReader.GetChoice<MenuChoices>();
            switch (menuChoice)
            {
                case MenuChoices.Quit:
                    shouldQuit = true;
                    break;
                case MenuChoices.StartNewCalculation:
                    calculationRunner.Run(_performedOperations);
                    break;
                case MenuChoices.ClearHistory:
                    ClearHistory();
                    break;
            }
        } while (!shouldQuit);

        Console.WriteLine("Thank you for using the Calculator!");
    }

    private void ClearHistory()
    {
        Console.Clear();
        _performedOperations.Clear();
        Console.WriteLine("History cleared. Press any key to continue...");
        keyAwaiter.Wait();
    }
}