using System.Text.RegularExpressions;

using CalculatorLibrary;

namespace CalculatorProgram;

partial class Program
{
    static void Main()
    {
        bool endApp = false;
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new();
        while (!endApp)
        {
            Console.Write("Type a number, and then press Enter: ");
            string? numInput1 = Console.ReadLine();

            double cleanNum1;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput1 = Console.ReadLine();
            }

            Console.Write("Type another number, and then press Enter: ");
            string? numInput2 = Console.ReadLine();

            double cleanNum2;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tc - Cosine");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tt - Tangent");
            Console.WriteLine("\tsi - Sine");
            Console.WriteLine("\tp - Power");
            Console.WriteLine("\tx - 10 to the Power");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            if (op == null || !OperatorRegex().IsMatch(op))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    double result = calculator.DoOperation(cleanNum1, cleanNum2, op);
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

            // Display stored operations
            var operations = Calculator.GetOperations();
            if (operations.Count > 0)
            {
                Console.WriteLine("Stored Operations:");
                for (int i = 0; i < operations.Count; i++)
                {
                    Console.WriteLine($"{i}: {operations[i].Operand1} {operations[i].Operation} {operations[i].Operand2} = {operations[i].Result}");
                }
                Console.WriteLine();
            }

            // Additional options
            Console.WriteLine("Additional options:");
            Console.WriteLine("\t1 - Use stored operation");
            Console.WriteLine("\t2 - Repeat last operation");
            Console.WriteLine("\t3 - Apply operation to all stored results");
            Console.WriteLine("\t4 - Chain operations");
            Console.WriteLine("\t5 - Clear stored operations");
            Console.WriteLine("\t6 - Exit");
            Console.Write("Your choice? ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    UseStoredOperation(calculator);
                    break;
                case "2":
                    RepeatLastOperation(calculator);
                    break;
                case "3":
                    ApplyOperationToAll(calculator);
                    break;
                case "4":
                    ChainOperations(calculator);
                    break;
                case "5":
                    Calculator.ClearOperations();
                    Console.WriteLine("Stored operations cleared.");
                    break;
                case "6":
                    endApp = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Continuing with normal operation.");
                    break;
            }

            if (!endApp)
            {
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
            }

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }

    private static void UseStoredOperation(Calculator calculator)
    {
        Console.Write("Enter the index of the operation to use: ");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            Console.Write("Enter the new operand: ");
            if (double.TryParse(Console.ReadLine(), out double newOperand))
            {
                try
                {
                    double result = calculator.UseStoredOperation(index, newOperand);
                    Console.WriteLine($"Result: {result}");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid operation index.");
                }
            }
            else
            {
                Console.WriteLine("Invalid operand.");
            }
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }
    }

    private static void RepeatLastOperation(Calculator calculator)
    {
        Console.Write("Enter the new operand: ");
        if (double.TryParse(Console.ReadLine(), out double newOperand))
        {
            try
            {
                double result = calculator.RepeatLastOperation(newOperand);
                Console.WriteLine($"Result: {result}");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid operand.");
        }
    }

    private static void ApplyOperationToAll(Calculator calculator)
    {
        Console.Write("Enter the operand to apply to all stored operations: ");
        if (double.TryParse(Console.ReadLine(), out double operand))
        {
            var results = calculator.ApplyOperationToAll(operand);
            Console.WriteLine("Results:");
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine($"{i}: {results[i]}");
            }
        }
        else
        {
            Console.WriteLine("Invalid operand.");
        }
    }

    private static void ChainOperations(Calculator calculator)
    {
        List<int> indices = new List<int>();
        Console.WriteLine("Enter the indices of operations to chain (enter a non-number to finish):");
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                indices.Add(index);
            }
            else
            {
                break;
            }
        }

        Console.Write("Enter the initial value: ");
        if (double.TryParse(Console.ReadLine(), out double initialValue))
        {
            try
            {
                double result = calculator.ChainOperations(indices, initialValue);
                Console.WriteLine($"Result of chained operations: {result}");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid operation index in the chain.");
            }
        }
        else
        {
            Console.WriteLine("Invalid initial value.");
        }
    }

    [GeneratedRegex("[a|s|m|d|c|r|t|si|p|x]")]
    private static partial Regex OperatorRegex();
}