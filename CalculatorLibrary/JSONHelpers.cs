using Newtonsoft.Json;
using Spectre.Console;

namespace CalculatorLibrary;

public class JSONHelpers
{
    public const string LogFilePath = "calculatorlog.json";

    public List<Operation> Operations;
    public int TimesUsed;

    public JSONHelpers()
    {
        Operations = LoadOperationsFromJsonFile();
        TimesUsed = Operations.Count;
    }

    public void WriteToJSONFile(string expression, string result)
    {
        Operation operation = new() { TimesUsed = ++TimesUsed, Expression = expression, Result = result };
        Operations.Add(operation);
        SaveOperationsToJsonFile();
    }

    public List<string> GetListOfOperations()
    {
        var outputOperations = new List<string>();

        string json = File.ReadAllText(LogFilePath);

        if (!string.IsNullOrWhiteSpace(json))
        {
            ListOfOperations? log = JsonConvert.DeserializeObject<ListOfOperations>(json);
            var operations = log!.Operations!.Select(o => new { o.TimesUsed, o.Expression, o.Result });

            foreach (var operation in operations)
            {
                outputOperations.Add($"{operation.TimesUsed}: {operation.Expression} = {operation.Result}");
            }

            return outputOperations;
        }
        else
        {
            return new List<string>();
        }
    }

    public static List<Operation> LoadOperationsFromJsonFile()
    {
        if (File.Exists(LogFilePath))
        {
            try
            {
                string json = File.ReadAllText(LogFilePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    ListOfOperations listOfOperations = JsonConvert.DeserializeObject<ListOfOperations>(json)!;
                    List<Operation> operations = listOfOperations.Operations!;

                    return operations;
                }
                else
                {
                    return new List<Operation>();
                }
            }
            catch (JsonException e)
            {
                AnsiConsole.MarkupLine($"[red]An error occurred while reading log file.[/]");
                AnsiConsole.MarkupLine($"Details: [red]{e.Message}[/]");
                CreateNewFile();
                return new List<Operation>();
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]Error: {e.Message}[/]");
                CreateNewFile();
                return new List<Operation>();
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No file found![/]");
            CreateNewFile();
            return new List<Operation>();
        }
    }

    private static void CreateNewFile()
    {
        AnsiConsole.MarkupLine($"[yellow]Creating new file...[/]");
        using (StreamWriter logFile = File.CreateText(LogFilePath)) { }

        AnsiConsole.Status()
            .Start("[yellow]Press any key to continue.[/]", ctx =>
            {
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                Console.ReadKey(true);
            });

        Console.Clear();
    }

    public void SaveOperationsToJsonFile()
    {
        using (StreamWriter logFile = File.CreateText(LogFilePath))
        {
            JsonWriter writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();

            foreach (Operation operation in Operations)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("TimesUsed");
                writer.WriteValue(operation.TimesUsed);
                writer.WritePropertyName("Expression");
                writer.WriteValue(operation.Expression);
                writer.WritePropertyName("Result");
                writer.WriteValue(operation.Result);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.Close();
        }
    }

    public static List<string> RetrieveResults()
    {
        var outputResults = new List<string>();

        string json = File.ReadAllText(LogFilePath);

        if (json != null)
        {
            ListOfOperations? log = JsonConvert.DeserializeObject<ListOfOperations>(json);

            var results = log!.Operations!.Select(r => new { r.Result });

            foreach (var result in results)
            {
                string? output = result.Result.ToString();

                outputResults.Add(output!);
            }

            return outputResults;
        }
        else
        {
            return new List<string>();
        }
    }
}
