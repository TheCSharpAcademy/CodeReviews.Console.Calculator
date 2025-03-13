using Spectre.Console;
using static Calculator.S1m0n32002.CalculatorController;

namespace Calculator.S1m0n32002
{
    static class Calculator
    {
        public enum MenuOptions
        {
            Start,
            History,
            UsageCounter,
            Exit,
            Yes,
            No,
        }

        public readonly static Dictionary<string, MenuOptions> strMenuOptions = new()
        {
            {"[bold green]Start[/]" ,MenuOptions.Start },
            {"History"              ,MenuOptions.History },
            {"Usage Counter"        ,MenuOptions.UsageCounter },
            {"[Yellow]Exit[/]"      ,MenuOptions.Exit },
            {"No :("                ,MenuOptions.Yes },
            {"No"                   ,MenuOptions.No },
        };

        public static void CalculatorStart()
        {
            while (true)
            {
                WriteTitle();

                var prompt = new SelectionPrompt<string>()
                    .AddChoices([.. 
                            strMenuOptions.Keys.Where(x => strMenuOptions[x] != MenuOptions.Yes ||
                                                                    strMenuOptions[x] != MenuOptions.No)
                        ]);

                var answer = AnsiConsole.Prompt(prompt);

                if (strMenuOptions.TryGetValue(answer, out var menuOption))
                {
                    switch (menuOption)
                    {
                        case MenuOptions.Start:
                            DoCalc();
                            break;
                        case MenuOptions.History:
                            //ShowHistory;
                            break;
                        case MenuOptions.UsageCounter:
                            //ShowUsageCounter();
                            break;
                        case MenuOptions.Exit:
                            var anser = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            {
                                Title = "Are you sure you want to exit?"
                            }.AddChoices([.. strMenuOptions.Keys.Where(x => strMenuOptions[x] == MenuOptions.Yes ||
                                                                                  strMenuOptions[x] == MenuOptions.No)]));
                            
                            if (strMenuOptions.TryGetValue(anser, out var exitOption))
                            {
                                if (exitOption == MenuOptions.Yes)
                                    return;
                                else
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Writes the title on top of the console
        /// <\summary>
        static void WriteTitle()
        {
            Console.Clear();

            AnsiConsole.Write(new Rule("[white]Console Calculator in C#[/]").RuleStyle(Style.Parse("yellow")));
            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Prompt the user to choose an operation
        /// </summary>
        /// <returns></returns>
        static Operations? PromptOperationChoice()
        {
            var prompt = new SelectionPrompt<string>()
            {
                Title = "Choose an operation"
            }
            .AddChoices([.. strOperations.Keys, "[yellow]Exit[/]"]);

            var answer = AnsiConsole.Prompt(prompt);

            if (strOperations.TryGetValue(answer, out var operation))
                return operation;
            else
                return null;
        }

        static void DoCalc()
        {
            WriteTitle();

            var answer = PromptOperationChoice();
            if (answer == null)
                return;

            Operations operation = (Operations)answer;

            var numbers = CompileOperation(operation);

            try
            {
                var result = DoOperation(operation, [.. numbers]);

                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine("Your result: {0:0.00}\n", result);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteLine("Oh no! An exception occurred trying to do the math.\n");
                AnsiConsole.WriteException(e);
            }

            AnsiConsole.MarkupLine("Press [blue]ENTER[/] to continue");
            Console.ReadLine();
        }

        static IEnumerable<double> CompileOperation(Operations operation)
        {
            List<double> numbers = [];

            numbers.Add(AnsiConsole.Prompt(new TextPrompt<double>("Enter one number number:")));

            /// Prompt the user for the second number if needed
            switch (operation)
            {
                case Operations.Divide:
                    numbers.Add(AnsiConsole.Prompt(new TextPrompt<double>("Enter the second number")
                    {
                        Validator = (input) =>
                        {
                            if (input == 0)
                                return ValidationResult.Error("Cannot divide by zero");
                            return ValidationResult.Success();
                        }
                    }));
                    break;

                case Operations.Sum:
                case Operations.Subtract:
                case Operations.Multiply:
                case Operations.Power:
                    numbers.Add(AnsiConsole.Prompt(new TextPrompt<double>("Enter the second number")));
                    break;

                default:
                    break;
            }

            return numbers;
        }
    }
}
