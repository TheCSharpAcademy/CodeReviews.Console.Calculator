namespace CalculatorApplication;

public static class Calculator
{
    public static int NumberOfCalculations{ get; set; }
    public static List<double> Calculations { get; set; } = new List<double>();
    public static double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.

        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "s":
                result = num1 - num2;
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "m":
                result = num1 * num2;
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.s
                if (num2 != 0)
                {
                    result = num1 / num2;
                    Calculations.Add(result);
                    NumberOfCalculations++;
                }
                break;
            case "q":
                result = Math.Sqrt(num1);
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "p":
                result = Math.Pow(num1, num2);
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            case "t":
                result = Math.Pow(10,num1);
                Calculations.Add(result);
                NumberOfCalculations++;
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        return result;
    }

    public static string ShowCalculations()
    {
        string result = "";
        if (Calculations.Count == 0)
        {
            result = "There are no calculations to show";
        }
        foreach (var calculation in Calculations)
        {
            result += $"{calculation}, ";
        }
        return result;
    }
    
    public static void ClearCalculations()
    {
        Calculations.Clear();
    }
}