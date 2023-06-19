using CalculatorLibrary.Models;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public static class DataAccess
{
    public static List<Equation> LoadEquations(string filePath)
    {
        var equations = new List<Equation>();

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            equations = JsonConvert.DeserializeObject<List<Equation>>(json);
        }

        return equations;
    }

    public static void SaveEquations(List<Equation> equations, string filePath)
    {
        var equsionsJson = JsonConvert.SerializeObject(equations, Formatting.Indented);
        File.WriteAllText(filePath, equsionsJson);
    }

    public static void DeleteEquations(List<Equation> equations, string filePath)
    {
        equations.Clear();
        SaveEquations(equations, filePath);
    }
}
