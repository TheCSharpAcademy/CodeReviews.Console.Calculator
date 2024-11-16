namespace CalculatorLibrary.UI.Menu;

public static class MenuRenderer
{
    public static void Render(MainMenu menu)
    {
        Console.WriteLine("What do you want to do next?");
        Console.Write(menu.ToString());
    }
}