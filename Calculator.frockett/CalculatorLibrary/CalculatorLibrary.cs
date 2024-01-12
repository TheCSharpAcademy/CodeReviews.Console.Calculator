using Newtonsoft.Json;

namespace CalculatorLibrary;

public class CalculatorLog
{
    ListFunctions listFunctions = new ListFunctions();
    JsonWriter writer;
    //int totalComputations = 0;

    public CalculatorLog() 
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public void WriteToLog(double num1, double num2, string operation, double result)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        writer.WriteValue(operation);
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();
    }

    public void Finish(int totalComputations)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Computations ran");
        writer.WriteValue(totalComputations);
        writer.WriteEndObject();
        writer.WriteEndArray();
        writer.Close();
    }
}

