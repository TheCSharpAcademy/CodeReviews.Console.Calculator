using System.Text.RegularExpressions;
using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        bool firstInput = true; //control for using previous result as an input
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            double cleanNum1 = 0;
            double cleanNum2 = 0;
            double result = 0;
            bool historyCleared = false; //so as it doesn't ask if you want to use previous result as an input after clearing history
                                         //it needs to be reset after each iteration, otherwise it would be always true after clearing history once

            cleanNum1 = calculator.GetInput(firstInput); //turned getting input into a method in order to prevent repetition
            cleanNum2 = calculator.GetInput(firstInput);

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Unrecognized input");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                        Console.WriteLine($"The calculator was used {calculator.GetCount()} time(s).");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occured.\n Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Do you want to see the history of calculations? (y/n)");
            var history = Console.ReadLine();
            if (history.ToLower() == "y")
            {
                calculator.DisplayHistory();
                Console.WriteLine("Do you want to clear the history? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    calculator.ClearHistory();
                    historyCleared = true;
                }
            }
            if (historyCleared) firstInput = true;
            else firstInput = false;
            Console.Write("Press 'n' and Enter to close the app or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
            Console.Clear();
        }
        calculator.Finish();
        return;
    }
}