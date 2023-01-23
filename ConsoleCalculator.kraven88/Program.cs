using CalculatorLibrary;

namespace CalculatorProgram;

internal class Program
{
    private static void Main(string[] args)
    {
        var endApp = false;
        var counter = 0;  // Number of times the Calculator has been used
        var calculator = new Calculator();

        // Display title as the C# console calculator app;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (endApp == false)
        {

            // Declare variables and initialize with zero;
            var numText1 = "";
            var numText2 = "";
            var result = 0.0;

            // Ask the user to type the first number;
            Console.Write("Type a number, then press Enter: ");
            numText1 = Console.ReadLine()!.Trim();

            var num1 = 0.0;
            while (double.TryParse(numText1, out num1) == false)
            {
                Console.Write("Invalid number. Please enter a numeric value: ");
                numText1 = Console.ReadLine()!.Trim();
            }

            // Ask the user to type the second number;
            Console.Write("Type another number, then press Enter: ");
            numText2 = Console.ReadLine()!.Trim();

            var num2 = 0.0;
            while (double.TryParse(numText2, out num2) == false)
            {
                Console.Write("Invalid number. Please enter a numeric value: ");
                numText2 = Console.ReadLine()!.Trim();
            }

            // Ask the user to choose an option;
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your choice: ");

            var operation = Console.ReadLine()!.Trim();

            try
            {
                result = calculator.DoOperation(num1, num2, operation);

                if (double.IsNaN(result))
                    Console.WriteLine("This operation will result in mathematical error.\n");
                else
                {
                    counter++;
                    Console.WriteLine($"Your result: {result:0.##}. You used the Calculator {CountFormatter(counter)}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oh no! An exception occured while doing the math. \nDetails: {ex.Message}");
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing the app;
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
                endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        return;
    }

    private static string CountFormatter(int count)
    {
        var output = $"{count} time";
        if (count > 1) output += "s";
        return output;
    }
}