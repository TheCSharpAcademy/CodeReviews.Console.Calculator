using Calculator.BNAndras.CalculatorLibrary;
using Calculator.BNAndras.CalculatorLibrary.Models;
using Calculator.BNAndras.CalculatorProgram.Models;

namespace Calculator.BNAndras.CalculatorProgram;

internal static class MenuController
{
    private static IEnumerable<(bool Displayable, CalculationResult Result)> Results { get; set; } = [];

    public static MenuOperation GetMenuOperation() => 
        DisplayController.PromptForOperation("Please select a menu operation", Enum.GetValues<MenuOperation>());

    internal static void Quit()
    {
        JsonController.LogResults(Results.Select(r => r.Result));
        Environment.Exit(1);
    }

    internal static void ShowHistory() =>
        DisplayController.DisplaySessionHistory(
            Results.Where(r => r.Displayable)
                   .Select(r => r.Result));

    public static void ClearHistory()
    {
        Results = Results.Select(r => (false, r.Result));
    
        DisplayController.DisplayClearHistory();
    }

    public static void DisplayOperationCount()
    {
        IEnumerable<(MathOperation operation, int count)> operations = Results
            .Where(r => r.Displayable)
            .GroupBy(r => r.Result.Operation)
            .Select(g => (operation: g.Key, count: g.Count()));

        DisplayController.DisplayOperationCounts(operations);
    }

    public static void CalculateValue(ref double previousResult)
    {
        MathOperation mathOperation =
            DisplayController.PromptForOperation("Please select a menu operation", Enum.GetValues<MathOperation>());

        List<double> operands = GetOperandsFor(mathOperation, previousResult);
        try
        {
            CalculationResult calculationResult = CalculatorController.Do(mathOperation, operands);
            Results = Results.Append((true, calculationResult));
            DisplayController.WriteMarkup(double.IsNaN(calculationResult.Result)
                ? "[red]This operation will result in a mathematical error.[/]"
                : $"[green]Your result: {calculationResult.Result:0.##}[/]");
        }
        catch (Exception e)
        {
            DisplayController.WriteMarkup($"""
                                           Oh no! An exception occurred trying to do the math.
                                           Details: [red]{e.Message}[/]
                                           """);
        }

        DisplayController.AlertUser("Press any key to continue.");

        previousResult = double.NaN;
    }

    public static double ReuseLastResult()
    {
        if (Results.Count() is 0)
        {
            DisplayController.AlertUser("No previous calculation is available.");
            return double.NaN;
        }

        double previousResult = Results.Last().Item2.Result;
        DisplayController.AlertUser($"Reusing the last result {previousResult}. Press any key to continue");
        return previousResult;
    }


    private static List<double> GetOperandsFor(MathOperation mathOperation, double operand1 = double.NaN)
    {
        List<double> operands =
        [
            operand1 is not double.NaN
                ? operand1
                : DisplayController.GetDouble()
        ];

        if (CalculatorController.HasTwoOperands(mathOperation))
        {
            operands.Add(DisplayController.GetDouble());
        }

        return operands;
    }

    public static void ShowWelcomeScreen()
    {
        DisplayController.AlertUser("""
                                    Welcome to my C# Calculator.
                                    Press any key to continue.
                                    """);
    }

    public static void ClearDisplay() => DisplayController.ClearDisplay();
}