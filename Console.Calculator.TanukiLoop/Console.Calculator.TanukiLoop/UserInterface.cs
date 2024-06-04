using CalculatorLibrary;
using Spectre.Console;

namespace CalculatorProgram;

public class UserInterface
{

    public static string MainMenuPrompt(List<MathOperation> calcHistory)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLineInterpolated($"Calculator usage count: {calcHistory.Count}");
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Main Menu: Select an option. Use arrow keys or start typing to highlight the option and enter to submit selection.")
                .AddChoices([
                    "Add",
                    "Subtract",
                    "Multiply",
                    "Divide",
                    "Clear Calculation History",
                    "Quit"
                ])
                .EnableSearch()
            
        );

        
        return selection;

    }


    public static double PreviousCalculationResultSelectionPrompt(List<MathOperation> calculationHistory)
    {
        var mathOperation = AnsiConsole.Prompt(
            new SelectionPrompt<MathOperation>()
                .Title("Select a calculation result to use (use arrow keys):")
                .AddChoices(calculationHistory)
            );

        return mathOperation.result;
    }

    public static double PreviousCalculationResultPrompt(List<MathOperation> calculationHistory)
    {

        var table = new Table();
        
        // Add Columns
        table.AddColumn("No.");
        table.AddColumn("Calculation");

        for (int i = 0; i < calculationHistory.Count(); i++)
        {
            table.AddRow(i.ToString(), calculationHistory[i].ToString());
        }
        
        //Show table
        AnsiConsole.Write(table);
        
        // Prompt for calculation No.

        // const int MaxCalculationNo = calculationHistory.Count();
        

        var calculationNo = AnsiConsole.Prompt(
            new TextPrompt<int>("Enter calculation No.")
                .PromptStyle("darkorange3")
                .ValidationErrorMessage("[red]That's not a valid calculation No.[/]")
                .Validate(calculationNo =>
                {
                    if (calculationNo < 0)
                    {
                        return ValidationResult.Error("The calculation No. cannot be less than zero.");
                        
                    }
                    
                    if (calculationNo >= calculationHistory.Count)
                    {
                        return ValidationResult.Error($"The calculation No. must be less than {calculationHistory.Count()}");
                    }
                    
                    return ValidationResult.Success();
                    // return calculationNo swi
                })
        );


        return calculationNo;

    }

    public static double PromptForNumber()
    {
        double number = AnsiConsole.Prompt(
            new TextPrompt<double>("Enter the operand number value")
                .PromptStyle("darkorange3")
                .ValidationErrorMessage("[red]That's not a valid number[/]")
        );

        return number;
    }

    public static double PromptForNumberOrCalculationResult(List<MathOperation> calculationHistory)
    {
        if (!calculationHistory.Any())
        {
            return PromptForNumber();

        }
        
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Enter a number via input or previous a previous result?")
                .AddChoices(
                    "Enter number input",
                    "Use a previous result"
                )
        );

        var number = selection switch
        {
            "Use a previous result" => PreviousCalculationResultSelectionPrompt(calculationHistory),
            _ => PromptForNumber()
        };

        return number;

    }
    
}