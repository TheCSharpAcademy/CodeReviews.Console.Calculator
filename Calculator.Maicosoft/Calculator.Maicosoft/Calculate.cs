namespace Calculator.Maicosoft;

internal class Calculate
{
    internal static double DoMath(double num1, double num2, string operation)
    {
        double result = double.NaN;
        switch (operation)
        {
            case "a":
                result = num1 + num2;
                break;
            case "s":
                result = num2 - num1;
                break;
            case "m":
                result = num1 * num2;
                break;
            case "d":
                result = num1 / num2;
                break;
        }
        return result;
    }
}
