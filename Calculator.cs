
public class Calculator
{
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; 

        switch (op)
        {
            case "+":
                result = Addition(num1, num2);
                Helpers.AddHistory(CalculationType.Add, num1, num2, result); 
                break;

            case "-":
                result = Substaction(num1, num2);
                Helpers.AddHistory(CalculationType.Substact, num1, num2, result);
                break;

            case "*":
                result = Multiplication(num1, num2);
                Helpers.AddHistory(CalculationType.Multiply, num1, num2, result);
                break;

            case "/":
                Console.Clear();
                // Ask the user to enter a non-zero divisor.
                result = Division(num1, num2, result);
                Helpers.AddHistory(CalculationType.Division, num1, num2, result);
                break;

            case "r":
                result = SquareRoot(num1);
                Helpers.AddHistory(CalculationType.SquareRoot, num1, num2, result);
                break;

            case "p":
                result = Power(num1, num2);
                Helpers.AddHistory(CalculationType.Power, num1, num2, result);
                break;

            case "sin":
                result = Sine(num1);
                Helpers.AddHistory(CalculationType.Sine, num1, num2, result);
                break;

            case "cos":
                result = Cosine(num1);
                Helpers.AddHistory(CalculationType.Cosine, num1, num2, result);
                break;

            case "tan":
                result = Tangent(num1);
                Helpers.AddHistory(CalculationType.Tangent, num1, num2, result);
                break;

            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }

    private static double Tangent(double num1)
    {
        Console.Clear();
        double result = Math.Tan(num1);
        Console.WriteLine($"Tangent of {num1} = {result}");
        return result;
    }

    private static double Cosine(double num1)
    {
        Console.Clear();
        double result = Math.Cos(num1);
        Console.WriteLine($"Cosine of {num1} = {result}");
        return result;
    }

    private static double Sine(double num1)
    {
        Console.Clear();
        double result = Math.Sin(num1);
        Console.WriteLine($"Sine of {num1} = {result}");
        return result;
    }

    private static double Power(double num1, double num2)
    {
        Console.Clear();
        double result = Math.Pow(num1, num2);
        Console.WriteLine($"{num1} elevated to {num2} = {result}");
        return result;
    }

    private static double SquareRoot(double num1)
    {
        Console.Clear();
        double result = Math.Sqrt(num1);
        Console.WriteLine($"Square Root of {num1} = {result}");
        return result;
    }

    private static double Division(double num1, double num2, double result)
    {
        if (num2 != 0)
        {
            result = num1 / num2;
            Console.WriteLine($"{num1} / {num2} = {result}");
        }

        return result;
    }

    private static double Multiplication(double num1, double num2)
    {
        Console.Clear();
        double result = num1 * num2;
        Console.WriteLine($"{num1} * {num2} = {result}");
        return result;
    }

    private static double Substaction(double num1, double num2)
    {
        Console.Clear();
        double result = num1 - num2;
        Console.WriteLine($"{num1} - {num2} = {result}");
        return result;
    }

    private static double Addition(double num1, double num2)
    {
        Console.Clear();
        double result = num1 + num2;
        Console.WriteLine($"{num1} + {num2} = {result}");
   
        return result;
    }
}