using System.Text.RegularExpressions;

namespace Calculator.CharlieDW
{
    internal class Helpers
    {
        internal static bool IsNotANumber(string userInput)
        {
            string rxPattern = @"[a-zA-z]";

            if (Regex.IsMatch(userInput, rxPattern))
                return true;

            return false;
        }
    }
}
