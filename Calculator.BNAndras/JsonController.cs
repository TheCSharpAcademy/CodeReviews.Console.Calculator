using System.Text.Json.Serialization;
using System.Text.Json;
using Calculator.BNAndras.CalculatorLibrary.Models;

namespace Calculator.BNAndras.CalculatorProgram;

internal static class JsonController
{

    private static string FilePath => "calculatorlog.json";

    private static JsonSerializerOptions Options => new()
    {
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    internal static void LogResults(IEnumerable<CalculationResult> history)
    {
        File.WriteAllText(FilePath, JsonSerializer.Serialize(history, Options));
    }
}