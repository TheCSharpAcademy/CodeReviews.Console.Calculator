namespace SimpleLoggerLibrary;

using Newtonsoft.Json;

public class LoggerLibrary
{
    public string? FileName = @"DefaultName.json";
    public string? ApplicationName = @"Default Application";
    private static JsonWriter? Writer;

    public void OpenLog()
    {
        if(FileName != null){
            // Setup Stream
            StreamWriter log = File.CreateText(FileName);
            log.AutoFlush = true;
            Writer = new JsonTextWriter(log);
            Writer.Formatting = Formatting.Indented;

            //Create top level JSON Object
            Writer.WriteStartObject();
            Writer.WritePropertyName($"{ApplicationName}");
            Writer.WriteStartArray();
        }
    }

    public void WriteLogEntry(string[] LogEvent)
    {
        if (Writer != null){
            Writer.WriteStartObject();
            Writer.WritePropertyName("Event Type");
            Writer.WriteValue(LogEvent[0]);
            for (int i = 1; i < LogEvent.Length; i++)
            {
                Writer.WritePropertyName($"Value {i}");
                Writer.WriteValue(LogEvent[i]);
            }
            Writer.WriteEndObject();
        }
    }

    public void CloseLog()
    {
        if(Writer != null){
            // Complete JSON Structure
            Writer.WriteEndArray();
            Writer.WriteEndObject();
            Writer.Close();
        }

    }

    public dynamic ReadFullLog()
    {
        StreamReader logFile = new StreamReader(FileName ??= @"DefaultName.json");
        string? json = logFile.ReadToEnd();
        
        dynamic? result = JsonConvert.DeserializeObject(json ??= "");

        foreach (object obj in result ??= new Object()){
            System.Console.WriteLine("{0}",obj.ToString());
        }
        return result;
    }
}