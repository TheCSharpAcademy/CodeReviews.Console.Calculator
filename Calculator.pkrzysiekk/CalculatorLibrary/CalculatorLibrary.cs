
using Newtonsoft.Json;

namespace CalculatorLibrary
{
  
      public class Calculator
        {
        int TimesUsed { get; set; }
        readonly JsonWriter writer;
       public CalculatorHistory history=new();
       public CalculatorMenu menu=new();
        public Calculator()
        {

            var logFile = new StreamWriter(new FileStream("calculatorlogg.json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                AutoFlush = true
            };
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        // CalculatorLibrary.cs
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
                case "p":
                    return Math.Pow(num1, num2);

                         
                    
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            TimesUsed++;
            return result;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WritePropertyName("Times used");
            writer.WriteValue(TimesUsed);
            writer.WriteEndObject();
            
            writer.Close();
        }
    }
    }

