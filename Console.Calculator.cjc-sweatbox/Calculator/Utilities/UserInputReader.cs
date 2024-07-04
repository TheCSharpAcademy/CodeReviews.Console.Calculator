// -------------------------------------------------------------------------------------------------
// CalculatorProgram.Utilities.UserInputReader
// -------------------------------------------------------------------------------------------------
// Helper class to present a question and return a valid user response from the console.
// -------------------------------------------------------------------------------------------------

namespace CalculatorProgram.Utilities;

internal class UserInputReader
{
    #region Methods: Internal Static

    internal static char GetChar(string message, char[] allowedChars)
    {
        string? input = "";
        char inputChar;
        char output;
        bool validated = false;

        Console.Write(message);
        input = Console.ReadLine();


        while (!validated)
        {
            // Validation 1: We must have something.
            if (!string.IsNullOrWhiteSpace(input))
            {
                // We now know we have at least 1 char in the input, so convert input string to char.
                inputChar = input.First();
                                
                // Validation 2: Input must be an allowed char.
                if (allowedChars.Contains(inputChar))
                {
                    validated = true;
                }
            }
            
            // Get input again if invalid.
            if (!validated)
            {
                Console.Write($"This is not valid input. {message}");
                input = Console.ReadLine();
            }
        }

        output = input!.First();
        return output;
    }

    internal static double GetDouble(string message)
    {
        string? input = "";
        double output = double.NaN;
        
        Console.Write(message);
        input = Console.ReadLine();
        
        // Note: this will validate the input and assign valid values to the output variable.
        while (string.IsNullOrWhiteSpace(input) || !double.TryParse(input, out output))
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }
                
        return output;
    }

    internal static int GetInt(string message)
    {
        string? input = "";
        int output = 0;

        Console.Write(message);
        input = Console.ReadLine();

        // Note: this will validate the input and assign valid values to the output variable.
        while (string.IsNullOrEmpty(input) || !int.TryParse(input, out output))
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }

        return output;
    }

    internal static int GetInt(string message, int min, int max)
    {
        string? input = "";
        int output = 0;

        Console.Write(message);
        input = Console.ReadLine();

        // Note: this will validate the input and assign valid values to the output variable.
        while (string.IsNullOrEmpty(input) || !int.TryParse(input, out output) || int.Parse(input) < min || int.Parse(input) > max)
        {
            Console.Write($"This is not valid input. {message}");
            input = Console.ReadLine();
        }

        return output;
    }

    #endregion
}
