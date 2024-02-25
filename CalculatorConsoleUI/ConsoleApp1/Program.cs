using CalculatorLib;
using ConsoleUI;

ConsoleMassages.AppWelcomeMessage();
ExecuteCalculatorProgram();


static void ExecuteCalculatorProgram()
{
    Calculator.InitializeCalculator();

    string choice;
    double? resultFromHistory = null;

    do
    {

        double firstOperand;
        double secondOperand;
        ConsoleMassages.DisplayMainMenu();
        choice = ConsoleHelper.GetStringFromUser("Please enter the letter corresponds to the desigred Choice");
        switch (choice)
        {
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
            case "^":
                firstOperand = ConsoleHelper.GetNumberOrUsePickedHistoryResult("Enter First Operand", resultFromHistory);
                secondOperand = ConsoleHelper.GetNumberOrUsePickedHistoryResult("Enter Second Operand", resultFromHistory);
                double result = Calculator.DoOperation(firstOperand, choice, secondOperand);
                Console.WriteLine($"result : {result}");
                break;
            case "#":
            case "!":
            case "~":
            case "&":
            case "$":
                double operand = ConsoleHelper.GetNumberOrUsePickedHistoryResult("Enter The Operand", resultFromHistory);
                result = Calculator.DoOperation(operand, choice);
                Console.WriteLine($"result : {result}");
                break;
            case "h":
                if(Calculator.GetCalculatorHistoryCount() > 0)
                {
                    var x = Calculator.GetCalculatorHistory();
                    ConsoleMassages.DisplayList(x);
                }
                else
                {
                    Console.WriteLine("The History is now Empty!");
                }
                
                break;
            case "c":
                Calculator.ClearCalculatorHistory();
                Console.WriteLine("Hitory Cleared");
                break;
            case "r":
                int range = Calculator.GetCalculatorHistoryCount();
                if(range > 0)
                {
                    do
                    {
                        var historyList = Calculator.GetCalculatorHistory();
                        ConsoleMassages.DisplayList(historyList);
                        result = (int)ConsoleHelper.GetNumberFromUser("Please Enter The Number that Correspond the Operation History") - 1;
                        if (result >= range || result < 0)
                        {
                            Console.WriteLine("Invalid Index");
                        }

                    } while ((int)result >= range || (int)result < 0);
                    resultFromHistory = Calculator.GetCalculatorHistory((int)result)[0].result;
                }
                else
                {
                    Console.WriteLine("No History Yet");
                }

                break;

            default:
                Console.WriteLine("Please Enter a valid Choice");
                Thread.Sleep(1000);
                Console.Clear();
                continue;
        }
        Console.Write("Press Any Key to Continue!, To exit press ('exit'): ");
        choice = Console.ReadLine().Trim().ToLower();
        Console.Clear();

    } while (choice != "exit");

}







