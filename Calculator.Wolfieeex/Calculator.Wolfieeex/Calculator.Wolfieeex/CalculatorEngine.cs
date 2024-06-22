namespace Calculator.Wolfieeex;

class CalculatorEngine
{
    public static string GetMathematicalSign(string operation)
    {
        switch (operation.ToLower())
        {
            case "a":
                return "+";
            case "s":
                return "-";
            case "m":
                return "*";
            case "d":
                return "/";
        }
        return "";
    }
    internal static double CalculateResult(ref double number1, ref double number2, string operation)
    {
        switch (operation.ToLower())
        {
            case "a":
                return number1 + number2;
            case "s":
                return number1 - number2;
            case "m":
                return number1 * number2;
            case "d":
                if (number2 == 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write($"\r{new string(' ', Console.BufferWidth)}");
                    Console.Write($"\rYour divisor must not equal 0. Please try again- reinsert your second number and press ENTER: ");
                    HelperMethods.ReadNumericInput(ref number2, "second", true);
                }
                return number1 / number2;
        }
        return double.NaN;
    }
}
