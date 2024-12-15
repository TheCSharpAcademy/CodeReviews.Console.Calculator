using CalculatorLibrary;
using Spectre.Console;
using System.Globalization;

public class MainMenu
{
    public static int ShowMainMenu(Calculator calculator)
    {
        Console.Clear();
        var choice = "";

        if (calculator.SelectedResult == 0)
        {
            choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an Option: ")
                    .PageSize(10)
                    .AddChoices([
                         "Use Simple calculator",
                         "Use Advanced calculator",
                         "Show List of operations",
                         "Use previous result",
                         "Clear List of operations",
                         "Exit the app"
                    ]));
        }
        else
        {
            choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose Calculator: ")
                    .PageSize(10)
                    .AddChoices([
                         "Use Simple calculator",
                         "Use Advanced calculator",
                         "Back"
                    ]));

            if (choice == "Back")
            {
                calculator.SelectedResult = 0;
                return 1;
            }
        }

        if (choice == "Use Simple calculator")
        {
            while (true)
            {
                SimpleCalculatorMenu.ShowSimpleCalculatorMenu(calculator);

                if (AskToContinue() == 1)
                    return 1;
            }
        }
        else if (choice == "Use Advanced calculator")
        {
            while (true)
            {
                DisplayHelpers.ShowAdvancedCalculatorInfo();

                string input = "";

                if (calculator.SelectedResult == 0)
                    input = AnsiConsole.Prompt(new TextPrompt<string>("Enter expression: "));
                else
                {
                    input = AnsiConsole.Prompt(
                        new TextPrompt<string>($"Continue expression: [yellow]{calculator.SelectedResult}[/]"));
                    input = calculator.SelectedResult + input;
                }

                calculator.SelectedResult = 0;

                try
                {
                    string? result = calculator.Calculate(input);

                    while (true)
                    {
                        if(result != null)
                        {
                            AnsiConsole.MarkupLine($"Your result: [yellow]{result}[/]");
                            break;
                        }
                        else
                        {
                            input = AnsiConsole.Prompt(new TextPrompt<string>("Enter expression: "));
                            result = calculator.Calculate(input);
                        }
                    }
                }
                catch (Exception e)
                {
                    AnsiConsole.MarkupLine($"[red]{e.Message}[/]");
                }

                if (AskToContinue() == 1) return 1;
            }
        }
        else if (choice == "Show List of operations")
        {
            JSONHelpers jSONHelpers = new JSONHelpers();
            List<string> listOfOperations = jSONHelpers.GetListOfOperations();

            if (listOfOperations.Count > 0)
            {
                var table = new Table();
                table.AddColumn("Operations:");

                foreach (string operation in listOfOperations)
                    table.AddRow(operation);

                AnsiConsole.Write(table);

                return PressAnyKeyToExitToMenu();
            }
            else return NotifyIfListIsEmpty();
        }
        else if (choice == "Use previous result")
        {
            JSONHelpers jSONHelpers = new JSONHelpers();
            List<string> listOfOperations = jSONHelpers.GetListOfOperations();

            if (listOfOperations.Count > 0)
            {
                List<string> listOfPreviousResults = JSONHelpers.RetrieveResults();
                
                string result = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose from previous results: ")
                        .PageSize(10)
                        .AddChoices(listOfPreviousResults)
                        .AddChoices("Back"));

                while (true)
                {
                    if (result == "Back") return 1;
                    else if (Convert.ToDouble(result) == 0)
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[red]Please choose another previous result, it must be different from 0.[/]");
                        
                        result = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Previous results: ")
                                .PageSize(10)
                                .AddChoices(listOfPreviousResults)
                                .AddChoices("Back"));
                    }
                    else if (double.TryParse(result, CultureInfo.InvariantCulture, out double doublelValue))
                    {
                        calculator.SelectedResult = doublelValue;
                        return 1;
                    }
                    else
                    {
                        Console.Clear();
                        AnsiConsole.MarkupLine("[red]Failed to parse it.[/]");

                        result = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please choose another previous result.")
                                .PageSize(10)
                                .AddChoices(listOfPreviousResults)
                                .AddChoices("Back"));
                    }
                }
            }
            else return NotifyIfListIsEmpty();
        }

        else if (choice == "Clear List of operations")
        {
            JSONHelpers jSONHelpers = new JSONHelpers();
            List<string> listOfOperations = jSONHelpers.GetListOfOperations();

            if (listOfOperations.Count > 0)
            {
                AnsiConsole.MarkupLine("[red]You want to clear list of all operations![/]\n");

                var answer = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[red]Are you sure?[/]")
                        .PageSize(5)
                        .AddChoices([
                        "Yes",
                        "No"
                        ]));

                if (answer == "Yes")
                {
                    File.Delete(JSONHelpers.LogFilePath);
                    using (StreamWriter logFile = File.CreateText(JSONHelpers.LogFilePath)) { }
                    jSONHelpers.Operations = new List<Operation>();

                    Console.Clear();
                    AnsiConsole.MarkupLine("[green]List of operations was cleared![/]");
                    return PressAnyKeyToExitToMenu();
                }
                else return 1;
            }
            else return NotifyIfListIsEmpty();
        }
        else if (choice == "Exit the app")
        {
            return 0;
        }

        return 1;
    }

    static int PressAnyKeyToExitToMenu()
    {
        AnsiConsole.Status()
            .Start("[yellow]Press any key to exit to main menu.[/]", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                Console.ReadKey(true);
            });
        return 1;
    }

    static int NotifyIfListIsEmpty()
    {
        AnsiConsole.MarkupLine("[yellow]List is empty.[/]");
        return PressAnyKeyToExitToMenu();
    }

    static int AskToContinue()
    {
        int result = 0;
        AnsiConsole.Status()
            .Start("\n[yellow]Press Esc key to exit to main menu, or any other key to continue: [/]", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    result = 1;
                else
                {
                    Console.Clear();
                    result = -1;
                }
            });
        return result;
    }
}
