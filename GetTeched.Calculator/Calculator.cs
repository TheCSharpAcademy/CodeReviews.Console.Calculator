namespace GetTeched.Calculator;
class Calculator
{
    public static double DoOperation(double firstNumber, double secondNumber, string operation)
    {
        double result = double.NaN;

        switch (operation)
        {
            case "a":
                result = firstNumber + secondNumber;
                break;
            case "s":
                result = firstNumber - secondNumber;
                break;
            case "m":
                result = firstNumber * secondNumber;
                break;
            case "d":
                if (secondNumber != 0)
                {
                    result = firstNumber / secondNumber;
                }
                break;
            default:
                break;
        }
        return result;
    }
}
