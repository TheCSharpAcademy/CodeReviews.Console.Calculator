using System.Text.RegularExpressions;
using Calculator;
using Calculator.Models;

namespace Calculator;

class Calculator
{
    public static double DoOperation(double num1, double num2, string op, out string? trigFunction)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        trigFunction = null;

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                break;
            case "s":
                result = num1 - num2;
                break;
            case "m":
                result = num1 * num2;
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                break;
            case "p":
                result = Math.Pow(num1, num2);
                break;
            case "q":
                result = Math.Sqrt(num1);
                break;
            case "x":
                result = Math.Pow(10, num1);
                break;
            case "t":
                Console.WriteLine("Choose a trigonometry function from the following list:");
                Console.WriteLine("\ts - Sine");
                Console.WriteLine("\tc - Cosine");
                Console.WriteLine("\tt - Tangent");
                Console.Write("Your option? ");
                string? trig_op = Console.ReadLine();

                double trig_result = 0;
                switch (trig_op)
                {
                    case "s":
                        trigFunction = "sin";
                        trig_result = Math.Sin(num1);
                        break;
                    case "c":
                        trigFunction = "cos";
                        trig_result = Math.Cos(num1);
                        break;
                    case "t":
                        trigFunction = "tan";
                        trig_result = Math.Tan(num1);
                        break;
                    default:
                        break;
                }
                return trig_result;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        int timesUsed = 0;
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine($"Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tq - Square Root");
            Console.WriteLine("\tx - 10x");
            Console.WriteLine("\tt - Trigonometry functions");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();
            string? numInput1 = "";
            string? numInput2 = "";
            string? trigFunction = null;
            double cleanNum1 = 0;
            double cleanNum2 = 0;
            double result = 0;

            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine

            if(op == "a" || op == "s" || op == "m" || op == "d" || op == "p")
            {

                // Ask the user to type the first number.
                Console.Write($"Calculator has been used {timesUsed} time/s\n");
                Console.Write("Type a number or enter 'h' to use a result from a previous operation: ");
                numInput1 = Console.ReadLine();
                cleanNum1 = GetOperationNumber(numInput1);

                // Ask the user to type the second number.
                Console.Write("Type another number or enter 'h' to use a result from a previous operation: ");
                numInput2 = Console.ReadLine();
                cleanNum2 = GetOperationNumber(numInput2);

                try
                {
                    result = Calculator.DoOperation(cleanNum1, cleanNum2, op, out trigFunction);
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
            else
            {
                // Ask the user to type the first number.
                Console.Write($"Calculator has been used {timesUsed} time/s\n");
                Console.Write("Type a number or enter 'h' to use a result from a previous operation: ");
                numInput1 = Console.ReadLine();
                cleanNum1 = GetOperationNumber(numInput1);

                try
                {
                    result = Calculator.DoOperation(cleanNum1, double.NaN, op, out trigFunction);
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

            timesUsed++;
            Operation new_operation = new(cleanNum1, cleanNum2, op, result, trigFunction);
            StoredOperations.PastOperations.Add(new_operation);

            // Wait for the user to respond before closing.
            Console.WriteLine("n) Close app.\nd) Clean operation history\nEnter) Continue: ");
            var menu_option = Console.ReadLine();
            if (menu_option == "n")
            {
                endApp = true;
            }
            else if(menu_option == "d")
            {
                StoredOperations.PastOperations.Clear();
                Console.WriteLine("Operation history has been cleared. Press any key to continue");
                Console.ReadKey();
            }

            Console.WriteLine("\n"); // Friendly linespacing.
        }
        return;
    }
    private static double GetOperationNumber(string? numInput)
    {
        if (numInput.ToLower() == "h")
        {
            if (StoredOperations.PastOperations.Count == 0)
            {
                Console.WriteLine("No previous operations to use. Enter a numeric value: ");
                numInput = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Choose an operation from the following list:");
                for (int i = 1; i < StoredOperations.PastOperations.Count; i++)
                {
                    StoredOperations.PastOperations[i].DisplayOperation(i);
                }
                Console.Write("Your option? ");
                string? option = Console.ReadLine();
                int index = 0;
                while (!int.TryParse(option, out index) || index < 0 || index >= StoredOperations.PastOperations.Count)
                {
                    Console.Write("This is not valid input. Please enter an operation from the list: ");
                    option = Console.ReadLine();
                }
                numInput = StoredOperations.PastOperations[index].Result.ToString();
            }
        }

        double cleanNum = 0;
        while (!double.TryParse(numInput, out cleanNum))
        {
            Console.Write("This is not valid input. Please enter a numeric value: ");
            numInput = Console.ReadLine();
        }

        return cleanNum;
    }
}