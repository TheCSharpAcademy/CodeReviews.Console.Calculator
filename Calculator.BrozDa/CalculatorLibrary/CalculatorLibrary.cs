using System.Diagnostics;
using Newtonsoft.Json;
namespace CalculatorLibrary
{
    public class Calculator
    {
        private int _numberOfUses;
        private JsonWriter _writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            _writer = new JsonTextWriter(logFile);
            _writer.Formatting = Formatting.Indented;
            InitializeJSONLog();
            _numberOfUses = GetNumberOfUses();
        }
        private int GetNumberOfUses()
        {
            StreamReader readNumberOfUses = new StreamReader("NumberOfUses.txt");
            int numberOfUses = Convert.ToInt32(readNumberOfUses.ReadLine());
            readNumberOfUses.Close();
            return numberOfUses;
        }
        private void WriteNumberOfUses(int numberOfUses)
        {
            StreamWriter writeNumberOfUses = new StreamWriter("NumberOfUses.txt");
            writeNumberOfUses.Write(numberOfUses);
            writeNumberOfUses.Close();
        }
        private void InitializeJSONLog()
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();
        }

        private void AddOperandsToJSON(double num1, double num2)
        {
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operand1");
            _writer.WriteValue(num1);
            _writer.WritePropertyName("Operand2");
            _writer.WriteValue(num2);
            _writer.WritePropertyName("Operation");
        }
        private void AddResultToJSON(double result)
        {
            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            AddOperandsToJSON(num1, num2);
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
            AddResultToJSON(result);
            _numberOfUses++;
            return result;
        }
        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
        }
        public void PrintTitle()
        {
            Console.WriteLine("Welsome to Console Calculator in C#\r");
            Console.WriteLine($"Calculator was used to solve {_numberOfUses} problems so far");
            Console.WriteLine("------------------------\n");
        }
    }
}
