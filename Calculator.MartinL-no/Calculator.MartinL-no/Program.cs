using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static readonly Calculator Calc = new Calculator();

    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            string op = GetOperator();

            switch (op)
            {
                case "x":
                    Calc.ClearOperations();
                    Console.WriteLine("Previous operations deleted");
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                case "r":
                case "si":
                case "co":
                case "ta":
                    SingleInputOperation(op);
                    break;
                default:
                    TwoInputOperation(op);
                    break;
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.Clear();
        }

        return;
    }

    private static double SingleInputOperation(string op)
    {
        ShowPreviousOperations();
        var input = GetInputOne();
        var result = Calc.DoOperation(input, op);
        ShowResult(result);

        return result;
    }


    private static void TwoInputOperation(string op)
    {
        ShowPreviousOperations();
        var input1 = GetInputOne();
        var input2 = GetInputTwo();

        try
        {
            var result = Calc.DoOperation(input1, input2, op);
            if (double.IsNaN(result))
            {
                Console.WriteLine("This operation will result in a mathematical error.\n");
            }
            else ShowResult(result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
        }
    }

    private static string GetOperator()
    {
        // Ask the user to choose an operator.
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tr - Square root");
        Console.WriteLine("\te - Exponent");
        Console.WriteLine("\tsi - Sine");
        Console.WriteLine("\tco - Cosine");
        Console.WriteLine("\tta - Tangent");
        Console.WriteLine("\tx - Delete previous operations");
        Console.Write("Your option? ");

        return Console.ReadLine();
    }

    private static double GetInputOne()
    {
        Console.Write($"Type a number{(Calc.IsPreviousOperations() ? " or input from above" : "")}, and then press Enter: ");
        return GetInput();
    }

    private static double GetInputTwo()
    {
        Console.Write($"Type a number{(Calc.IsPreviousOperations() ? " or input from above" : "")}, and then press Enter: ");
        return GetInput();
    }

    private static double GetInput()
    {
        var input = Console.ReadLine();

        double cleanInput = 0;
        while (!Calc.GetValidInput(input, out cleanInput))
        {
            Console.Write($"This is not valid input. Please enter an integer value{(Calc.IsPreviousOperations() ? " or input from above" : "")}: ");
            input = Console.ReadLine();
        }

        return cleanInput;
    }

    private static void ShowPreviousOperations()
    {
        var operations = Calc.GetPreviousOperations();

        if (operations.Count > 0)
        {
            Console.WriteLine("Results of previous operation(s) that can be used as input (Enter letter): ");

            foreach (var operation in operations)
            {
                Console.WriteLine(operation);
            }

            Console.WriteLine();
        }
    }

    private static void ShowResult(double result)
    {
        Console.WriteLine("Your result: {0:0.##}\n", result);
    }
}