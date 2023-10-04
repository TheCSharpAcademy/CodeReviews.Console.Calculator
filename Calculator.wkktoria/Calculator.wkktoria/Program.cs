using CalculatorLibrary;

namespace Calculator.wkktoria;

internal static class Program
{
    private static void Main(string[] args)
    {
        var endApp = false;

        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------");

        var calculator = new CalculatorLibrary.Calculator();

        while (!endApp)
        {
            var onlyOneNumber = false;

            var num1 = double.NaN;
            var num2 = double.NaN;
            var result = double.NaN;

            Console.WriteLine("\nChoose an option from the following list:");
            calculator.PrintAvailableOperations();

            Console.Write("> ");
            var op = Console.ReadLine();

            while (!calculator.IsValidOperation(op))
            {
                Console.Write("Invalid operation. Choose one from the list above: ");
                op = Console.ReadLine();
            }

            op = Helpers.ParseOperation(op);

            Console.WriteLine();

            switch (op)
            {
                case "times":
                    Console.WriteLine($"Times used: {CalculatorLibrary.Calculator.GetTimesUsed()}");
                    break;
                case "print":
                    CalculatorLibrary.Calculator.PrintLatestCalculations();
                    break;
                case "delete":
                    CalculatorLibrary.Calculator.DeleteLatestCalculations();
                    break;
                default:
                {
                    if (op is "sqr" or "sin" or "cos" or "10x")
                    {
                        onlyOneNumber = true;
                    }

                    if (onlyOneNumber)
                    {
                        num1 = Helpers.GetNumber();
                    }
                    else
                    {
                        num1 = Helpers.GetNumber();
                        num2 = Helpers.GetNumber();
                    }

                    Console.WriteLine($"\nData: first number: {num1}{(double.IsNaN(num2) ? "" : $"; second number: {num2}")}; operation: {op} ");

                    try
                    {
                        result = calculator.DoOperation(num1, num2, op);
            
                        if (double.IsNaN(result))
                            Console.WriteLine("This operation will result in a mathematical error.");
                        else
                            Console.WriteLine("Result: {0:0.##}", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An exception occurred trying to do the math.");
                    }

                    break;
                }
            }
            
            Console.WriteLine("\nPress any key to continue, or 'q' to quit...");
            if (Console.ReadLine() == "q") endApp = true;
        }

        calculator.Finish();
    }
}