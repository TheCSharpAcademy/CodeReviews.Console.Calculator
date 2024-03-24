using CalculatorLibrary;

namespace CalculatorProgram;

internal class CalculatorData
{
    JsonParse jsonParse = new();
    internal List<string> CalculatorHistory()
    {
        List<JsonParse.Calculations> calculations = new();
        calculations = jsonParse.GetCalculationHistory();
        List<string> previousCalculations = new();

        foreach(var calculation in calculations)
        {
            switch(calculation.OperationType)
            {
                case "Add":
                    previousCalculations.Add($"{calculation.FirstNumber} + {calculation.SecondNumber} = {calculation.Result}");
                    break;
                case "Subtract":
                    previousCalculations.Add($"{calculation.FirstNumber} - {calculation.SecondNumber} = {calculation.Result}");
                    break;
                case "Multiply":
                    previousCalculations.Add($"{calculation.FirstNumber} X {calculation.SecondNumber} = {calculation.Result}");
                    break;
                case "Divide":
                    previousCalculations.Add($"{calculation.FirstNumber} / {calculation.SecondNumber} = {calculation.Result}");
                    break;
                case "Square Root":
                    previousCalculations.Add($"{calculation.Result} is the square root of {calculation.FirstNumber}");
                    break;
                case "Power of 10":
                    previousCalculations.Add($"{calculation.FirstNumber}^10 = {calculation.Result}");
                    break;
                case "Power of X":
                    previousCalculations.Add($"{calculation.FirstNumber}^{calculation.SecondNumber} = {calculation.Result}");
                    break;
                case "Sin":
                    previousCalculations.Add($"The sine of {calculation.FirstNumber} degrees is {calculation.Result}");
                    break;
                case "Cos":
                    previousCalculations.Add($"The cosine of {calculation.FirstNumber} degrees is {calculation.Result}");
                    break;
                case "Tan":
                    previousCalculations.Add($"The tangent of {calculation.FirstNumber} degrees is {calculation.Result}");
                    break;
            }
        }
        return previousCalculations;
    }
    internal void CalculatorStatistics()
    {
        int calculatorUsage = jsonParse.GetCalculatorUsageStats();
        Console.WriteLine($"This calculator has been used {calculatorUsage} of times. Press any key to return to the menu");
        Console.ReadLine();
    }

    internal List<double> ResultHistory()
    {
        List<JsonParse.Calculations> calculations = new();
        calculations = jsonParse.GetCalculationHistory();
        List<double> previousResult = new();

        calculations.ForEach(calculation => previousResult.Add(calculation.Result));

        return previousResult;
    }
}
