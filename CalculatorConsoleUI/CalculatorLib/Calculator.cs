namespace CalculatorLib;

public static class Calculator
{
    private static List<(double firstOperand, string op, double? secondOperand, double result)> History { get; set; }
    public static int CalculatorUsageCount { get; set; }
    public static void InitializeCalculator()
    {
        History = new List<(double firstOperand, string op, double? secondOperand, double result)>();
        CalculatorUsageCount = 0;
    }

    public static double DoOperation(double firstOperand, string op, double secondOperand)
    {
        double result;
        switch (op)
        {
            case "+":
                result = firstOperand + secondOperand;
                break;
            case "-":
                result = firstOperand - secondOperand;
                break;
            case "*":
                result = firstOperand * secondOperand;
                break;
            case "^":
                result = Math.Pow(firstOperand, secondOperand);
                break;
            case "%":
                result = firstOperand % secondOperand;
                break;
            case "/":
                try
                {
                    result = firstOperand / secondOperand;
                }
                catch (DivideByZeroException ex)
                {

                    throw new Exception($"Error: cant divide by zero {ex.Message}");
                }
                break;
            default:
                throw new InvalidOperationException($"Error: Invalid Operation");
        }
        History.Add((firstOperand, op, secondOperand, result));
        CalculatorUsageCount++;
        return result;

    }
    public static double DoOperation(double operand, string op)
    {
        double result;
        switch (op)
        {
            case "#":
                try
                {
                    op = $"sqrt({operand})";
                    result = Math.Sqrt(operand);
                }
                catch (ArithmeticException ex)
                {
                    throw new ArithmeticException($"Error: you cant take square root of negative number", ex);
                }
                break;
            case "!":
                op = $"10x({operand})";
                result = Math.Pow(10, operand);
                break;
            case "~":
                op = $"sin({operand})";
                result = Math.Sin(operand);
                break;
            case "&":
                op = $"cos({operand})";
                result = Math.Cos(operand);
                break;
            case "$":
                op = $"tan({operand})";
                result = Math.Tan(operand);
                break;
            default: throw new InvalidOperationException("Invalid Operation");
        }
        History.Add((operand, op, null, result));
        CalculatorUsageCount++;
        return result;
    }
    public static double RevealResultFromHistory(int operationIndex)
    {
        var result = History[operationIndex].result;
        return result;
    }
    public static int GetCalculatorHistoryCount() { return History.Count; }
    public static int GetCalculatorUsageCount() { return CalculatorUsageCount; }

    public static void ClearCalculatorHistory()
    {
        History.Clear();
    }

    public static List<(double firstOperand, string operation, double? secondOperand, double result)> GetCalculatorHistory(int? index = null)
    {
        List<(double firstOperand, string operation, double? secondOperand, double result)> result = new();
        if (index == null)
            result = History;
        else
            result.Add(History[index.Value]);
        return result;
    }
}
