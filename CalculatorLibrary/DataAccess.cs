using CalculatorLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary;
public static class DataAccess
{
    public static List<Equasion> LoadEquasions(string filePath)
    {
        var equasions = new List<Equasion>();

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            equasions = JsonConvert.DeserializeObject<List<Equasion>>(json);
        }

        return equasions;
    }

    public static void SaveEquasions(List<Equasion> equasions, string filePath)
    {
        var equsionsJson = JsonConvert.SerializeObject(equasions, Formatting.Indented);
        File.WriteAllText(filePath, equsionsJson);
    }

    public static void DeleteEquasions(List<Equasion> equasions, string filePath)
    {
        equasions.Clear();
        SaveEquasions(equasions, filePath);
    }
}
