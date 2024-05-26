using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

internal class Menu
{
    List<Operations> OperationsList;

    public Menu()
    {
        OperationsList = new List<Operations>();
    }

    internal void ShowMenu()
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            string optionSelected = SelectOption();
            if (optionSelected == "o")
            {
                ShowPreviousResults();
            }
            else if (optionSelected == "e")
            {
                ClearPreviousResults();
            }
            else
            {

                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;

                // Ask the user to type the first number.
                cleanNum1 = Helpers.GetNumber("Type a number, and then press Enter: ");
                if (Regex.IsMatch(optionSelected, "[a|s|m|d|p]"))
                {
                    cleanNum2 = Helpers.GetNumber("Type an other number, and then press Enter: ");
                }
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, optionSelected);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        OperationsList.Add(new Operations
                        {
                            FirstOperand = cleanNum1,
                            SecondOperand = cleanNum2,
                            Operation = optionSelected,
                            Result = result
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }

            Console.WriteLine("------------------------------------------------------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
    }

    private void ClearPreviousResults()
    {
        Console.Clear();
        OperationsList.Clear();
        Console.WriteLine("The previous results are been deleted");
    }

    private void ShowPreviousResults()
    {
        Console.Clear();
        Console.WriteLine("Previous results.");
        Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15}", "Operand 1", "Operand 2", "Operation", "Result");
        foreach(var result in OperationsList)
        {
            Console.WriteLine("{0,-15} {1,-15} {2,-10} {3,-15}", result.FirstOperand, result.SecondOperand, result.Operation, result.Result);
        }
    }

    private string SelectOption()
    {
        // Ask the user to choose an operator.
        string? op;
        do
        {
            Console.Clear();
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine($"\to - Show Previous Results");
            Console.WriteLine($"\te - Delete Previous Results");
            Console.WriteLine($"\ta - Add");
            Console.WriteLine($"\ts - Subtract");
            Console.WriteLine($"\tm - Multiply");
            Console.WriteLine($"\td - Divide");
            Console.WriteLine($"\tp - Taking the Power");
            Console.WriteLine($"\tr - Square Root");
            Console.WriteLine($"\tx - 10x");
            Console.WriteLine($"\tt - Sine");
            Console.Write("Your option? ");
            op = Console.ReadLine();
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|r|p|x|t|o|e]"))
            {
                Console.WriteLine("Error: Unrecognized input.\n Press any key to show the menu.");
                Console.ReadLine();
            }
            else
                return op;
        } while (true);
    }



}