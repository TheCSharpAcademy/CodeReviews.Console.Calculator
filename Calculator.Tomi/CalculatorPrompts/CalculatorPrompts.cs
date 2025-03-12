using System.Text.RegularExpressions;

namespace Calculator.Tomi.CalculatorPrompts;

public static class Prompts
{

    public static string PromptForOperation()
    {
        PrintOperations();
        string input;
        while (true)
        {
            input = (Console.ReadLine()?.Trim().ToLower()) ?? string.Empty;

            if (!string.IsNullOrEmpty(input) && (input == "a" || input == "s" || input == "m" || input == "d" || input == "sq" || input == "sqr" || input == "pow"))

            {
                return input;
            }

            Console.WriteLine("Error: Unrecognized operation.. please choose a valid operation");
            PrintOperations();

        }

    }

    public static void PrintOperations()
    {
        Console.WriteLine("Choose an operator from the following list:");
        Console.WriteLine("\ta - Add");
        Console.WriteLine("\ts - Subtract");
        Console.WriteLine("\tm - Multiply");
        Console.WriteLine("\td - Divide");
        Console.WriteLine("\tsq - Square");
        Console.WriteLine("\tsqr - Square root");
        Console.WriteLine("\tpow - Raise-To-Power");
        Console.Write("Your option? ");
    }

    public static bool AskToReUseResult(bool canReuseResult)
    {
        if (!canReuseResult) return false;

        Console.WriteLine("\nwanna re-use previouse result for next calculation ?? enter 'y' for yes or 'n' for no");
        string reUseResultPrompt;

        while (true)
        {
            reUseResultPrompt = (Console.ReadLine()?.Trim().ToLower()) ?? string.Empty;

            if (string.IsNullOrEmpty(reUseResultPrompt))
            {
                Console.WriteLine("response shouldn't be empty.. enter 'y' for yes or 'n' for no");
                continue;
            }

            if (Regex.IsMatch(reUseResultPrompt, "[y]"))
            {
                return true;
            }
            else if (Regex.IsMatch(reUseResultPrompt, "[n]"))
            {
                return false;
            }
            Console.WriteLine("invalid response.. enter 'y' for yes or 'n' for no");
        }
    }

    public static List<double> PromptForOperands(int numberOfOperand)
    {
        List<double> operands = [];
        for (int i = 0; i < numberOfOperand; i++)
        {

            Console.Write($"Type {(i == 0 ? "a" : "another")} number, and then press Enter: ");
            string? input = Console.ReadLine();
            double cleanedOperand;

            while (!double.TryParse(input, out cleanedOperand))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }

            operands.Add(cleanedOperand);
        }
        return operands;
    }

}
