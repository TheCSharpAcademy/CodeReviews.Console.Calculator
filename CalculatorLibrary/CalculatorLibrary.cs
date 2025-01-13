using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        
        private JsonWriter writer;
        public int TimesUsed { get; set; } // This is a property to keep track of the number of operations performed
        public List<string> Calculations { get; private set; } = new List<string>();


        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
            TimesUsed = 0;
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            string operation = "";
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    operation = "+";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    operation = "-";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    operation = "*";
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    op = "/";
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    operation = "√";
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Taking The Power");
                    operation = "^";
                    break;
                case "t":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10x");
                    operation = "10^";
                    break;
                case "f":
                    result = Math.Sin(num1);
                    writer.WriteValue("Trigonometry Function");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            TimesUsed++;

            Calculations.Add($"{num1} {operation} {num2} = {result}");
            return result;
        }

        public void ClearCalculations()
        {
            Calculations.Clear();
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

    }
}
