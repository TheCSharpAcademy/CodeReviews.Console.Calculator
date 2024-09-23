using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class MathOperation
    {
        public string? Operation { get; set; }
        public double Result { get; set; }
    }
    public class Calculator
    {
        List<MathOperation> operations = new List<MathOperation>();
        JsonWriter writer;
        int calculatorUses = 0;
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

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "+":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "-":
                    result = num1 - num2;
                    writer.WriteValue("Substract");
                    break;
                case "*":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;           
                case "h":
                    foreach (var item in operations)
                    {
                        Console.WriteLine(item.Operation);
                    }
                    break;
                case "/":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;
                default:
                    break;
            }
            operations.Add(new MathOperation {Operation = $"{num1} {op} {num2} = {result}", Result = result });
            calculatorUses++;
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Calculator uses");
            writer.WriteValue(calculatorUses);
            writer.WriteEndObject();
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
