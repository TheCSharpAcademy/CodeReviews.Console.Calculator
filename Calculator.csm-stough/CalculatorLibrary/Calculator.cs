using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        public struct Calculation
        {
            public double left;
            public double right;
            public double result;

            public Calculation(double left, double right, double resut)
            {
                this.left = left;
                this.right = right;
                this.result = resut;
            }
        }

        JsonWriter writer;
        private List<Calculation> calculations;

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();

            calculations = new List<Calculation>();
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
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            calculations.Add(new Calculation(num1, num2, result));

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

        public int GetCalculationNumber()
        {
            return calculations.Count;
        }

        public void DeleteCalculations()
        {
            calculations.Clear();
        }

        public double GetLastResult()
        {
            return calculations[calculations.Count - 1].result;
        }

        public List<Calculation> GetCalculationList()
        {
            return calculations;
        }
    }
}