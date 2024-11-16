using CalculatorLibrary.ConsoleWrapper;
using CalculatorLibrary.Logic;
using CalculatorLibrary.UI.Operation;

namespace CalculatorLibrary.UI.OperandSource.HistoryReader;

public class HistoryOperandReader(Operations previousOperations, IConsoleWrapper consoleWrapper) : IOperandReader
{
    public double ReadOperand()
    {
        RenderHistory();
        Console.WriteLine("Choose index");
        var positionLeft = Console.CursorLeft;
        var positionTop = Console.CursorTop;
        while (true)
        {
            if (!int.TryParse(consoleWrapper.ReadLine(), out var index) ||
                previousOperations.ElementAtOrDefault(index - 1) is null)
            {
                Console.SetCursorPosition(positionLeft, positionTop);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            return previousOperations[index - 1].Result;
        }
    }

    private void RenderHistory()
    {
        for (var i = 0; i < previousOperations.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            OperationRenderer.Render(previousOperations[i]);
            Console.Write("\n");
        }
    }
}