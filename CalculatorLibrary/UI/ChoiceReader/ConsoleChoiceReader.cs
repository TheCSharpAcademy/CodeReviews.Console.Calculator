using CalculatorLibrary.ConsoleWrapper;

namespace CalculatorLibrary.UI.ChoiceReader;

public sealed class ConsoleChoiceReader(IConsoleWrapper consoleWrapper) : IChoiceReader
{
    public TChoice GetChoice<TChoice>() where TChoice : Enum
    {
        var positionTop = Console.CursorTop;
        var positionLeft = Console.CursorLeft;
        char choice;
        do
        {
            Console.Write(' ');
            Console.SetCursorPosition(positionLeft, positionTop);
            choice = consoleWrapper.ReadKey(true).KeyChar;
            Console.SetCursorPosition(positionLeft, positionTop);
        } while (!Enum.IsDefined(typeof(TChoice), (int)choice));

        return (TChoice)(object)(int)choice;
    }
}