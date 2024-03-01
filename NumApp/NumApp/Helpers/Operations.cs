namespace NumApp.Helpers;

internal class Operations
{
    private const string Addition = "+";
    private const string Subtraction = "-";
    private const string Multiplication = "×";
    private const string Division = "÷";
    private const string Square = "x^2";
    private const string SquareRoot = "√x";
    private const string Hexadecimal = "Hex";
    private const string Binary = "Bin";

    /// <summary>
    /// Returns the sum of the two given numbers.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal static double Add(double x, double y)
    {
        return x + y;
    }

    /// <summary>
    /// Returns the difference of the two given numbers.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal static double Subtract(double x, double y)
    {
        return x - y;
    }

    /// <summary>
    /// Returns the product of the two given numbers.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal static double Multiply(double x, double y)
    {
        return x * y;
    }

    /// <summary>
    /// Returns the quotient of the two given numbers.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal static double Divide(double x, double y)
    {
        return x / y;
    }

    /// <summary>
    /// Returns the square of the given number.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    internal static double GetSquare(double x)
    {
        return Math.Pow(x, 2);
    }

    /// <summary>
    /// Returns the square root of the given number.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    internal static double GetSquareRoot(double x)
    {
        return Math.Sqrt(x);
    }

    /// <summary>
    /// Returns a random integer between x and y (inclusive).
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    internal static int GetRandom(double x, double y)
    {
        int xInt = (int)x;
        int yInt = (int)y;

        Random random = new Random();
        int result = random.Next(xInt, yInt + 1);
        return result;
    }

    /// <summary>
    /// Returns the given decimal number converted to hexadecimal (only integers allowed).
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    internal static string GetHexadecimal(double x)
    {
        string input = x.ToString();

        if (input.Contains('.'))
            return "Only integers";

        string hexReversed = "";
        int xInt = (int)x;

        while (true)
        {
            int remainder = xInt % 16;
            string hexRemainder = remainder.ToString();

            hexRemainder = hexRemainder switch
            {
                "15" => "F",
                "14" => "E",
                "13" => "D",
                "12" => "C",
                "11" => "B",
                "10" => "A",
                _ => hexRemainder
            };

            hexReversed += hexRemainder;

            xInt /= 16;

            if (xInt < 1)
                break;
        }

        char[] hexReversedArray = hexReversed.ToCharArray();
        Array.Reverse(hexReversedArray);
        string hexConverted = new string(hexReversedArray);

        return $"0x{hexConverted}";
    }

    /// <summary>
    /// Returns the given decimal number converted to binary (only integers allowed).
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    internal static string GetBinary(double x)
    {
        string input = x.ToString();

        if (input.Contains('.'))
            return "Only integers";

        string binReversed = "";
        int xInt = (int)x;

        while (true)
        {
            int remainder = xInt % 2;
            binReversed += remainder.ToString();

            xInt /= 2;

            if (xInt <= 1)
            {
                binReversed += "1";
                break;
            }
        }

        char[] binReversedArray = binReversed.ToCharArray();
        Array.Reverse(binReversedArray);
        string binConverted = new string(binReversedArray);

        return $"0b{binConverted}";
    }

    /// <summary>
    /// Performs the given calculation on the current value held by the calcultor and the value given.
    /// </summary>
    /// <param name="operationType"></param>
    /// <param name="value"></param>
    internal static void PerformCalculation(string operationType, double value)
    {
        switch (operationType)
        {
            case Addition:
                CalculatorPage.CurrentValue = Operations.Add(CalculatorPage.CurrentValue, value);
                break;
            case Subtraction:
                CalculatorPage.CurrentValue = Operations.Subtract(CalculatorPage.CurrentValue, value);
                break;
            case Multiplication:
                CalculatorPage.CurrentValue = Operations.Multiply(CalculatorPage.CurrentValue, value);
                break;
            case Division:
                CalculatorPage.CurrentValue = Operations.Divide(CalculatorPage.CurrentValue, value);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates the content of the operation entry depending on the operation.
    /// </summary>
    /// <param name="operationType"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static void UpdateOperationEntryValue(Entry operationEntry, string operationType, double value)
    {
        switch (operationType)
        {
            case Square:
                operationEntry.Text = GetSquare(value).ToString();
                break;
            case SquareRoot:
                operationEntry.Text = GetSquareRoot(value).ToString();
                break;
            case Hexadecimal:
                operationEntry.Text = GetHexadecimal(value);
                break;
            case Binary:
                operationEntry.Text = GetBinary(value);
                break;
            default:
                break;
        }
    }
}


