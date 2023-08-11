namespace Calculator.Maicosoft;

internal class Calculate
{
    internal static string DoMath(double num1, double num2, string operation)
    {
        double result = 0;
        string output = string.Empty;
        string op = operation;

        while (!new[] {"a", "s", "m", "d"}.Contains(op))
        {
            Console.WriteLine("Not a valid input, Please enter an operation: ");
            op = Console.ReadLine();
        }
        switch (op)
        {
            case "a":
                result = num1 + num2;
                output = $"{num1} + {num2} = {result}";
                break;
            case "s":
                result = num2 - num1;
                output = $"{num1} - {num2} = {result}";
                break;
            case "m":
                result = num1 * num2;
                result = Math.Round(result, 2);
                output = $"{num1} * {num2} = {result}";
                break;
            case "d":
                result = num1 / num2;
                result = Math.Round(result, 2);
                output = $"{num1} / {num2} = {result}";
                break;
        }
        return output;
    }
}
