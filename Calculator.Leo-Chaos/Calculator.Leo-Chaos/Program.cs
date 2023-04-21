using static CalculatorLibrary.Calculator;
using static CalculatorLibrary.HistoryFunctions;
using static Calculator.LeoChaos.MainMenuFunctions;

namespace Calculator.LeoChaos
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            CalcUses = 0;

            bool endApp = false;

            while (!endApp)
            {
                string op = MenuOperationList();

                double cleanNum1;
                double? cleanNum2 = null;
                double result;

                if (HistoryResult != 0)
                {
                    cleanNum1 = HistoryResult;
                    Console.WriteLine($"First number: {HistoryResult}");
                }
                else
                {
                    cleanNum1 = GetNumber();
                }

                if (RequiresSecondNumber(op))
                {
                    cleanNum2 = GetNumber();
                }

                try
                {
                    result = DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        AddToHistory(cleanNum1, cleanNum2, TextToEnum(op), result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                HistoryResult = 0;
                CalcUses++;

                if (CalcUses != 0)
                {
                    Console.WriteLine($"Operation count so far = {CalcUses} \n");
                }

                Console.Write("Press 'n' and Enter to close the app 'h' and enter for history, or press Enter to continue: ");
                var endChoice = Console.ReadLine();
                if (endChoice == "n") endApp = true;
                if (endChoice == "h") HistoryMenu();

                Console.WriteLine("\n");
            }

            return;
        }
    }
}