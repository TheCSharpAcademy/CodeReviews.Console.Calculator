﻿using Newtonsoft.Json;

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

        public double DoOperation(double num1, double num2, string op)
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

        public void RemoveOperation(int index)
        {
            operations?.RemoveAt(index);
        }

        public void ClearHistory()
        {
            operations?.Clear();
        }

        #endregion Methods
    }
}