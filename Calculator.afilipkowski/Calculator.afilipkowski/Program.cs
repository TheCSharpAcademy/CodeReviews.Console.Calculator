using System.Text.RegularExpressions;
using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        while (!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

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
                if (Console.ReadLine() == "y") calculator.ClearHistory();
            }
            Console.Write("Press 'n' and Enter to close the app or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;
            Console.Clear();
        }
        calculator.Finish();
        return;
    }
}