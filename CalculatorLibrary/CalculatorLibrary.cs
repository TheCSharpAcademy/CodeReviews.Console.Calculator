using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    List<Equasion> equasions = new();
    string filePath = "CalculatorLog.json";

    public Calculator()
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            equasions = JsonConvert.DeserializeObject<List<Equasion>>(json);
        }
    }
    public double DoOperation(double num1, double num2, string operation)
    {
        var eq = new Equasion()
        {
            A= num1,
            B= num2,
            Operation = operation switch
            {
                "a" => "Add",
                "s" => "Subtract",
                "m" => "Multiply",
                "d" => "Divide"
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
            default:
                break;
        }

        equasions.Add(eq);
        var equsionsJson = JsonConvert.SerializeObject(equasions);
        File.WriteAllText(filePath, equsionsJson);

        return eq.Result;
    }
}
