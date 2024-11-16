using System.Text.Json;
using CalculatorLibrary.Logic;

namespace Calculator.Application;

public class OperationLogWriter : IDisposable
{
    private readonly Utf8JsonWriter _writer;

    public OperationLogWriter()
    {
        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "calculator.log.json");
        if (File.Exists(filepath)) File.Delete(filepath);
        var jsonWriterOptions = new JsonWriterOptions { Indented = true };
        var stream = File.Create(filepath);

        _writer = new Utf8JsonWriter(stream, jsonWriterOptions);

        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
        _writer.Flush();
    }

    public void Log(OperationDetails operationDetails)
    {
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteNumberValue(operationDetails.LeftOperand);
        _writer.WritePropertyName("Operation");
        _writer.WriteStringValue(operationDetails.OperationType.ToString());
        if (operationDetails.OperationType.RequiresTwoOperands())
        {
            _writer.WritePropertyName("Operand2");
            _writer.WriteNumberValue(operationDetails.RightOperand ?? 0);
        }

        _writer.WritePropertyName("Result");
        _writer.WriteNumberValue(operationDetails.Result);
        _writer.WriteEndObject();
        _writer.Flush();
    }

    public void Dispose()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Dispose();
    }
}