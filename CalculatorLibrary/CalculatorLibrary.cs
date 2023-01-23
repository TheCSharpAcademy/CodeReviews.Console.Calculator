using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    public int counter = 0;    // Number of times the Calculator has been used
    private string filePath = "CalculatorLog.json";
    public List<Equasion> equasions;

    public Calculator()
    {
        equasions = LoadEquasions(filePath);
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
        SaveEquasions(equasions);

        return eq.Result;
    }

    private List<Equasion> LoadEquasions(string filePath)
    {
        var equasions = new List<Equasion>();

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            equasions = JsonConvert.DeserializeObject<List<Equasion>>(json);
        }

        return equasions;
    }
    private void SaveEquasions(List<Equasion> equasions)
    {
        var equsionsJson = JsonConvert.SerializeObject(equasions, Formatting.Indented);
        File.WriteAllText(filePath, equsionsJson);
    }
    public void DeleteEquasions()
    {
        equasions.Clear();
        SaveEquasions(equasions);
    }
}
