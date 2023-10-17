using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private List<Operation>? operations;
        private const string PATH = "calculatorlog.json";
        private const string ADD = "Add";
        private const string SUBSTRACT = "Substract";
        private const string MULTIPLY = "Multiply";
        private const string DIVIDE = "Divide";
        private const string OPERATION_ADD = "a";
        private const string OPERATION_SUB = "s";
        private const string OPERATION_MUL = "m";
        private const string OPERATION_DIV = "d";

        public Calculator()
        {
            loadHistory();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            Operation operation = new Operation();
            switch (op)
            {
                case OPERATION_ADD:
                    result = num1 + num2;
                    operation.Type = ADD;
                    break;
                case OPERATION_SUB:
                    result = num1 - num2;
                    operation.Type = SUBSTRACT;
                    break;
                case OPERATION_MUL:
                    result = num1 * num2;
                    operation.Type = MULTIPLY;
                    break;
                case OPERATION_DIV:
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    operation.Type = DIVIDE;
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            operation.Operand1 = num1;
            operation.Operand2 = num2;
            operation.Result = result;
            operations ??= new();
            operations.Add(operation);
            return result;
        }

        private void loadHistory()
        {
            // This text is added only once to the file.
            if (!File.Exists(PATH))
            {
                Console.WriteLine("Oh no, the file dose not exist!");
                return;
            }

            // Open the file to read from.
            string json = File.ReadAllText(PATH);
            operations = JsonConvert.DeserializeObject<List<Operation>>(json);
        }

        public void writeHistory()
        {
            // This text is added only once to the file.
            if (!File.Exists(PATH))
            {
                Console.WriteLine("Oh no, the file dose not exist!");
                return;
            }

            string json = JsonConvert.SerializeObject(operations, Formatting.Indented);

            // Write calculation history to the file.
            File.WriteAllText(PATH, json);
        }

        public int getTotalCalculationTimes() => operations.Count;
    }
}