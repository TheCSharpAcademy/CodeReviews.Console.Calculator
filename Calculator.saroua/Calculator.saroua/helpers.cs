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

    internal static void AddToCalculation(double num1, double num2, string op) {
        calculation.Add(new Calculation
        {
            Num1 = num1,
            Num2 = num2,
            Op = op
        });
    }

    //shows the past calculations and store the result in the variable result

    internal static void showPastCalculation()
    {
        foreach (Calculation calculation in calculation)
        {
            double result = 0;
            switch (calculation.Op) {
                case "a":
                    result = calculation.Num1 + calculation.Num2;
                    Console.WriteLine($"{calculation.Num1} + {calculation.Num2} = {result}");
                    break;
                case "m":
                    result = calculation.Num1 * calculation.Num2;
                    Console.WriteLine($"{calculation.Num1} * {calculation.Num2} = {result}");
                    break;
                case "s":
                    result = calculation.Num1 - calculation.Num2;
                    Console.WriteLine($"{calculation.Num1} - {calculation.Num2} = {result}");
                    break;
                case "d":
                    result = calculation.Num1 / calculation.Num2;
                    Console.WriteLine($"{calculation.Num1} / {calculation.Num2} = {result}");
                    break;
            }
        }
    }


}
