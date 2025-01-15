using Spectre.Console;
using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int usageCount = 0;
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();

            while (!endApp)
            {
                Console.Clear();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("Perform a new calculation", "Use previous result as operand", "View previous calculations", "Clear memory", "Exit"));

                switch (choice)
                {
                    case "Perform a new calculation":
                        PerformCalculation(calculator, ref usageCount);
                        break;

                    case "Use previous result as operand":
                        UsePreviousResult(calculator, ref usageCount);
                        break;

                    case "View previous calculations":
                        calculator.ViewCalculations();
                        break;

                    case "Clear memory":
                        calculator.ClearCalculatorMemory();
                        AnsiConsole.MarkupLine("[green]Memory cleared![/]");
                        Console.WriteLine("\nPress Any Key to Continue.");
                        Console.ReadKey();
                        break;

                    case "Exit":
                        endApp = true;
                        break;
                }
            }

            calculator.Finish();
        }

        private static void PerformCalculation(Calculator calculator, ref int usageCount)
        {
            double num1 = GetValidNumber("Type the first number: ");
            double num2 = GetValidNumber("Type the second number: ");

            string operation = GetOperation();
            double result = calculator.DoOperation(num1, num2, operation);
            if (double.IsNaN(result))
            {
                AnsiConsole.MarkupLine("[red]This operation resulted in a mathematical error.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]Result: {result:0.##}[/]\nTotal results calculated: {++usageCount}");
            }

            Console.WriteLine("\nPress Any Key to Continue.");
            Console.ReadKey();
        }

        private static void UsePreviousResult(Calculator calculator, ref int usageCount)
        {
            if (Memory.calculations.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No previous calculations available.[/]");
                Console.ReadKey();
                return;
            }

            var selectedCalculation = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a previous result to use as an operand:")
                    .PageSize(10)
                    .AddChoices(Memory.calculations));

            double selectedResult = ExtractResultFromCalculation(selectedCalculation);

            double num2 = GetValidNumber("Type the second number: ");
            string operation = GetOperation();

            double result = calculator.DoOperation(selectedResult, num2, operation);
            if (double.IsNaN(result))
            {
                AnsiConsole.MarkupLine("[red]This operation resulted in a mathematical error.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]Result: {result:0.##}[/]\nTotal results calculated: {++usageCount}");
            }

            Console.WriteLine("\nPress Any Key to Continue.");
            Console.ReadKey();
        }

        private static string GetOperation()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an operator:")
                    .AddChoices("a - Add", "s - Subtract", "m - Multiply", "d - Divide"))
                .Substring(0, 1);
        }

        private static double GetValidNumber(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<double>(prompt)
                    .PromptStyle("green")
                    .Validate(value => value >= double.MinValue && value <= double.MaxValue ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid number![/]")));
        }

        private static double ExtractResultFromCalculation(string calculation)
        {
            string[] parts = calculation.Split('=');
            if (parts.Length > 1 && double.TryParse(parts[1].Trim(), out double result))
            {
                return result;
            }
            return double.NaN;
        }
    }
}
