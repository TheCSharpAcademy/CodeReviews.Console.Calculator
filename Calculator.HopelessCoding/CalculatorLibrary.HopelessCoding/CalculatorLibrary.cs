using Newtonsoft.Json;

namespace CalculatorLibrary.HopelessCoding
{
    public class Calculator
    {
        JsonWriter writer;
        List<string> history = new List<string>();

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
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
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
                    history.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    history.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    history.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        history.Add($"{num1} / {num2} = {result}");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    history.Add($"{num1} ^ {num2} = {result}");
                    break;
                case "l":
                    ShowHistory();
                    break;
                default:
                    break; // Return NaN for an incorrect option entry
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void ShowHistory()
        {
            int i = 1;
            Console.WriteLine("\nYour calculation history:");
            foreach (var item in history)
            {
                Console.WriteLine($"h{i}: {item}");
                i++;
            }
            Console.WriteLine();
        }

        public void ClearHistory()
        {
            history.Clear();
            Console.WriteLine("\nHistory successfully cleared\n");
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public string GetHistoryValue(int historyIndex)
        {
            if ((historyIndex - 1) < history.Count && historyIndex >= 1)
            {
                var subString = history[historyIndex - 1].Trim().Split('=');
                Console.WriteLine($"Value of h{historyIndex} is {subString[1].Trim()}");
                return subString[1].Trim();
            }
            return "Error";
        }
    }
}
