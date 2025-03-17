using System.Text.RegularExpressions;
using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new();
        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            double firstNumber = 0;
            double secondNumber = 0;
            double result = 0;

            bool useHistory = false;

            if (calculator.CalculationList.Count > 0)
            {
                Console.Write("Do you want to use a previous result? (y/n): ");
                string historyChoice = Console.ReadLine().ToLower();

                if (historyChoice == "y")
                {
                    useHistory = true;
                    firstNumber = ExtractResult(calculator);
                }
            }

            if (!useHistory)
            {
                Console.Write("Type a number, and then press Enter: ");
                firstNumber = TypedNumber();
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add (+)");
            Console.WriteLine("\ts - Subtract (-)");
            Console.WriteLine("\tm - Multiply (*)");
            Console.WriteLine("\td - Divide (/)");
            Console.WriteLine("\tp - Power (x^y)");
            Console.WriteLine("\tsqrt - Square Root (√)");
            Console.WriteLine("\te - Exponential (10^x)");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == "sqrt" || op == "e")
            {
                secondNumber = 0;
            }
            else
            {
                Console.Write("Type another number, and then press Enter: ");
                secondNumber = TypedNumber();
            }

            if (op == null || !Regex.IsMatch(op, "[a|s|m|d|p|sqrt|e]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(firstNumber, secondNumber, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            Console.WriteLine($"Total operations performed: {calculator.Counter}");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Your history is:");
            foreach (string calculation in calculator.CalculationList)
            {
                Console.WriteLine(calculation);
            }
            Console.Write("Press 'c' to clear history, 'n' to exit, or any other key to continue: ");
            string input = Console.ReadLine();
            if (input == "n") endApp = true;
            else if (input == "c") calculator.ClearHistory();
            else continue;
            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }

    static double ExtractResult(Calculator calculator)
    {
        Console.WriteLine("Choose a result from the history:");
        for (int i = 0; i < calculator.CalculationList.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {calculator.CalculationList[i]}");//aici de verificat nr result
        }

        Console.Write("Enter the number of the result you want to use: ");
        int choice = 0;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > calculator.CalculationList.Count)
        {
            Console.Write("Invalid choice. Please enter a valid number: ");
        }

        string selectedCalculation = calculator.CalculationList[choice - 1];
        string[] parts = selectedCalculation.Split("=");
        double result = double.Parse(parts[1].Trim());
        return result;
    }

    static double TypedNumber()
    {
        string numInput = Console.ReadLine();
        double number = 0;
        while (!double.TryParse(numInput, out number))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput = Console.ReadLine();
        }

        return number;
    }
}
