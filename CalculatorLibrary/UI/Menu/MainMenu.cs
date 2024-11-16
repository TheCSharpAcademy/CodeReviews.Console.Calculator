namespace CalculatorLibrary.UI.Menu;

public class MainMenu
{
    public override string ToString()
    {
        return $@"{Convert.ToChar(MenuChoices.StartNewCalculation)}: Start new calculation
{Convert.ToChar(MenuChoices.ClearHistory)}: Clear history
{Convert.ToChar(MenuChoices.Quit)}: Quit
";
    }
}