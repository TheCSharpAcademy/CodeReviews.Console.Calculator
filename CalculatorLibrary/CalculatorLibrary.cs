using Newtonsoft.Json;
using System.Collections.Generic; // Added for List
using System.IO; // Added for File operations
using System; // Added for Math functions

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        private int usageCount; // To store usage count
        private List<CalculationEntry> calculationHistory; // To store calculation history

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();

            // Initialize usage count and history
            usageCount = 0;
            calculationHistory = new List<CalculationEntry>();
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

            // Increment usage count
            usageCount++;

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    break;
                case "q": // Square Root
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "p": // Taking the Power
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "t": // 10x
                    result = num1 * 10;
                    writer.WriteValue("Ten Times");
                    break;
                case "sin": // Sine
                    result = Math.Sin(num1 * Math.PI / 180); // Convert to radians
                    writer.WriteValue("Sine");
                    break;
                case "cos": // Cosine
                    result = Math.Cos(num1 * Math.PI / 180); // Convert to radians
                    writer.WriteValue("Cosine");
                    break;
                case "tan": // Tangent
                    result = Math.Tan(num1 * Math.PI / 180); // Convert to radians
                    writer.WriteValue("Tangent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            // Add to history if a valid operation occurred
            if (!double.IsNaN(result) && (op == "a" || op == "s" || op == "m" || op == "d" || op == "q" || op == "p" || op == "t" || op == "sin" || op == "cos" || op == "tan"))
            {
                calculationHistory.Add(new CalculationEntry(num1, num2, op, result));
            }

            return result;
        }

        public int GetUsageCount()
        {
            return usageCount;
        }

        public List<CalculationEntry> GetCalculationHistory()
        {
            return calculationHistory;
        }

        public void ClearCalculationHistory()
        {
            calculationHistory.Clear();
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WritePropertyName("UsageCount");
            writer.WriteValue(usageCount);
            writer.WriteEndObject();
            writer.Close();
        }
    }

    // Helper class to store calculation details for history
    public class CalculationEntry
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }

        public CalculationEntry(double operand1, double operand2, string operation, double result)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            Operation = operation;
            Result = result;
        }

        public override string ToString()
        {
            string opSymbol = "";
            switch (Operation)
            {
                case "a": opSymbol = "+"; break;
                case "s": opSymbol = "-"; break;
                case "m": opSymbol = "*"; break;
                case "d": opSymbol = "/"; break;
                case "q": return $"sqrt({Operand1}) = {Result}";
                case "p": opSymbol = "^"; break;
                case "t": return $"{Operand1} * 10 = {Result}";
                case "sin": return $"sin({Operand1}) = {Result}";
                case "cos": return $"cos({Operand1}) = {Result}";
                case "tan": return $"tan({Operand1}) = {Result}";
            }
            return $"{Operand1} {opSymbol} {Operand2} = {Result}";
        }
    }
}