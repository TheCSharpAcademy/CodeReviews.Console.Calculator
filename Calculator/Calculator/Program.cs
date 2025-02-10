using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("\tConsole Calculator in C#");
        Console.WriteLine("----------------------------------------");

        Calculator calculator = new Calculator();
        while (!endApp)
        while (!endApp)
        {
            Console.WriteLine($"This calculator was used {calculator.Counter} times");
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.WriteLine(
                "For the square root and trigonometry operations, only the first number will be used. Press any key to continue:"
            );
            Console.ReadKey();

            var getFirstNumMessage =
                calculator.Results.Count() == 1
                    ? "\nType a number, and then press Enter: "
                    : "\nIf you'd like to use a previous result, type 'p', \notherwise type a number, and then press Enter: ";

            var validationErrorMessage =
                calculator.Results.Count() == 1
                    ? "\nThis is not valid input. Please enter an integer value: "
                    : "\nThis is not valid input. Please enter an integer value or 'p': ";

            Console.Write(getFirstNumMessage);

            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;

            while (numInput1.ToLower() != "p" && !double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write(validationErrorMessage);
                numInput1 = Console.ReadLine();
            }

            if (numInput1.ToLower() == "p")
            {
                cleanNum1 = GetPreviousResult(calculator.Results);
            }

            // Ask the user to type the second number.
            var getSecondNumMessage =
                calculator.Results.Count() == 0
                    ? "\nType another number, and then press Enter: "
                    : "\nIf you'd like to use a previous result, type 'p', \notherwise type a number, and then press Enter: ";
            Console.Write(getSecondNumMessage);

            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;

            while (numInput2.ToLower() != "p" && !double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write(validationErrorMessage);
                numInput2 = Console.ReadLine();
            }

            if (numInput2.ToLower() == "p")
            {
                cleanNum2 = GetPreviousResult(calculator.Results);
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tpow - Power");
            Console.WriteLine("\tsqrt - Square Root");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\ttan - Tangent");
            Console.Write("\nYour option? ");
            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("\nError: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine(
                            "\nThis operation will result in a mathematical error.\n"
                        );
                    }
                    else
                        Console.WriteLine("\nYour result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Oh no! An exception occurred trying to do the math.\n - Details: "
                            + e.Message
                    );
                }
            }
            Console.WriteLine("----------------------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write(
                "Press 'n' and Enter to close the app, or press any other key and Enter to continue: "
            );
            if (Console.ReadLine() == "n")
                endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        calculator.Finish();
        return;
    }

    private static double GetPreviousResult(List<double> previousResults)
    {
        Console.WriteLine("Type the index of the previous result:");

        for (int index = 1; index < previousResults.Count; index++)
        {
            double result = previousResults[index - 1];
            Console.WriteLine($"{index}: {result}");
        }

        var userChoice = Console.ReadLine();

        return previousResults[int.Parse(userChoice) - 1];
    }
}
