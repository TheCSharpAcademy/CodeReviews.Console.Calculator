using System.Text.RegularExpressions;

namespace Calculator.Wolfieeex;

class HelperMethods
{
    static internal void ReadNumericInput(ref double number, string ordinalString, bool divisor = false)
    {
        bool checkNumber = true;
        while (checkNumber)
        {
            string? userInput = Console.ReadLine();
            ordinalString = ordinalString == "" ? ordinalString : ordinalString.Trim() + " ";

            if (string.IsNullOrEmpty(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour input was empty. Please try again- insert your {ordinalString}number and press ENTER: ");
                continue;
            }
            if (!double.TryParse(userInput, out number))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour input must not include letters or symbols. Please try again- insert your {ordinalString}number and press ENTER: ");
                continue;
            }
            if (divisor && number == 0)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour divisor must not equal 0. Please try again- reinsert your {ordinalString}number and press ENTER: ");
                continue;
            }
            checkNumber = false;
        }
    }
    static internal void ReadMatchingInput(ref string? letter, string RegexOptionsPattern)
    {
        bool checkNumber = true;
        while (checkNumber)
        {
            string? userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour input was empty. Please try to select your option again and press ENTER: ");
                continue;
            }
            if (!Regex.IsMatch(userInput.ToLower(), RegexOptionsPattern))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                Console.Write($"\rYour selection doesn't match any of the options above. Please try to select your option again and press ENTER: ");
                continue;
            }
            checkNumber = false;
            letter = userInput;
        }
    }
    static internal void AskForNumber(string ordinalString)
    {
        Console.Write($"Please type in your {ordinalString} number, and then press ENTER: ");
    }

}
