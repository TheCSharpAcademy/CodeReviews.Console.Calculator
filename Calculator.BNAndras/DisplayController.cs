using System.Text.RegularExpressions;
using Calculator.BNAndras.CalculatorLibrary.Models;
using Spectre.Console;

namespace Calculator.BNAndras.CalculatorProgram;

internal static class DisplayController
{

    private static IAnsiConsole Output { get; } = AnsiConsole.Create(new());

    private static Regex FindCamelCase { get; } = new(@"(?=[A-Z])");

    internal static T PromptForOperation<T>(string title, T[] choices, bool clearScreen = true) where T : notnull
    {
        T result = Output.Prompt(
            new SelectionPrompt<T>()
                .Title(title)
                .AddChoices(choices)
                .UseConverter(SplitOnCamelCase)
        );

        if (clearScreen)
        {
            Output.Clear();
        }

        return result;
    }

    private static void WriteHeader(string header)
    {
        Output.WriteLine(header);
        Output.Write(new Rule());
    }

    internal static void DisplaySessionHistory(IEnumerable<CalculationResult> history)
    {
        WriteHeader("Calculator history for current session, sorted from most recent to oldest.");

        Table table = new();
        table.AddColumn(new TableColumn("Operation").Centered());
        table.AddColumn(new TableColumn("Inputs").Centered());
        table.AddColumn(new TableColumn("Result").Centered());
        table.Border(TableBorder.Square);

        foreach (var (operation, inputs, result) in history)
        {
            table.AddRow(SplitOnCamelCase(operation), (string.Join(", ", inputs)), $"{result:0.##}");
        }

        Output.Write(table);

        AlertUser("Press any key to continue");
    }

    internal static void DisplayOperationCounts(IEnumerable<(MathOperation operation, int count)> counts)
    {

        WriteHeader("Printing a list of how often math operations were used in the current session.");


        Table table = new();
        table.AddColumn(new TableColumn("Operation").LeftAligned());
        table.AddColumn(new TableColumn("Count").LeftAligned());
        table.Border(TableBorder.Square);

        foreach ((MathOperation operation, int count) in counts)
        {
            table.AddRow(SplitOnCamelCase(operation), count.ToString());
        }

        Output.Write(table);

        AlertUser("Press any key to continue.");
    }

    internal static void AlertUser(string message)
    {

        Output.WriteLine(message);
        Output.Input.ReadKey(false);
        Output.Clear();
    }

    private static string SplitOnCamelCase<T>(T en) =>
        string.Join(' ', FindCamelCase.Split($"{en}"));

    internal static double GetDouble() => 
        Output.Prompt(
            new TextPrompt<double>("Please enter a number:?"));

    internal static void DisplayClearHistory()
    {
        WriteHeader("Clearing session history...");
        AlertUser("History is cleared. Press any key to continue.");
    }

    internal static void WriteMarkup(string message) => Output.MarkupLine(message);

    internal static void ClearDisplay() => Output.Clear();
}