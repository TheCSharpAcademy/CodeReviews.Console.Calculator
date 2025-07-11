using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator : IDisposable
    {
        private JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        
        public double ComputeResult(Mode function, double operand1, double operand2)
        {
            var res = 0.0;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand_1");
            writer.WriteValue(operand1);
            writer.WritePropertyName("Operand_2");
            writer.WriteValue(operand2);
            writer.WritePropertyName("Operation");
            switch (function)
            { 
                case Mode.Add:
                    res = operand1 + operand2;
                    writer.WriteValue("Add");
                    break;
                case Mode.Subtract:
                    res = operand1 - operand2;
                    writer.WriteValue("Subtract");
                    break;
                case Mode.Multiply:
                    res = operand1 * operand2;
                    writer.WriteValue("Multiply");
                    break;
                case Mode.Divide:
                    res = operand1 / operand2;
                    writer.WriteValue("Divide");
                    break;
                default:
                    res = 0;
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(res);
            writer.WriteEndObject();
            return res;
        }

        public void Dispose()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }

    public enum Mode
    {
        Add, Subtract, Multiply, Divide
    }
}
