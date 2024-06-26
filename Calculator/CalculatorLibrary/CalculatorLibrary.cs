namespace CalculatorLibrary;

public class Brain
{
    internal delegate string MathDelegate(decimal a, decimal b, out decimal c);
    public static int count = 0;
    public static int total = 0;
    internal static void Calculate()
    {
        count++;

        decimal userOptionChoice = Utility.ReadUserOptionInput();
        CalculationResult calculationResult = new CalculationResult();

        switch (userOptionChoice)
        {
            case 1:
                MathDelegate add = Add;
                Console.WriteLine(add(Utility.inputNumList[0], Utility.inputNumList[1], out decimal addResult));
                calculationResult.Operation = "+";
                calculationResult.Result = addResult;
                break;
            case 2:
                MathDelegate subtract = Subtract;
                Console.WriteLine(subtract(Utility.inputNumList[0], Utility.inputNumList[1], out decimal subtractResult));
                calculationResult.Operation = "-";
                calculationResult.Result = subtractResult;
                break;
            case 3:
                MathDelegate multiply = Multiply;
                Console.WriteLine(multiply(Utility.inputNumList[0], Utility.inputNumList[1], out decimal multiplyResult));
                calculationResult.Operation = "*";
                calculationResult.Result = multiplyResult;
                break;
            case 4:
                MathDelegate divide = Divide;
                Console.WriteLine(divide(Utility.inputNumList[0], Utility.inputNumList[1], out decimal divideResult));
                calculationResult.Operation = "/";
                calculationResult.Result = divideResult;
                break;
            case 5:
                MathDelegate power = Power;
                Console.WriteLine(power(Utility.inputNumList[0], Utility.inputNumList[1], out decimal powerResult));
                calculationResult.Operation = "^";
                calculationResult.Result = powerResult;
                break;
            case 6:

                break;
            default:
                Console.WriteLine("Error: Unrecognized input.");
                break;
        }

        calculationResult.Operand1 = Utility.inputNumList[0];
        calculationResult.Operand2 = Utility.inputNumList[1];
        calculationResult.Time = DateTime.Now;
        CalculationResult.SaveCalculationResult(calculationResult, out int num);
        total = num;
    }
    internal static string Add(decimal x, decimal y, out decimal result)
    {
        result = x + y;
        return Menu.DisplayCalculationOutcome(x, y, "+", result);
    }
    internal static string Subtract(decimal x, decimal y, out decimal result)
    {
        result = x - y;
        return Menu.DisplayCalculationOutcome(x, y, "-", result);
    }
    internal static string Multiply(decimal x, decimal y, out decimal result)
    {
        result = x * y;
        return Menu.DisplayCalculationOutcome(x, y, "*", result);
    }
    internal static string Divide(decimal x, decimal y, out decimal result)
    {
        result = Math.Round(x / y, 4);
        try
        {
            return Menu.DisplayCalculationOutcome(x, y, "/", result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine($"Error: Cannot divide by zero");
        }
        return Menu.DisplayCalculationOutcome(x, y, "/", result);
    }
    internal static string Power(decimal x, decimal y, out decimal result)
    {
        double temp = Math.Pow((double)x, (double)y);
        result = (decimal)temp;
        return Menu.DisplayCalculationOutcome(x, y, "^", result);
    }
}
