namespace Console.Calculator.TanukiLoop;

public class Calculator
{

    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default is NAN if an operation, such as division could result in an error.
        
        // Use a switch statemewnt to do the math.
        switch (op)
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
                // ASk the user to enter a non-zero divisor

                if (num2 != 0)
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