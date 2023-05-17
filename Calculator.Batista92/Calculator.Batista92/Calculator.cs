namespace Calculator.Batista92;

internal class Calculator
{
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;

        switch (op)
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
                if (num1 != 0) 
                {
                    result = num1 / num2;
                }
                break;
            default:
                break;
        }
        return result;
    }
}
