using Calculator.saroua.Models;

namespace Calculator.saroua;

internal class helpers
{
    //Method that returns the amount of calculator uses incremented by one
    internal static int IncrementTotalUse(int runAmount)
    {
        int totalUse = runAmount;
        totalUse++;
        return totalUse;
    }

    internal static List<Calculation> calculation = new List<Calculation>
    {

    };

    internal static void AddToCalculation(double num1, double num2, string op, double result) {
        calculation.Add(new Calculation
        {
            Num1 = num1,
            Num2 = num2,
            Op = op,
            Result = result
        });
    }

    //shows the past calculations and store the result in the variable result

    internal static void showPastCalculation()
    {
        int i = 0;
        foreach (Calculation calculation in calculation)
        {
            i++;
            switch (calculation.Op) {
                case "a":
                    Console.WriteLine($"{i}: {calculation.Num1} + {calculation.Num2} = {calculation.Result}");
                    break;
                case "m":
                    Console.WriteLine($"{i}: {calculation.Num1} * {calculation.Num2} = {calculation.Result}");
                    break;
                case "s":
                    Console.WriteLine($"{i}: {calculation.Num1} - {calculation.Num2} = {calculation.Result}");
                    break;
                case "d":
                    Console.WriteLine($"{i}: {calculation.Num1} / {calculation.Num2} = {calculation.Result}");
                    break;
                case "p":
                    Console.WriteLine($"{i}: {calculation.Num1} ^ {calculation.Num2} = {calculation.Result}");
                    break;
                case "sr":
                    Console.WriteLine($"{i}: Square root of {calculation.Num1} is {calculation.Result}");
                    break;
                case "sin":
                    Console.WriteLine($"{i}: Sin({calculation.Num1}) = {calculation.Result}");
                    break;
                case "cos":
                    Console.WriteLine($"{i}: Cos({calculation.Num1}) = {calculation.Result}");
                    break;
                case "tan":
                    Console.WriteLine($"{i}: Tan({calculation.Num1}) = {calculation.Result}");
                    break;
            }
        }
    }


}
