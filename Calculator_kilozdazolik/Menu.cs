using System.Text.RegularExpressions;

namespace Calculator_kilozdazolik;

public class Menu
{
    private Calculator _calculator = new();
    private Helpers _helper = new();

    public void ShowMenu()
    {
        bool endApp = false;
        
        while (!endApp) {
            Console.Clear();
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine($"The calculator was used: {_calculator.CalculatorUsed} times");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tsq - Square Root");
            Console.WriteLine("\tsi - Sin");
            Console.WriteLine("\tc - Cos");
            Console.WriteLine("\tt - Tan");
            Console.WriteLine("\tp - Power of Ten");
            Console.WriteLine("------------------------");
            Console.WriteLine("\tv - View History");
            Console.Write("Your option? ");
            string? op = Console.ReadLine();
            while ((string.IsNullOrEmpty(op)))
            {
                Console.WriteLine("Please enter a valid option");
                op = Console.ReadLine();
            }

            if (op == "v")
            {
                _helper.PrintHistory();
            }
            else if (Regex.IsMatch(op, "^(sq|si|c|t|p)$"))
            {
                _calculator.HandleUnaryOperation(op);
            }
            else
            {
                _calculator.HandleBinaryOperation(op);
            }
            
            Console.WriteLine("------------------------\n");
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n");
        }
    }
}