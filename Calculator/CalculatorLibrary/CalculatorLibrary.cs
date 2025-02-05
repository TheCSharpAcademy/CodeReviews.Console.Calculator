// CalculatorLibrary.cs
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;

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

        public double DoOperation(double num1, double num2, string op, out string operand)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            operand = "";
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
                    operand = "+";
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result = num1 - num2;
                    operand = "-";
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result = num1 * num2;
                    operand = "*";
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        operand = "/";
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                case "p":
                    result = Math.Pow(num1,num2);
                    operand = "^";
                    writer.WriteValue("Power (x^y)");
                    break;
                default:
                    operand = "?";
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public double DoOperationSingleNum(double num, string op, out string operand)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            operand = "";
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num);
            writer.WritePropertyName("Operand2");
            writer.WriteValue("N/A");
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "q":
                    // Ask the user to enter a non-zero divisor.
                    if (num > 0)
                    {
                        result = Math.Sqrt(num);
                        operand = "\u221A";  //check if correct unicode
                    }
                    writer.WriteValue("Divide");
                    break;
                // Return text for an incorrect option entry.
                case "x":
                    result = num * 10;
                    operand = "10 x";
                    writer.WriteValue("x 10");
                    break;
                default:
                    operand = "?";
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }
        public static string ChooseTrigonometryOperation()
        {
            Console.WriteLine("Choose the trigonometry function from the following list");
            Console.WriteLine("\tc - cos");
            Console.WriteLine("\ts - sin");  
            Console.WriteLine("\tt - tan");
            Console.Write("Your option? ");
            return Console.ReadLine();
        }
        public double DoTrigonometryOperation(double num, string op, out string operand)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            operand = "";
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num);
            writer.WritePropertyName("Operand2");
            writer.WriteValue("N/A");
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "c":
                    result = Math.Cos(num);  //change to cos calc
                    operand = "cos";
                    writer.WriteValue("Cos");
                    break;
                case "s":
                    result = Math.Sin(num);  //change to sin calc
                    operand = "sin";
                    writer.WriteValue("Sin");
                    break;
                case "t":
                    result = Math.Tan(num);   //change to tan calc
                    operand = "tan";
                    writer.WriteValue("Tan");
                    break;
                default:
                    operand = "?";
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
    public class LatestCalculation
    {
        public string CalculationString { get; set; }
        public double CalculationResult { get; set; }

        public LatestCalculation(string s, double d)
        {
            CalculationString = s;
            CalculationResult = d;
        }
    }
 }