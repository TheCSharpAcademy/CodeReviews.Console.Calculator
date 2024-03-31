using Newtonsoft.Json;
using System;

namespace CalculatorLibrary.Arashi256
{
    public class Calculator
    {
        public List<String> CalcHistory = new List<String>();
        public int OperationsCount { get; private set; }
        private JsonWriter _writer;

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            _writer = new JsonTextWriter(logFile);
            _writer.Formatting = Formatting.Indented;
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
            OperationsCount = 0;
        }

        public double DoTrig(double num, string op)
        {
            double result = double.NaN;
            switch (op)
            {
                case "i":
                    result = Math.Sin(num * Math.PI / 180);
                    break;
                case "c":
                    result = Math.Cos(num * Math.PI / 180);
                    break;
                case "z":
                    result = Math.Tan(num * Math.PI / 180);
                    break;
            }
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num);
            _writer.WritePropertyName("Operation");
            _writer.WriteValue(TranslateOperationString(op));
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
            OperationsCount++;
            CalcHistory.Add($"{OperationsCount}: {num} {TranslateOperationString(op)} = {result.ToString("N4")}");
            return result;
        }

        public double DoPowerTen(double num)
        {
            return DoPowerOperation(num, 10, "^");
        }

        public double DoPowerOperation(double baseNum, double exponent, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(baseNum);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(exponent);
            _writer.WritePropertyName("Operation");
            _writer.WriteValue(TranslateOperationString(op));
            result = Math.Pow(baseNum, exponent);
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
            OperationsCount++;
            CalcHistory.Add($"{OperationsCount}: {baseNum} {TranslateOperationString(op)} {exponent} = {result}");
            return result;
        }

        public double DoSquareRoot(double num, string op)
        {
            double result = Math.Sqrt(num);
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num);
            _writer.WritePropertyName("Operation");
            _writer.WriteValue("Sqrt");
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
            OperationsCount++;
            CalcHistory.Add($"{OperationsCount}: {num} {TranslateOperationString(op)} = {result.ToString("N4")}");
            return result;
        }

        public double DoSumOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    _writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    _writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    _writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    _writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
            OperationsCount++;
            CalcHistory.Add($"{OperationsCount}: {num1} {TranslateOperationString(op)} {num2} = {result}");
            return result;
        }

        public string TranslateOperationString(string op)
        {
            if (op == "a") return "+";
            if (op == "s") return "-";
            if (op == "m") return "*";
            if (op == "d") return "/";
            if (op == "r") return "sqrt";
            if (op == "p") return "^";
            if (op == "i") return "sin";
            if (op == "c") return "cos";
            if (op == "z") return "tan";
            return "";
        }

        public void ListHistory()
        {
            foreach (string operation in CalcHistory)
            {
                Console.WriteLine(operation);
            }
        }

        public double GetResultFromList(int index)
        {
            string result = CalcHistory.ElementAt(index - 1);
            return  Convert.ToDouble(result.Substring(result.IndexOf('=') + 1));
        }

        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
        }
    }
}