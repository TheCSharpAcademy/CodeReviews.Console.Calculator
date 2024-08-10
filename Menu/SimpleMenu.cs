namespace SimpleMenuLibrary;
public struct Option
{
    public string Description;
    public string Symbol;
}

public class Menu
{
    public string? Title;
    public List<Option> Options = [];

    public void AddOption(string[] option)
    {
        Options?.Add(new Option {Description = option[0],Symbol = option[1]});
    }

    public void ShowMenu(bool clear = true)
    {
        if (clear)
            Console.Clear();
        Console.WriteLine($"{Title}".ToUpper());
        Console.WriteLine("".PadRight(40, '-'));
        foreach (Option option in Options)
        {
            Console.Write($"{option.Symbol}".PadRight(20));
            Console.Write($"{option.Description}\n");
        }
        Console.WriteLine("".PadRight(40, '-'));
    }

    public string Prompt(string promptText = "Please enter your selection:")
    {
        string? input = null;
        
        while (!CheckInput(input ??= "no input"))
        {
            Console.Write($"{promptText}".ToUpper());
            input = Console.ReadLine();
        }
        return input.ToLower();
    }

    private bool CheckInput(string input)
    {
        if (input == null)
            return false;
        if (Options != null)
        {
            foreach (Option option in Options)
            {
                if (option.Symbol == input)
                    return true;
            }
        }
        return false;
    }
}