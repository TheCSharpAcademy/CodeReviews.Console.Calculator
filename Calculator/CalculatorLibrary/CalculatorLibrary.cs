using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public int UseAppCounter()
        {
            int useCounter = 0;
            useCounter++;
            return useCounter;
        }

        JsonWriter writer;
        JsonReader reader;
        string filePathJson =
            $@"C:\Users\Ryan\OneDrive\Desktop\Programming\C sharp School\Calculator\Calculator\bin\Debug\net8.0\calculatorlog.json";

        public Calculator()
        {
            StreamWriter logFile = File.CreateText(filePathJson);
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Date and time");
            writer.WriteValue(System.DateTime.Now);
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public class ReadAndParseJson // Working on reading in Json values
        {
            private readonly string _JsonPath;

            public ReadAndParseJson(string filePathJson)
            {
                _JsonPath = filePathJson;
            }

            (int, double, double, string, double) CalculatorReader()
            {
                StreamReader logFIle = File.OpenRead(filePathJson);
                writer = new JsonTextWriter(logFile);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Operations");
                writer.WriteStartArray();
            }
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Calculator Has been used:");
            writer.WriteValue($"{UseAppCounter()} times");
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

            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
