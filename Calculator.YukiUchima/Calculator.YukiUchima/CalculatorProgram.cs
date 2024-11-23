using CalculatorLibrary.Models;
using CalculatorLibrary;
using Spectre.Console;

namespace Calculator.YukiUchima;

internal class CalculatorProgram
{
    private static char _operation;
    private static double _result;
    
    static void Main(string[] args)
    {
        bool endApp = false;

        // Display title
        Console.WriteLine("Console Calculator in C#");
        Console.WriteLine("------------------------\n");

        // Create ONE instance of a calculator object
        CalculatorLibrary.Calculator calculator = new CalculatorLibrary.Calculator();

        while (!endApp)
        {
            // Ask user to pick an option
            var selectedOp = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose an option from the following list: ")
                .AddChoices(Enums.OpDescription.Values.ToList())
                );

            var selectedOpType = Enums.OpDescription.First(opDesc => opDesc.Value == selectedOp).Key;

            if (selectedOpType == Enums.OperationType.ShowCalculations)
            {
                calculator.PreviewCalculations();
            }
            else if (selectedOpType == Enums.OperationType.DeleteCalculations)
            {
                CalculatorLibrary.Calculator._CalculationsList.Clear();
                Console.Clear();
                Console.WriteLine("Memory Deleted - Removed Previous Calculations");
            }
            else
            {
                try
                {
                    double numInput1;
                    double numInput2;

                    //Get user's first number
                    numInput1 = Calculation.GetCalculateNumber(1);
                    
                    if (selectedOpType.Equals(Enums.OperationType.SquareRoot) || selectedOpType.Equals(Enums.OperationType.TenX)) {
                        _result = calculator.DoOperation(numInput1, selectedOpType);
                        Calculation newCalculation = new Calculation(numInput1, selectedOpType, _result);
                        CalculatorLibrary.Calculator._CalculationsList.Add(newCalculation);
                    }
                    else
                    {
                        //Get user's second number
                        numInput2 = Calculation.GetCalculateNumber(2);
                        _result = calculator.DoOperation(numInput1, selectedOpType, numInput2);
                        Calculation newCalculation = new Calculation(numInput1, selectedOpType, _result, numInput2);
                        CalculatorLibrary.Calculator._CalculationsList.Add(newCalculation);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            
            Console.WriteLine("------------------------\n");
            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }
}