namespace CalculatorProgram;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display the title as the C# console calculator app.
        System.Console.WriteLine("Console Calculator in C#\r");
        System.Console.WriteLine("------------------------\n");
        
        CalculatorPlus calculator = new();

        while (!endApp)
        {
            var menuSelection = UserInterface.MainMenuPrompt(calculator.History);

            switch (menuSelection)
            {
                case "Clear Calculation History":
                    calculator.ClearHistory();
                    break;
                case "Quit":
                    endApp = true;
                    Environment.Exit(0);
                    break;
                // Add, Subtract, Multiply, Divide at this point
                default:
                    // Ask for operands

                    var operand1 = UserInterface.PromptForNumberOrCalculationResult(calculator.History);
                    var operand2 = UserInterface.PromptForNumberOrCalculationResult(calculator.History);

                    // convert to operation string
                    var operationLetter = menuSelection.ToLower().Substring(0, 1);
                    try
                    {
                        var result = calculator.DoOperation(operand1, operand2, operationLetter);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathermatical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                        }

                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occured trying to do the math.\n - Details: " +
                                          e.Message);
                    }

                    break;
            }
        }
    }
}