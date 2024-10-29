using Spectre.Console;
using CalculatorLibrary;
using EnumsLibrary;
namespace UserInterfaceLibrary;


public class UserInterface
{
    public void ShowMenu()
    {


        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        Calculator calculator = new Calculator();

        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            double calculationResult = 0;

            var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MenuAction>()
                .Title("Choose an operation from the following list:")
                .AddChoices(Enum.GetValues<Enums.MenuAction>()));
            switch (actionChoice)
            {
                case Enums.MenuAction.Calculator:
                    // Ask the user to type the first number.
                    var cleanNum1 = AnsiConsole.Ask<double>("[green]Type a number, and then press Enter: [/]");
                    var cleanNum2 = AnsiConsole.Ask<double>("[green]Type another number, and then press Enter: [/]");

                    // Ask the user to choose an operator.
                    var operationTypeChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<Enums.Operation>()
                        .Title("[red]Choose an operator from the following list:[/]")
                        .AddChoices(Enum.GetValues<Enums.Operation>()));

                        try
                        {
                            calculationResult = calculator.DoOperation(cleanNum1, cleanNum2, operationTypeChoice);
                            if (double.IsNaN(calculationResult))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", calculationResult);
                                calculator.UsageCounter();
                            }
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    
                    
                    Console.WriteLine("------------------------\n");

                    // Wait for the user to respond before closing.
                    Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "n") endApp = true;

                    Console.WriteLine("\n"); // Friendly linespacing.

                    break;
                case Enums.MenuAction.UsePastResult:
                    double pastResult = calculator.RetrievePastResult();
                    Console.WriteLine();
                    var operand = AnsiConsole.Ask<int>("[green]Type another number, and then press Enter: [/]");

                    // Ask the user to choose an operator.
                    var operationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<Enums.Operation>()
                        .Title("[red]Choose an operator from the following list:[/]")
                        .AddChoices(Enum.GetValues<Enums.Operation>()));

                    try
                    {
                        calculationResult = calculator.DoOperation(pastResult, operand, operationChoice);
                        if (double.IsNaN(calculationResult))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", calculationResult);
                            calculator.UsageCounter();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                    
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;

                case Enums.MenuAction.ShowHistory:
                    calculator.ShowHistory();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                case Enums.MenuAction.DeleteHistory:
                    calculator.DeleteHistory();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;
                case Enums.MenuAction.ShowUsageCount:
                    calculator.ShowUsage();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    break;



            }
        }
        calculator.Finish();
        
        return;
    }



}    

