using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        private int _timesUsed;
        private List<string> _calculationsHistory = new ();

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
            string[] oneArgumentOperations = {"r", "x", "tan", "cos", "sin", "cot"};
            
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            if (oneArgumentOperations.Contains(op)) num2 = double.NaN;
                
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
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square Root");
                    break;
                case "x":
                    result = num1 * 10;
                    writer.WriteValue("10x");
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangent");
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosine");
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sine");
                    break;
                case "cot":
                    result = 1 / Math.Tan(num1);
                    writer.WriteValue("Cotangent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            _timesUsed++;
            AddToHistory(num1 + " " + op + " " + num2 + " = " + result);
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WritePropertyName("Times Used");
            writer.WriteValue(GetTimesUsed());
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public int GetTimesUsed()
        {
            return _timesUsed;
        }

        public void ShowCalculationsHistory()
        {
            for (int i = 0; i < _calculationsHistory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_calculationsHistory[i]}");
            }
        }
        
        private void AddToHistory(string calculation)
        {
            _calculationsHistory.Add(calculation);
        }
        
        public void DeleteHistory()
        {
            _calculationsHistory.Clear();
        }
        
        public void DeleteEntry(int index)
        {
            _calculationsHistory.RemoveAt(index);
        }
        
        public int GetHistoryCount()
        {
            return _calculationsHistory.Count;
        }
        
        public double GetHistoryEntry(int index)
        {
            index -= 1;
            
            int equalSignLocation = _calculationsHistory[index].IndexOf('=');
            string result = _calculationsHistory[index].Substring(equalSignLocation + 1).Trim();

            double resultDouble = Convert.ToDouble(result);
            
            return resultDouble;
        }
        
        public double ConvertToRadians(double degrees)
        {
            Console.WriteLine($"{degrees} degrees in radians is {Math.PI / 180 * degrees, 0:0.##}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return (Math.PI / 180) * degrees;
        }
    }
}