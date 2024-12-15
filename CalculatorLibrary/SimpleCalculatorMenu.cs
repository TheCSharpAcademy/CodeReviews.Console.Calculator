using Spectre.Console;
using CalculatorLibrary;

internal static class SimpleCalculatorMenu
{
    public static void ShowSimpleCalculatorMenu(Calculator calculator)
    {
        Console.Clear();

        double numInput1 = 0;
        double numInput2 = 0;
        double result = 0;

        if (calculator.SelectedResult == 0)
            numInput1 = AnsiConsole.Prompt(new TextPrompt<double>("Type a number, and then press Enter: "));
        else
        {
            numInput1 = (double)calculator.SelectedResult;
            AnsiConsole.MarkupLine($"You are using previous result [yellow]{numInput1}[/] from earlier calculation.");
            calculator.SelectedResult = 0;
        }

        var operation = ChooseMathOperator();

        if (operation == "Advanced operators")
        {
            operation = ChooseAdvancedOperator();

            if (operation == "Back")
                operation = ChooseMathOperator();
            else if (operation == "Taking the Power")
                numInput2 = AnsiConsole.Prompt(new TextPrompt<double>("Enter an exponent: "));
            else if (operation == "Trigonometry functions")
            {
                operation = ChooseTrigonometryFunction();
                if (operation == "Back")
                    operation = ChooseMathOperator();
            }
        }
        else
        {
            numInput2 = AnsiConsole.Prompt(new TextPrompt<double>("Type another number, and then press Enter: "));
        }

        try
        {
            result = calculator.GetResult(numInput1, numInput2, operation);

            if (double.IsNaN(result))
                AnsiConsole.MarkupLine("[red]This operation will result in a mathematical error.[/]\n");
            else 
                AnsiConsole.MarkupLine($"Your result: [yellow]{Calculator.RoundTheResult(result)}[/]");
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]Oh no! An exception occurred trying to do the math.[/]\n" +
                $" - Details: [purple3]{e.Message}[/]");
        }
    }

    static string ChooseMathOperator()
    {
        var operation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose an operator from the following list:")
                .PageSize(10)
                .AddChoices([
                     "Add",
                     "Subtract",
                     "Multiply",
                     "Divide",
                     "Advanced operators"
                ]));

        return operation;
    }

    static string ChooseAdvancedOperator()
    {
        var operation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose advanced operator:")
                .PageSize(10)
                .AddChoices([
                     "Square Root",
                     "Taking the Power",
                     "10x",
                     "Trigonometry functions",
                     "Back"
                ]));

        return operation;
    }

    static string ChooseTrigonometryFunction()
    {
        var operation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose Trigonometry function:")
                .PageSize(10)
                .AddChoices([
                     "Sine",
                     "Cosine",
                     "Tangent",
                     "Back"
                ]));

        return operation;
    }
}
