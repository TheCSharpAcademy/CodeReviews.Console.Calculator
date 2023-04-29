using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public List<Calculation> calculations { get; set; } = new();

        JsonWriter writer;
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
            Calculation calculation = new() 
            {
                Operand1 = num1,
                Operand2 = num2,
            };
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(calculation.Operand1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(calculation.Operand2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    calculation.Operator = '+';
                    writer.WriteValue("Add");
                    break;
                case "s":
                    calculation.Operator = '-';
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    calculation.Operator = '*';
                    writer.WriteValue("Multiply"); break;
                case "d":
                    if (num2 != 0)
                    {
                        calculation.Operator = '/';
                        writer.WriteValue("Divide");
                    }
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(calculation.Result);
            writer.WriteEndObject();

            calculations.Add(calculation);
            return calculation.Result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
