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
                    previousCalculations.Add($"{calculation.firstNumber} + {calculation.secondNumber} = {calculation.result}");
                    break;
                case "Subtract":
                    previousCalculations.Add($"{calculation.firstNumber} - {calculation.secondNumber} = {calculation.result}");
                    break;
                case "Multiply":
                    previousCalculations.Add($"{calculation.firstNumber} X {calculation.secondNumber} = {calculation.result}");
                    break;
                case "Divide":
                    previousCalculations.Add($"{calculation.firstNumber} / {calculation.secondNumber} = {calculation.result}");
                    break;
            }
        }
        return previousCalculations;
    }
    internal void CalculatorStatistics()
    {
        int calculatorUsage = jsonParse.GetCalculatorUsageStats();
        Console.WriteLine($"This calculator has been used {calculatorUsage} of times");
    }
}
