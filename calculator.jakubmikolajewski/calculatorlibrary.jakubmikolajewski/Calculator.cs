using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public static List<string> previousOperations = new List<string>();
        public static List<double> previousResults = new List<double>();
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double DoOperation(string menuChoice, double number1, double number2)
        {
            StartOperationLog(number1, number2);

            double result = 0.0d;
            switch (menuChoice)
            {
                case "a":
                    result = (number1 + number2);
                    writer.WriteValue("Add");
                    previousOperations.Add($"\n{previousResults.Count}. {number1} + {number2} = {result}");
                    break;
                case "s":
                    result = (number1 - number2);
                    writer.WriteValue("Subtract");
                    previousOperations.Add($"\n{previousResults.Count}. {number1} - {number2} = {result}");
                    break;
                case "m":
                    result = (number1 * number2);
                    writer.WriteValue("Multiply");
                    previousOperations.Add($"\n{previousResults.Count}. {number1} * {number2} = {result}");
                    break;
                case "d":
                    number2 = Validator.ValidateDivisorNonZero(number2);
                    result = (number1 / number2);
                    writer.WriteValue("Divide");
                    previousOperations.Add($"\n{previousResults.Count}. {number1} / {number2} = {result}");
                    break;
                case "p":
                    result = (Math.Pow(number1, number2));
                    writer.WriteValue($"Power");
                    previousOperations.Add($"\n{previousResults.Count}. {number1} to the power of {number2} = {result}");
                    break;
                case "r":
                    result = (Math.Pow(number1, 1 / number2));
                    writer.WriteValue($"Root");
                    previousOperations.Add($"\n{previousResults.Count}. Root({number2}) of {number1} = {result}");
                    break;
                case "sin":
                    result = (Math.Sin(number1));
                    writer.WriteValue($"Sin");
                    previousOperations.Add($"\n{previousResults.Count}. Sin of {number1} = {result}");
                    break;
                case "cos":
                    result = (Math.Cos(number1));
                    writer.WriteValue($"Cos");
                    previousOperations.Add($"\n{previousResults.Count}. Cos of {number1} = {result}");
                    break;
            }
            FinishOperationLog(result);

            previousResults.Add(result);
            return result;
        }
        private void StartOperationLog(double number1, double number2)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(number1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(number2);
            writer.WritePropertyName("Operation");
        }
        private void FinishOperationLog(double result)
        {
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
        }
        public void FinishWriter()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}