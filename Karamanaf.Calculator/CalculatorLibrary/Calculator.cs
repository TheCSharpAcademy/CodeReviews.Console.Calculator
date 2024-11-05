using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private List<PreviousResults> previousCalculations = new List<PreviousResults>();
        private int usageCount = 0;
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
            double result = double.NaN;
            OperationType opType = OperationType.Double;

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
                    op = "+";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    op = "-";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    op = "*";
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
                    opType = OperationType.Single;
                    op = $"\u221a{num1}";
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    op = "^";
                    break;
                case "x":
                    result = num1 * 10;
                    writer.WriteValue("10x");
                    opType = OperationType.Single;
                    op = $"10x{num1}";
                    break;
                case "q":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    opType = OperationType.Single;
                    op = $"sin{num1}";
                    break;
                case "w":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    opType = OperationType.Single;
                    op = $"cos{num1}";
                    break;
                case "t":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    opType = OperationType.Single;
                    op = $"tan{num1}";
                    break;
                default:
                    writer.WriteValue("Invalid Operation");
                    break;
            }

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            if (!double.IsNaN(result))
            {
                previousCalculations.Add(new PreviousResults(num1, num2, op, opType, result));
                usageCount++;
            }

            return result;
        }

        public void ClearPreviousCalculations()
        {
            previousCalculations.Clear();
            Console.WriteLine("Previous calculations have been cleared.");
        }

        public List<PreviousResults> GetPreviousCalculations()
        {
            return previousCalculations;
        }

        public double? GetResultById(int id)
        {
            if (id > 0 && id <= previousCalculations.Count)
            {
                return previousCalculations[id - 1].Result;
            }
            Console.WriteLine("Invalid ID. No result found.");
            return null;
        }

        public void ShowPreviousResults()
        {
            var previousResults = GetPreviousCalculations();
            if (previousResults != null && previousResults.Count > 0)
            {
                foreach (var result in previousResults)
                {
                    Console.Write($"{result.Id}. ");
                    if (result.OpType == OperationType.Single)
                    {
                        Console.WriteLine($"{result.Operation} = {result.Result}");
                    }
                    else
                    {
                        Console.WriteLine($"{result.Operand1} {result.Operation} {result.Operand2} = {result.Result}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No previous results found.");
            }
        }

        public int GetUsageCount()
        {
            return usageCount;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
