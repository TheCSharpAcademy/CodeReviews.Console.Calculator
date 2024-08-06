using Newtonsoft.Json;


namespace CalculatorProgram
{
    public class Calculator
    {
        private static int operationCount; // Counter for operations
        private List<string> calculationHistory = new List<string>(); // List to store calculation history

        JsonWriter writer;

        // Adding constructor
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

            // Increment the operation counter
            operationCount++;

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculationHistory.Add($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculationHistory.Add($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculationHistory.Add($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                        calculationHistory.Add($"{num1} / {num2} = {result}");
                    }
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    calculationHistory.Add($"{num1} ^ {num2} = {result}");
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

        public double DoSingleOperation(double num, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand");
            writer.WriteValue(num);
            writer.WritePropertyName("Operation");

            // Increment the operation counter
            operationCount++;

            // Use a switch statement to do the math.
            switch (op)
            {
                case "sqrt":
                    result = Math.Sqrt(num);
                    writer.WriteValue("Square Root");
                    calculationHistory.Add($"sqrt({num}) = {result}");
                    break;
                case "10x":
                    result = Math.Pow(10, num);
                    writer.WriteValue("10^x");
                    calculationHistory.Add($"10^{num} = {result}");
                    break;
                case "sin":
                    result = Math.Sin(num);
                    writer.WriteValue("Sine");
                    calculationHistory.Add($"sin({num}) = {result}");
                    break;
                case "cos":
                    result = Math.Cos(num);
                    writer.WriteValue("Cosine");
                    calculationHistory.Add($"cos({num}) = {result}");
                    break;
                case "tan":
                    result = Math.Tan(num);
                    writer.WriteValue("Tangent");
                    calculationHistory.Add($"tan({num}) = {result}");
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
            writer.WritePropertyName("TotalOperations");
            writer.WriteValue(operationCount); // Log the total number of operations
            writer.WriteEndObject();
            writer.Close();
        }

        public List<string> GetCalculationHistory()
        {
            return calculationHistory;
        }

        public void ClearCalculationHistory()
        {
            calculationHistory.Clear();
        }
    }
}
