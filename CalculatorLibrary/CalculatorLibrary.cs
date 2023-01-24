using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    private List<Equasion> equasions;
    private string filePath;
    public int counter = 0;    // Number of times the Calculator has been used

    public Calculator(List<Equasion> equasions, string filePath)
    {
        this.equasions= equasions;
        this.filePath = filePath;
    }

    public double DoOperation(double num1, double num2, string operation)
    {
        var eq = new Equasion()
        {
            A= num1,
            B= num2,
            Operation = operation switch
            {
                "a"     => "Add",
                "s"     => "Subtract",
                "m"     => "Multiply",
                "d"     => "Divide",
                "p"     => "Power",
                "r"     => "Root",
                "sin"   => "Sine",
                "cos"   => "Cosine",
                "tg"    => "Tangent",
                "ctg"   => "Cotangent",
            }
        };

        // Use switch statement to do the math;
        switch (operation)
        {
            case "a":
                eq.Result = num1 + num2;
                break;
            case "s":
                eq.Result = num1 - num2;
                break;
            case "m":
                eq.Result = num1 * num2;
                break;
            case "d":
                if (num2 != 0)
                    eq.Result = num1 / num2;
                break;
            case "p":
                eq.Result = Math.Pow(num1, num2);
                break;
            case "r":
                eq.Result = Math.Pow(num2, 1.0 / num1);
                break;
            case "sin":
                eq.Result = Math.Sin(num1);
                break;
            case "cos":
                eq.Result = Math.Cos(num1);
                break;
            case "tg":
                eq.Result = Math.Tan(num1);
                break;
            case "ctg":
                eq.Result = (Math.Cos(num1) / Math.Sin(num1));
                break;
            default:
                break;
        }

        equasions.Add(eq);
        DataAccess.SaveEquasions(equasions, filePath);

        return eq.Result;
    }

    
}
