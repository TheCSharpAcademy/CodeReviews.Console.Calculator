// -------------------------------------------------------------------------------------------------
// CalculatorLibrary.Constants.AllowedChars
// -------------------------------------------------------------------------------------------------
// Defines valid options for a user input.
// -------------------------------------------------------------------------------------------------

namespace CalculatorLibrary.Constants;

public static class AllowedChars
{
    public static readonly char[] MainMenuInput = { 'n', 'N', 'r', 'R', 'v', 'V', 'c', 'C', 'q', 'Q' };

    public static readonly char[] OneNumberCalculation = { 'r', 'R', 'p', 'P', 's', 'S', 'c', 'C', 't', 'T' };
    
    public static readonly char[] TwoNumberCalculation = { '+', '-', '*', '/', 'e', 'E' };
}
