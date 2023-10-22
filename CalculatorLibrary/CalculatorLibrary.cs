using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        #region fields

        private List<Operation>? operations;

        #endregion fields

        #region Constructor

        public Calculator()
        {
            loadHistory();
        }

        #endregion Constructor

        #region Methods

        public double DoOperation(string op, double num1, double num2 = 0)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            Operation operation = new Operation();
            switch (op)
            {
                case Constants.OPERATION_ADD:
                    result = num1 + num2;
                    operation.Type = Constants.ADD;
                    break;
                case Constants.OPERATION_SUB:
                    result = num1 - num2;
                    operation.Type = Constants.SUBSTRACT;
                    break;
                case Constants.OPERATION_MUL:
                    result = num1 * num2;
                    operation.Type = Constants.MULTIPLY;
                    break;
                case Constants.OPERATION_DIV:
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    operation.Type = Constants.DIVIDE;
                    break;
                case Constants.OPERATION_SR:
                    result = Math.Sqrt(num1);
                    operation.Type = Constants.SQUARE_ROOT;
                    break;
                case Constants.OPERATION_P:
                    result = Math.Pow(num1, 2);
                    operation.Type = Constants.POWER;
                    break;
                case Constants.OPERATION_SIN:
                    result = Math.Sin(num1);
                    operation.Type = Constants.SIN;
                    break;
                case Constants.OPERATION_COS:
                    result = Math.Cos(num1);
                    operation.Type = Constants.COS;
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
            if (!File.Exists(Constants.PATH))
            {
                Console.WriteLine("Oh no, the file dose not exist!");
                return;
            }

            // Open the file to read from.
            string json = File.ReadAllText(Constants.PATH);
            operations = JsonConvert.DeserializeObject<List<Operation>>(json);
        }

        public void writeHistory()
        {
            // This text is added only once to the file.
            if (!File.Exists(Constants.PATH))
            {
                Console.WriteLine("Oh no, the file dose not exist!");
                return;
            }

            string json = JsonConvert.SerializeObject(operations, Formatting.Indented);

            // Write calculation history to the file.
            File.WriteAllText(Constants.PATH, json);
        }

        public int getTotalCalculationTimes() => operations.Count;

        public List<Operation> GetHistory() => operations;

        public void ClearHistory()
        {
            operations?.Clear();
            Console.WriteLine("Successfully clear the calculation history.");
        }

        public void PrintHistory()
        {
            Console.WriteLine("================Calculation History=================");
            var ind = 1;
            GetHistory().ForEach(his => { Console.WriteLine($"{ind}: {his}"); ind++; });
            Console.WriteLine("====================================================");
        }

        public double GetCalculationResultById(int id)
        {
            return operations[id].Result;
        }

        public void printCalculationTimes()
        {
            Console.WriteLine($"Your history calculation times is {getTotalCalculationTimes()}");
        }

        #endregion Methods
    }
}