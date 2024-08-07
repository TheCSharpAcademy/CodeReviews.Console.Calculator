using Newtonsoft.Json;


namespace CalculatorLibrary
{
    public class Calculator
    {
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
        private JsonWriter writer;
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

        /// <summary>
        /// Does operations when 2 numbers are used
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="op"></param>
        /// <param name="calculations"></param>
        /// <returns></returns>
        public double DoOperationTwoNumber(double num1, double num2, string op, ref List<Tuple<string, double>> calculations)
        {
            
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operands2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculations.Add( new Tuple<string, double>( $"Your calclulation was {num1} + {num2}, and the answer was {result}", result));
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was {num1} - {num2}, and the answer was {result}", result));
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was {num1} * {num2}, and the answer was {result}",result));
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    writer.WriteValue("Divide");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was {num1} / {num2}, and the answer was {result}",result));
                    break;
                case "sqr":
                   
                    
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            
            return result;
        }
        /// <summary>
        /// Does operations then one number is used
        /// </summary>
        /// <param name="num"></param>
        /// <param name="op"></param>
        /// <param name="calculations"></param>
        /// <returns></returns>
        public double DoOperationOneNumber(double num, string op, ref List<Tuple<string, double>> calculations)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            writer.WriteStartObject();
            writer.WritePropertyName("Operand");
            writer.WriteValue(num);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "sqr":
                    result = Math.Sqrt(num);
                    writer.WriteValue("Square root");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was square root of number {num}, and the answer was {result}", result));
                    break;
                case "pow":
                    Console.WriteLine("What power you want to take?");
                    string? number = Console.ReadLine();
                    int power = Helpers.NumberValidation1(number);
                    result = Math.Pow(num, power);
                    writer.WriteValue("Taking power");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was taking power of number {num}, and the answer was {result}", result));
                    break;
                case "10x":
                    result = num * 10;
                    writer.WriteValue("10x");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was 10x of number {num}, and the answer was {result}", result));
                    break;
                case "sin":
                    result = Math.Sin(num);
                    writer.WriteValue("Sin");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was sin of number {num}, and the answer was {result}", result));
                    break;
                case "cos":
                    result = Math.Cos(num);
                    writer.WriteValue("Cos");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was cos of number {num}, and the answer was {result}", result));
                    break;
                case "tan":
                    result = Math.Tan(num);
                    writer.WriteValue("Tan");
                    calculations.Add(new Tuple<string, double>($"Your calclulation was cos of number {num}, and the answer was {result}", result));
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
        
    }
}
