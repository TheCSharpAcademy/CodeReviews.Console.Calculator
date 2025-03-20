using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public int Counter { get; private set; }
        public List<string> CalculationList { get; set; } = new List<string>();
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
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "sqrt":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "e":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("Exponential");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            Counter++;
            string symbol = op switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "p" => "^",
                "sqrt" => "√",
                "e" => "10^",
                _ => "?"
            };
            if (op == "sqrt" || op == "e") CalculationList.Add($"{symbol}({num1}) = {result}");
            else CalculationList.Add($"{num1} {symbol} {num2} = {result}");
            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WritePropertyName("TotalOperations");
            writer.WriteValue(Counter);

            writer.WritePropertyName("LatestCalculation");
            writer.WriteStartArray();
            foreach (var calculation in CalculationList)
            {
                writer.WriteValue(calculation);
            }
            writer.WriteEndArray();
            writer.Close();
        }

        public void ClearHistory()
        {
            CalculationList.Clear();
            Console.WriteLine("History has been cleared.");
        }
    }
}
