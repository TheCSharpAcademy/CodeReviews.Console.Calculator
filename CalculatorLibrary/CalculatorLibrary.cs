using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

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

        public class ExpressionsHistory
        {
            public required string Expression {  get; set; }
            public required double Result { get; set; }
        }

        public List<ExpressionsHistory> History { get; set; } = new();
        public int CountExpressions { get; set; };

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {   
                        Expression = $"{num1} + {num2}",
                        Result = result
                    });
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"{num1} - {num2}",
                        Result = result
                    });
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"{num1} * {num2}",
                        Result = result
                    });
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        CountExpressions++;
                        History.Add(new ExpressionsHistory
                        {
                            Expression = $"{num1} / {num2}",
                            Result = result
                        });
                    }
                    writer.WriteValue("Divide");
                    break;
                case "sqrt":
                    result = Math.Sqrt(num1);
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"√{num1}",
                        Result = result
                    });
                    writer.WriteValue("Square Root");
                    break;
                case "pow":
                    result = Math.Pow(num1, num2);
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"{num1}^{num2}",
                        Result = result
                    });
                    writer.WriteValue("Taking the Power");
                    break;
                case "10x":
                    result = num1 * 10;
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"{num1} * 10x",
                        Result = result
                    });
                    writer.WriteValue("10x");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"sin{num1}",
                        Result = result
                    });
                    writer.WriteValue("Sine");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"cos{num1}",
                        Result = result
                    });
                    writer.WriteValue("Cosine");
                    break;
                case "tg":
                    result = Math.Tan(num1);
                    CountExpressions++;
                    History.Add(new ExpressionsHistory
                    {
                        Expression = $"tg{num1}",
                        Result = result
                    });
                    writer.WriteValue("Tangent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
