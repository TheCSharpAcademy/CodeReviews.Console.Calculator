using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;

        private int operationCounter;

        private List<string> log = new List<string>();

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
            string symbol = "";
            double result = double.NaN;

            operationCounter++;

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
                    symbol = "+";
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    symbol = "-";
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    symbol = "*";
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    symbol = "/";
                    writer.WriteValue("Divide");
                    break;
                case "sr":
                    result = Math.Sqrt(num1);
                    symbol = "√";
                    writer.WriteValue("SquareRoot");
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    symbol = "^";
                    writer.WriteValue("Power");
                    break;
                case "p10":
                    result = Math.Pow(10, num1);
                    symbol = "10^";
                    writer.WriteValue("PowerOf10");
                    break;
                case "sin":
                    result = Math.Sin(num1 * Math.PI / 180);
                    symbol = "sin";
                    writer.WriteValue("Sine");
                    break;
                case "cos":
                    result = Math.Cos(num1 * Math.PI / 180);
                    symbol = "cos";
                    writer.WriteValue("Cosine");
                    break;
                case "tan":
                    result = Math.Tan(num1 * Math.PI / 180);
                    symbol = "tan";
                    writer.WriteValue("Tangent");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            if (op == "sr" || op == "p10" || op == "sin" || op == "cos" || op == "tan")
                log.Add($"Operation {operationCounter}: {symbol}{num1} = {result}");
            else
                log.Add($"Operation {operationCounter}: {num1} {symbol} {num2} = {result}");

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WritePropertyName("OperationCount");
            writer.WriteValue(operationCounter);
            writer.WriteEndObject();
            writer.Close();
        }

        public void ClearLog()
        {
            log.Clear();
        }

        public List<string> OperationLog()
        {
            return log;
        }
    }
}