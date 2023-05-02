using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public List<Calculation> calculations { get; set; } = new();
        public int UseCount { get; private set; }

        JsonWriter writer;
        public Calculator()
        {
            UseCount = 0;
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
                case "A":
                    calculation.Operator = '+';
                    writer.WriteValue("Add");
                    break;
                case "S":
                    calculation.Operator = '-';
                    writer.WriteValue("Subtract");
                    break;
                case "M":
                    calculation.Operator = '*';
                    writer.WriteValue("Multiply");
                    break;
                case "D":
                    if (num2 != 0)
                    {
                        calculation.Operator = '/';
                        writer.WriteValue("Divide");
                    }
                    break;
                case "P":
                    calculation.Operator = '^';
                    writer.WriteValue("TakeThePower");
                    break;
                case "R":
                    calculation.Operator = 'r';
                    writer.WriteValue("Root");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(calculation.Result);
            writer.WriteEndObject();

            calculations.Add(calculation);
            UseCount++;
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
