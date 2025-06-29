using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        int operationCounter = 0;

        public List<string> calculationHistory = new();

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile)
            {
                Formatting = Formatting.Indented
            };
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public double Add(double num1, double num2) => num1 + num2;

        public double Subtract(double num1, double num2) => num1 - num2;

        public double Multiply(double num1, double num2) => num1 * num2;

        public double Divide(double num1, double num2)
        {
            if (num2 == 0)
            {
                Console.WriteLine("Division by zero is not allowed.");
                return double.NaN;
            }
            return num1 / num2;
        }

        public double SquareRoot(double num)
        {
            if (num < 0)
            {
                Console.WriteLine("Square root of a negative number is not allowed.");
                return double.NaN;
            }
            return Math.Sqrt(num);
        }

        public double Power(double baseNum, double exponent) => Math.Pow(baseNum, exponent);

        public double TenPowerX(double num) => Math.Pow(10, num);

        public double Sine(double angle) => Math.Sin(angle);

        public double Cosine(double angle) => Math.Cos(angle);

        public double Tangent(double angle) => Math.Tan(angle);

        public double DoOperationWithTwoNum(double num1, double num2, string op)
        {
            operationCounter++;

            double result = op switch
            {
                "a" => Add(num1, num2),
                "s" => Subtract(num1, num2),
                "m" => Multiply(num1, num2),
                "d" => Divide(num1, num2),
                "pow" => Power(num1, num2),
                _ => double.NaN
            };

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            writer.WriteValue(GetSymbolForOperation(op));
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Operation Counter");
            writer.WriteValue(operationCounter);
            writer.WriteEndObject();

            string historyEntry = $"{num1} {GetSymbolForOperation(op)} {num2} = {result}";
            calculationHistory.Add(historyEntry);

            return result;
        }

        public double DoOperationWithOneNum(double num1, string op)
        {
            operationCounter++;

            double result = op switch
            {
                "sqrt" => SquareRoot(num1),
                "10x" => TenPowerX(num1),
                "sin" => Sine(num1),
                "cos" => Cosine(num1),
                "tan" => Tangent(num1),
                _ => double.NaN
            };

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operation");
            writer.WriteValue(GetSymbolForOperation(op));
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Operation Counter");
            writer.WriteValue(operationCounter);
            writer.WriteEndObject();

            string historyEntry = $"{GetSymbolForOperation(op)}({num1}) = {result}";
            calculationHistory.Add(historyEntry);

            return result;
        }

        public double DoOperation(double num1, double num2, string op)
        {
            if (op == "sqrt" || op == "10x" || op == "sin" || op == "cos" || op == "tan")
            {
                return DoOperationWithOneNum(num1, op);
            }
            else
            {
                return DoOperationWithTwoNum(num1, num2, op);
            }
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public int GetUsageCount() => operationCounter;

        public List<string> GetCalculationHistory() => calculationHistory;

        public void ClearCalculationHistory() => calculationHistory.Clear();

        private string GetSymbolForOperation(string op)
        {
            return op switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                "sqrt" => "√",
                "pow" => "^",
                "10x" => "10^",
                "sin" => "sin",
                "cos" => "cos",
                "tan" => "tan",
                _ => "?"
            };
        }
    }
}
