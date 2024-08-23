using CalculatorLibrary;
using Newtonsoft.Json;

namespace CalculatorProgram
{
    class CalculatorOperationHandler
    {
        private CalculatorCounter _counter;
        private CalculatorHistory _calculatorHistory;
        private MathCalculator _mathCalculator;
        private JsonWriter writer;

        internal CalculatorOperationHandler(CalculatorCounter counter, CalculatorHistory history)
        {
            _counter = counter;
            _calculatorHistory = history;
            _mathCalculator = new MathCalculator();


            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        internal double PerformOperation(double num1, double num2, string op)
        {
            double result = _mathCalculator.DoOperation(num1, num2, op);
            _counter.IncrementCounter();

            // Log operation
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    _calculatorHistory.AddToCalculatorHistory(num1, num2, result, "+", OperationType.Add);
                    writer.WriteValue("Addition");
                    break;
                case "s":
                    _calculatorHistory.AddToCalculatorHistory(num1, num2, result, "-", OperationType.Subtract);
                    writer.WriteValue("Subtraction");
                    break;
                case "m":
                    _calculatorHistory.AddToCalculatorHistory(num1, num2, result, "*", OperationType.Multiply);
                    writer.WriteValue("Multiplication");
                    break;
                case "d":
                    _calculatorHistory.AddToCalculatorHistory(num1, num2, result, "/", OperationType.Divide);
                    writer.WriteValue("Division");
                    break;
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
            writer.WriteStartArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}