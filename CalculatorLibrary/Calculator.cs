using NCalc;
using Spectre.Console;

namespace CalculatorLibrary;

public class Calculator
{
    public double SelectedResult { get; set; } = 0;

    public string? Calculate(string expression)
    {
        Expression exp = new Expression(expression);
        JSONHelpers jSONHelpers = new JSONHelpers();

        try
        {
            string? roundedValue = null;
            object? result = exp.Evaluate();

            if (result != null)
            {
                double doublelValue = Convert.ToDouble(result);
                roundedValue = RoundTheResult(doublelValue);
                jSONHelpers.WriteToJSONFile(expression, roundedValue);
            }
            return roundedValue;
        }
        catch (EvaluationException e)
        {
            AnsiConsole.MarkupLine("[red]Error: Invalid input. Please double-check your expression.[/]\n" +
                $"Details: [purple3]{e.Message}[/]");
            return null;
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]An unexpected error occurred. Please try again.[/]\n" +
                $"Details: [purple3]{e.Message}[/]");
            return null;
        }
    }

    public double GetResult(double num1, double num2, string operation)
    {
        JSONHelpers jSONHelpers = new JSONHelpers();
        double result = double.NaN;

        switch (operation)
        {
            case "Add":
                result = num1 + num2;
                jSONHelpers.WriteToJSONFile($"{num1}+{num2}", RoundTheResult(result));
                break;
            case "Subtract":
                result = num1 - num2;
                jSONHelpers.WriteToJSONFile($"{num1}-{num2}", RoundTheResult(result));
                break;
            case "Multiply":
                result = num1 * num2;
                jSONHelpers.WriteToJSONFile($"{num1}*{num2}", RoundTheResult(result));
                break;
            case "Divide":
                while (true)
                {
                    if (num2 == 0)
                    {
                        AnsiConsole.MarkupLine("[red]Second number can not be '0'![/]");
                        num2 = AnsiConsole.Prompt(new TextPrompt<double>("Type a number, and then press Enter: "));
                    }
                    else break;
                }
                result = num1 / num2;
                jSONHelpers.WriteToJSONFile($"{num1}/{num2}", RoundTheResult(result));
                break;
            case "Square Root":
                result = Math.Sqrt(num1);
                jSONHelpers.WriteToJSONFile($"Square root of {num1}", RoundTheResult(result));
                break;
            case "Taking the Power":
                result = Math.Pow(num1, num2);
                jSONHelpers.WriteToJSONFile($"{num1} to the power of {num2}", RoundTheResult(result));
                break;
            case "10x":
                result = num1 * 10;
                jSONHelpers.WriteToJSONFile($"{num1} times 10", RoundTheResult(result));
                break;
            case "Sine":
                result = Math.Sin(num1 * Math.PI / 180);
                jSONHelpers.WriteToJSONFile($"The sine of {num1} degrees", RoundTheResult(result));
                break;
            case "Cosine":
                result = Math.Cos(num1 * Math.PI / 180);
                jSONHelpers.WriteToJSONFile($"The cosine of {num1} degrees", RoundTheResult(result));
                break;
            case "Tangent":
                result = Math.Tan(num1 * Math.PI / 180);
                jSONHelpers.WriteToJSONFile($"The tangent of {num1} degrees", RoundTheResult(result));
                break;
            default:
                break;
        }
        return result;
    }

    public static string RoundTheResult(double value)
    {
        return Math.Round(value, 4, MidpointRounding.AwayFromZero).ToString();
    }
}
