namespace Calculator.CharlieDW
{
    public class Calculator
    {
        public static double DoOperation(double num1, double num2, string operation)
        {
            double result = double.NaN;

            switch (operation)
            {
                case "a":
                    result = num1 + num2;
                    break;
                case "s":
                    result = num1 - num2;
                    break;
                case "m":
                    result = num1 * num2;
                    break;
                case "d":
                    if (num2 != 0)
                        result = num1 / num2;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
