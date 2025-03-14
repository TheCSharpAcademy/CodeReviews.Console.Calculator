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

        public static readonly Dictionary<string, MenuOptions> strMenuOptions = new()
        {
            {"[bold green]Start[/]" ,MenuOptions.Start },
            {"History"              ,MenuOptions.History },
            {"Usage Counter"        ,MenuOptions.UsageCounter },
            {"[Yellow]Exit[/]"      ,MenuOptions.Exit },
            {"No :("                ,MenuOptions.Yes },
            {"No"                   ,MenuOptions.No },
        };

        /// <summary>
        /// Starts the calculator
        /// </summary>
        public static void StartCalculator()
        {
            while (true)
            {
                WriteTitle();

                var prompt = new SelectionPrompt<string>()
                    .AddChoices([..
                            strMenuOptions.Keys.Where(x => strMenuOptions[x] != MenuOptions.Yes &&
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
                            var result = ShowHistory();
                            if (result != null)
                                DoCalc(result);
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

        enum HistoryOperations
        {
            Exit,
            ClearHistory,
        }

        /// <summary>
        /// Shows the history of operations
        /// </summary>
        static OperationResult? ShowHistory()
        {
            while (true)
            {
                Dictionary<string, OperationResult> strResultLog = [];
                var rList = ResultLog.Reverse<OperationResult>();

                int c = 0;
                foreach (var result in rList)
                {
                    strResultLog.Add($"{++c}. {result}", result);
                }

                Dictionary<string, HistoryOperations> strHistoryOperations = new()
                {
                    { "[yellow]Exit[/]"         ,HistoryOperations.Exit },
                    { "[red]Clear History[/]"   ,HistoryOperations.ClearHistory },
                };

                var answer = AnsiConsole.Prompt(new SelectionPrompt<string>()
                {
                    Title = "History",
                }
                .AddChoices(strResultLog.Keys)
                .AddChoiceGroup("Menu", strHistoryOperations.Keys));

                if (strResultLog.TryGetValue(answer, out var operationResult))
                {
                    return operationResult;
                }

                if (strHistoryOperations.TryGetValue(answer, out var HO))
                {
                    switch (HO)
                    {
                        case HistoryOperations.Exit:
                            return null;
                        case HistoryOperations.ClearHistory:
                            ClearHistory();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Shows the history of operations
        /// </summary>
        static void ClearHistory()
        {
            Dictionary<string, bool> strYesNo = new()
            {
                { "No", false },
                { "Yes", true },
            };

            var answer = AnsiConsole.Prompt(new SelectionPrompt<string>()
            {
                Title = "History"
            }.AddChoices(strYesNo.Keys)
            );

            if (strYesNo.TryGetValue(answer, out var clearHistory))
            {
                if (clearHistory)
                    ResultLog.Clear();
            }
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

        /// <summary>
        /// Perform the operation
        /// </summary>
        static void DoCalc(OperationResult? existingResult = null)
        {
            WriteTitle();

            var answer = PromptOperationChoice();
            if (answer == null)
                return;

            Operations operation = (Operations)answer;

            var numbers = PrepareOperation(operation, existingResult);

            try
            {
                var result = DoOperation(operation, numbers.Item1, numbers.Item2);

                if (result == null)
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else Console.WriteLine(result);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteLine("Oh no! An exception occurred trying to do the math.\n");
                AnsiConsole.WriteException(e);
            }

            AnsiConsole.MarkupLine("Press [blue]ENTER[/] to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Prepares the data needed for the operation
        /// </summary>
        /// <param name="operation"> Requested operation </param>
        /// <param name="existingResult"> Existing result to use </param>
        /// <returns> List with the numbers to use during the calculation </returns>
        private static (double, double) PrepareOperation(Operations operation, OperationResult? existingResult = null)
        {
            double num1 = 0;
            double num2 = 0;

            if (existingResult != null)
                num1 = existingResult.Value;
            else
                num1 = AnsiConsole.Prompt(new TextPrompt<double>("Enter one number:"));

            /// Prompt the user for the second number if needed
            switch (operation)
            {
                case Operations.Divide:
                    num2 = AnsiConsole.Prompt(new TextPrompt<double>("Enter the second number:")
                    {
                        Validator = (input) =>
                        {
                            if (input == 0)
                                return ValidationResult.Error("Cannot divide by zero");
                            return ValidationResult.Success();
                        }
                    });
                    break;

                case Operations.Sum:
                case Operations.Subtract:
                case Operations.Multiply:
                case Operations.Power:
                    num2 = AnsiConsole.Prompt(new TextPrompt<double>("Enter the second number:"));
                    break;

                case Operations.TenPower:
                    num2 = num1;
                    num1 = 10;
                    break;

                default:
                    break;
            }

            return (num1, num2);
        }
    }
}
