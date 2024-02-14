using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        private List<Operation> operations = new List<Operation>();
        public int TimesUsed { get; set; }

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
            string operation = "";
            
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
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    operation = "/";
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("To Power");
                    operation = "^";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            RecordOperation(num1, num2, operation, result);
            TimesUsed++;

            return result;
        }

        private void RecordOperation(double num1, double num2, string op, double result) {
            operations.Add(new Operation
            {
                FirstOperand = num1,
                SecondOperand = num2,
                OperationType = op,
                Result = result
            });
        }

        public void DeleteLatestOperations() {
            operations.Clear();
        }

        public void ShowLatestOperations()
        {
            foreach (Operation operation in operations)
            {
                Console.WriteLine($"{operation.FirstOperand} {operation.OperationType} {operation.SecondOperand} = {operation.Result}");
            }
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}