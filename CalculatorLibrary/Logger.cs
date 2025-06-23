using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Logger : IDisposable
    {
        private JsonWriter _writer;
        private StreamWriter _logFile;

        public Logger(string logFilePath = "calculatorlog.json")
        {
            _logFile = File.CreateText(logFilePath);
            _logFile.AutoFlush = true;

            _writer = new JsonTextWriter(_logFile)
            {
                Formatting = Formatting.Indented
            };

            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }

        public void LogOperation(double num1, double? num2, string operationCode, string operationName, double result)
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);

            if (num2.HasValue)
            {
                _writer.WritePropertyName("Operand2");
                _writer.WriteValue(num2.Value);
            }

            _writer.WritePropertyName("Operation");
            _writer.WriteValue(operationName);

            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
        }

        public void Dispose()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
            _logFile.Close();
        }
    }
}
