class Calculator
{
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        // Use a switch statement to do the math.
        switch (op)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "x":
                result = num1 * num2;
                break;
            case "/":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                break;
            case "r":
                // We are a simple calculator and cannot imagine the complex plane!
                if (num2 >= 0)
                {
                    result = Math.Pow(num2, 0.5);
                }
                break;
            case "^":
                // We are STILL a simple calcuator. No complex numbers!
                if (num1 >= 0)
                {
                    result = Math.Pow(num1, num2);
                }
                break;
            case "sin":
                result = Math.Sin(num2);
                break;
            case "cos":
                result = Math.Cos(num2);
                break;
            case "tan":
                result = Math.Tan(num2);
                break;
            case "log":
                result = Math.Log10(num2);
                break;
            case "ln":
                result = Math.Log(num2);
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }
}
